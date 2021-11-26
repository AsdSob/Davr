using System.Collections.Generic;
using System.Linq;
using Davr.Vash.Authorization;
using Davr.Vash.DTOs.Users;
using Davr.Vash.Entities;
using Davr.Vash.Helpers;
using Microsoft.Extensions.Options;
using BCryptNet = BCrypt.Net.BCrypt;


namespace Davr.Vash.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequest model);

        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UserService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }


        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id) 
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }


        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Role = model.Role,
                Username = model.Username,
                BranchId = model.BranchId,
            };

            // hash password
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // save user
            _context.Users.Add(user);
            _context.SaveChanges();
        }


        public void Update(int id, UpdateRequest model)
        {
            var user = GetById(id);

            // validate
            if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // copy model to user and save
            if (!string.IsNullOrEmpty(model.Username))
            {
                user.Username = model.Username;
            }
            if (!string.IsNullOrEmpty(model.FirstName))
            {
                user.FirstName = model.FirstName;
            }
            if (!string.IsNullOrEmpty(model.LastName))
            {
                user.LastName = model.LastName;
            }
            if (!string.IsNullOrEmpty(model.MiddleName))
            {
                user.MiddleName = model.MiddleName;
            }
            if (model.Role != null )
            {
                user.Role = model.Role;
            }
            
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = GetById(id);

            if (user.Username.ToLower() == "admin")
            {
                throw new AppException(user.Username + " cannot be deleted");
                return;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}