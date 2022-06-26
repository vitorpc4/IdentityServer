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
            if (result.IsSuccess) return Ok(result.Reasons);
            return Unauthorized(result.Errors);
        }
    }
}
