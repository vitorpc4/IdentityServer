using FluentResults;
using IdentityServer.Data.Dtos;
using IdentityServer.Data.Requests;
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
            if (result.IsSuccess) return Ok(result);
            return StatusCode(500);
        }

        [HttpGet("/active")]
        public IActionResult ConfirmAccount([FromQuery] ActiveAccountRequest request)
        {
            Result resultado = _registerService.ConfirmAccount(request);
            if(resultado.IsSuccess) return Ok(resultado.Successes);
            return StatusCode(500);

        }
    }
}
