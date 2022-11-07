using System.Security.Cryptography;
using System.Text;
using DatingApp.API.Data;
using DatingApp.API.Data.Entities;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
namespace DatingApp.API.Controllers
{
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;
        public AuthController(
            IAuthService authService,
            DataContext context,
            ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
            _authService = authService;
        }
 
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                return Ok(_authService.Register(registerUserDto));
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
 
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthUserDto authUserDto)
        {
            try
            {
                return Ok(_authService.Login(authUserDto));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }
    }
}
