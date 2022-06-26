using FluentResults;
using IdentityServer.Data.Requests;
using IdentityServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult loginUser(RequestLogin requestLogin)
        {
            Result result = _loginService.LoginUser(requestLogin);
            if (result.IsSuccess) return Ok(result);
            return Unauthorized(result.Errors);
        }

        [HttpPost("/request-newpassword")]
        public IActionResult NewPassowrd(RequestNewPassword requestNewPassowrd)
        {
            Result result = _loginService.NewPassword(requestNewPassowrd);
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result.Errors);
        }

        [HttpPost("/change-password")]
        public IActionResult changePassword(RequestChangePassword requestChangePassword)
        {
            Result result = _loginService.changePassword(requestChangePassword);
            if(result.IsSuccess) return Ok(result);
            return BadRequest(result.Errors);
        }
    }
}
