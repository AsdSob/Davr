using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Davr.Vash.Authorization;
using Davr.Vash.DTOs;
using Davr.Vash.DTOs.Users;
using Davr.Vash.Entities;
using Davr.Vash.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Davr.Vash.Controllers
{
    [EnableCors]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
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
            var models = _userService.GetAll();
            var users = _mapper.Map<IList<UserDto>>(models).ToArray();
            return Ok(users);
        }

        [HttpGet("{id:int}" )]
        public IActionResult GetById(int id)
        {
            // only admins can access other user records
            var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.Id && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var model =  _userService.GetById(id);

            var user = _mapper.Map<UserDto>(model);

            return Ok(user);
        }

        [HttpPost("{regRequest}")]
        public IActionResult Register([FromBody]RegisterRequest newUser)
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
