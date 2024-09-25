using Application.DTOs.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserApplication _loginApplication;

        public LoginController(IUserApplication userApplication)
        {
            _loginApplication = userApplication;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequestDto userRequestDto)
        {
            var response = await _loginApplication.RegisterUser(userRequestDto);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Generate")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenRequestDto requestDto)
        {
            var response = await _loginApplication.GenerateToken(requestDto);
            return Ok(response);
        }
    }
}
