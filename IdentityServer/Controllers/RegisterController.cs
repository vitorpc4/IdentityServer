using FluentResults;
using IdentityServer.Data.Dtos;
using IdentityServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private RegisterService _registerService;
        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public IActionResult Register(RegisterDto registerDto)
        {
            Result result = _registerService.Register(registerDto);
            if (result.IsSuccess) return Ok(result.Reasons);
            return StatusCode(500);
        }
    }
}
