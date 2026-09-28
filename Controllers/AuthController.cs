using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventra.DTOs.Request;
using Inventra.Services.Implementation;
using Inventra.Services.Interface;
using System.Security.Claims;
namespace Inventra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Auth Controller Works");
        }
        [HttpGet("current-user-id")]
        [Authorize]
        public IActionResult CurrentUserId()
        {
            var employeeId = User.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            return Ok(employeeId);
        }
        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            return Ok(new
            {
                EmployeeId = User.FindFirst(
                    System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,

                Email = User.FindFirst(
                    System.Security.Claims.ClaimTypes.Email)?.Value,

                Role = User.FindFirst(
                    System.Security.Claims.ClaimTypes.Role)?.Value
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (result == null)
                return Unauthorized("Invalid Email or Password");

            var token = _jwtService.GenerateToken(result);
            return Ok(new { Token = token });
        }
    }
}
