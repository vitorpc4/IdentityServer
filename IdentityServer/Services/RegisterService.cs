using AutoMapper;
using FluentResults;
using IdentityServer.Data;
using IdentityServer.Data.Dtos;
using IdentityServer.Data.Requests;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Web;

namespace IdentityServer.Services
{
    public class RegisterService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        public RegisterService(AppDbContext context, IMapper mapper, UserManager<CustomIdentityUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result Register(RegisterDto registerDto)
        {
            User user = _mapper.Map<User>(registerDto); 
            CustomIdentityUser userIdentity = _mapper.Map<CustomIdentityUser>(user);

            Task<IdentityResult> resultIdentity = _userManager
                .CreateAsync(userIdentity, registerDto.Password);
            _userManager.AddToRoleAsync(userIdentity, "regular");
            if (resultIdentity.Result.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(userIdentity).Result;
                string encodedToken = HttpUtility.UrlEncode(code);
                string linkActivation = $"http://localhost:5163/active?userId={userIdentity.Id}&activateCode={encodedToken}";
                return Result.Ok().WithSuccess(linkActivation);
            };

            return Result.Fail("An error occurred while registering this user");

        }

        public Result ConfirmAccount(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.userId);
            if(identityUser != null)
            {
                if (request.activateCode != null)
                {
                    var identityResult = _userManager.ConfirmEmailAsync(identityUser, request.activateCode).Result;
                    if (identityResult.Succeeded)
                    {
                        return Result.Ok();
                    }
                }
            }
            return Result.Fail("User account activation failed");
        }
    }
}
