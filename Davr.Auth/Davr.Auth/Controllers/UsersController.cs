using Davr.Auth.Authorization;
using Davr.Auth.Entities;
using Davr.Auth.Helpers;
using Davr.Auth.Models.Users;
using Davr.Auth.Services;
using DavrBank.AuthorizationApi.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Davr.Auth.Controllers
{
    [Authorize]
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [AutoValidate]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:int}" )]
        public IActionResult GetById(int id)
        {
            // only admins can access other user records
            var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.Id && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var user =  _userService.GetById(id);
            return Ok(user);
        }

        [HttpPut("{regRequest}")]
        public IActionResult Register(RegisterRequest newUser)
        {
            // only admins can register new users
            _userService.Register(newUser);

            return Ok(new { message = "Registration successful" });
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
