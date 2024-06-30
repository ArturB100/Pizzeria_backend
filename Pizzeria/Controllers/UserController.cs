using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Data;
using Pizzeria.Dto;
using Pizzeria.Model;
using Pizzeria.Services;
using System.Diagnostics;
using System.Net;

namespace Pizzeria.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        private UserService _userService;
        private readonly JWTauthService _jwtService;
        private readonly AddressService _addressService;
        private readonly IMapper _mapper;

        public UserController( UserService userService, JWTauthService jWTauthService, AddressService addressService, IMapper mapper) 
        {
            _userService = userService; 
            _jwtService = jWTauthService;
            _addressService = addressService;
            _mapper = mapper;
        }

/*
        [HttpGet("allUsers")]
        public IActionResult GetUsers ()
        {
            var users = _dbContext.User;
            return Ok(users);
        }*/

        [HttpGet]
        [ProducesResponseType(typeof(UserShowDataDto), (int)HttpStatusCode.OK)]
        public IActionResult GetLoggedUserData ()
        {
            User user = _jwtService.GetUserFromRequest(HttpContext);
            if (user == null)
            {
                return Unauthorized();
            }
            UserShowDataDto userDto = _mapper.Map<UserShowDataDto>(user);
            Address userAddress = _userService.GetAddressOfUser(user);
                        
            userDto.Address = userAddress;
            return Ok(userDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult), (int)HttpStatusCode.OK)]
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

        [HttpPost("assignAddress")]
        public IActionResult AssignAddressToUser (NewAddressDto dto)
        {
            User user = _jwtService.GetUserFromRequest (HttpContext);
            if (user == null)
            {
                return Unauthorized();
            }

            Address address = _addressService.AddAddressIfNotExists(dto);
            _userService.AssignAddressToUser(address, user);
            return Ok(address);

        }




    }
}
