using FluentResults;
using IdentityServer.Data.Requests;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public class LoginService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;
        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public Result LoginUser(RequestLogin requestLogin)
        {
            var user = recoveryUserByEmail(requestLogin.Email);
            var resultIdentity = _signInManager
                .PasswordSignInAsync(user.UserName, requestLogin.Password, false, false);
            if (resultIdentity.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(u => u.NormalizedEmail == requestLogin.Email.ToUpper());
                var getRole = _signInManager
                    .UserManager
                    .GetRolesAsync(identityUser).Result.FirstOrDefault();
                Token token = _tokenService
                    .CreateToken(identityUser, getRole);
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }

        private CustomIdentityUser recoveryUserByEmail(string email)
        {
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
            if (identityUser != null)
            {
                return identityUser;
            }
            return null;
        }

    }
}
