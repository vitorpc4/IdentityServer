using FluentResults;
using IdentityServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;
        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Result result = _logoutService.Logout();
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
    }
}
