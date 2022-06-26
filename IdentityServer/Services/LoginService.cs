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
        private UserManager<CustomIdentityUser> _userManager;
        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService, UserManager<CustomIdentityUser> userManager)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
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
        public Result NewPassword(RequestNewPassword requestNewPassowrd)
        {
           var identityUser = recoveryUserByEmail(requestNewPassowrd.Email);
           if(identityUser != null)
            {
                var tokenPassword = _userManager.GeneratePasswordResetTokenAsync(identityUser).Result;
                return Result.Ok().WithSuccess(tokenPassword);
            }
            return Result.Fail("an error occurred when requesting the token to update the password");
        }

        public Result changePassword(RequestChangePassword requestChangePassword)
        {
            var identityUser = recoveryUserByEmail(requestChangePassword.Email);
            if(identityUser != null)
            {
                var resetPassword = _userManager.ResetPasswordAsync(identityUser, requestChangePassword.Token,requestChangePassword.Password).Result;
                if (resetPassword.Succeeded)
                {
                    return Result.Ok().WithSuccess("Password change was successful");
                }
                
            }
            return Result.Fail("An error occurred when resetting the password");
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
