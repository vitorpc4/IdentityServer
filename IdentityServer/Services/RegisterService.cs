﻿using AutoMapper;
using FluentResults;
using IdentityServer.Data;
using IdentityServer.Data.Dtos;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public class RegisterService
    {
        private IMapper _mapper;
        private AppDbContext _context;
        private UserManager<CustomIdentityUser> _userManager;
        public RegisterService(AppDbContext context, IMapper mapper, UserManager<CustomIdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result Register(RegisterDto registerDto)
        {
            User user = _mapper.Map<User>(registerDto); 
            CustomIdentityUser userIdentity = _mapper.Map<CustomIdentityUser>(user);

            Task<IdentityResult> resultIdentity = _userManager
                .CreateAsync(userIdentity, registerDto.Password);
            if (resultIdentity.Result.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(userIdentity).Result;
                return Result.Ok().WithSuccess(code);
            };

            return Result.Fail("An error occurred while registering this user");

        }
    }
}