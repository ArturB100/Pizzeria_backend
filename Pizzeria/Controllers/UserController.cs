using Microsoft.AspNetCore.Mvc;
using Pizzeria.Data;
using Pizzeria.Dto;
using Pizzeria.Model;
using Pizzeria.Services;

namespace Pizzeria.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        private UserService _userService;
        private readonly JWTauthService _jwtService;

        public UserController( UserService userService, JWTauthService jWTauthService) 
        {
            _userService = userService; 
            _jwtService = jWTauthService;
        }

/*
        [HttpGet("allUsers")]
        public IActionResult GetUsers ()
        {
            var users = _dbContext.User;
            return Ok(users);
        }*/

        [HttpGet]
        public IActionResult GetLoggedUserData ()
        {
            User user = _jwtService.GetUserFromRequest(HttpContext);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult RegisterNewUser (NewUserDtoReq dto)
        {
            OperationResult results = _userService.RegisterNewUser(dto);
            if (results.Success)
            {
                return Ok(results);
            }
            else
            {
                return BadRequest(results);
            }
        }




    }
}
