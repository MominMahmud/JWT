using JsonWT.Context;
using JsonWT.DTOs;
using JsonWT.Helpers;
using JsonWT.Models;
using Microsoft.AspNetCore.Mvc;

namespace JsonWT.Controllers
{
    [Route("api")]

    [ApiController]

    public class AuthController:Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        
        public AuthController(IUserRepository repository, JwtService jwtService) {
            _userRepository = repository;
            _jwtService = jwtService;
        }
        [HttpPost(template:"register")]
        public IActionResult Register(RegisterDto dto)
        {

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword( dto.Password)
            };
            _userRepository.Create(user);
            return Created(uri:"Success",value:_userRepository.Create(user));
        }

        [HttpPost(template:"login")]

        public IActionResult Login(LoginDto dto) {

           var user = _userRepository.getByEmail(dto.Email);
            if (user == null)
            {
                return BadRequest(error:new {message="Invalid Credentials"});
            }
            if(!BCrypt.Net.BCrypt.Verify(text:dto.Password, hash: user.Password))
            {
                return BadRequest(error: new { message = "Invalid Credentials" });
            }

            var jwt= _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", value: jwt, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(new
            {
                message = "success"

            });


        }

        [HttpGet(template:"user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _userRepository.getById(userId);
                return Ok(user);
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
        }

        [HttpPost(template:"logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(key: "jwt");
            return Ok(new
            {
                message="success"
            }); ;
        }

        
    }
}
