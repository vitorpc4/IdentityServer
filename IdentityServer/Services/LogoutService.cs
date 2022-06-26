using FluentResults;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public class LogoutService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        public LogoutService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result Logout()
        {
            var identityLogout = _signInManager.SignOutAsync();
            if (identityLogout.IsCompletedSuccessfully)
            {
                return Result.Ok();
            }
            return Result.Fail("An error occurred when exiting the application");
        }
    }
}
