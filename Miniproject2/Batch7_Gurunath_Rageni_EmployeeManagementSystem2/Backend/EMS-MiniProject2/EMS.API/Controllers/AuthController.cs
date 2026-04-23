using EMS.API.DTOs;
using EMS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth) => _auth = auth;

        /// <summary>Register a new Admin or Viewer account.</summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AuthRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _auth.RegisterAsync(dto);
            if (!result.Success) return Conflict(result);
            return Ok(result);
        }

        /// <summary>Login and receive a JWT token.</summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] AuthRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _auth.Login(dto);
            if (!result.Success) return Unauthorized(result);
            return Ok(result);
        }
    }
}