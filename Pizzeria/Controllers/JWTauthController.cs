using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Model;
using Pizzeria.Services;

namespace Pizzeria.Controllers
{
    [Route("auth")]
    [ApiController]
    public class JWTauthController : Controller
    {
     

        private readonly JWTauthService _JWTauthService;
        private readonly UserService _userService;

        public JWTauthController (JWTauthService jWTauthService, UserService userService)
        {
            _JWTauthService = jWTauthService;
            _userService = userService;
        }


        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            User user = _userService.Authenticate(loginDto.email, loginDto.password);
            if (user == null)
            {
                return Unauthorized();
            }

            int userId = user.Id;

            var token = _JWTauthService.GenerateJwtToken(userId.ToString());

            return Ok(new { token });
        }

        [HttpGet("testGetId")]
        public IActionResult TestGetId()
        {
            int id = _JWTauthService.GetUserIdFromRequest(HttpContext);
            return Ok(new {id});
        }


    }
}
