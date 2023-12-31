﻿using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Models.Enums;
using HaberApp.Core.Services;
using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Exceptions;
using HaberApp.ServiceLayer.HelperModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HaberApp.ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ResponseResult<string> responseResult;
        private readonly JwtSettings jwtSettings;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, JwtSettings jwtSettings)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.responseResult = new ResponseResult<string>();
            this.jwtSettings = jwtSettings;
        }
        public async Task<ResponseResult<string>> CreateAppUserUserAsync(CreateUserRequestDto model)
        {
            var user = new AppUser()
            {
                Email = model.EMail,
                UserName = model.EMail,
                Name = model.Name,
                Surname = model.Surname
            };
            var userResult = await userManager.CreateAsync(user, model.Password);
            if (userResult.Succeeded)
            {

                var roleResult = await userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleTypes), model.RoleType));
                if (roleResult.Succeeded)
                {
                    this.responseResult.Entity = "User Created";
                    return this.responseResult;
                }
                else
                {
                    await userManager.DeleteAsync(user);
                    throw new CustomAppException(roleResult.Errors.Select(a => a.Description).ToList());
                }
            }
            else
            {
                throw new CustomAppException(userResult.Errors.Select(a => a.Description).ToList());
            }
        }

        public async Task<ResponseResult<string>> LoginUser(LoginUserRequestDto model)
        {
            if (model == null)
            {
                this.responseResult.StatusCode = System.Net.HttpStatusCode.BadRequest;
                this.responseResult.Success = false;
                this.responseResult.ErrorList.Add("Bilgileri Giriniz!");
                return this.responseResult;
            }

            var user = await userManager.FindByEmailAsync(model.EMail);
            if (user == null)
            {
                this.responseResult.StatusCode = System.Net.HttpStatusCode.NotFound;
                this.responseResult.Success = false;
                this.responseResult.ErrorList.Add("Kullanıcı Bulunamadı!");
                return this.responseResult;
            }

            if (await userManager.IsLockedOutAsync(user))
            {
                this.responseResult.StatusCode = System.Net.HttpStatusCode.BadRequest;
                this.responseResult.Success = false;
                this.responseResult.ErrorList.Add("Oturum Kilitlenmiştir");
                return this.responseResult;
            }

            if (await userManager.CheckPasswordAsync(user, model.Password) == false)
            {
                this.responseResult.StatusCode = System.Net.HttpStatusCode.BadRequest;
                this.responseResult.Success = false;
                await this.userManager.AccessFailedAsync(user);
                var count = await this.userManager.GetAccessFailedCountAsync(user);
                this.responseResult.ErrorList.Add($"Şifre Yanlış. Kalan Deneme Hakkınız {count}");
                return this.responseResult;
            }

            var claims = new List<Claim>();
            claims.AddRange(await userManager.GetClaimsAsync(user));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                var userRole = await roleManager.FindByNameAsync(role);
                var roleClaims = await roleManager.GetClaimsAsync(userRole);

                foreach (var roleClaim in roleClaims)
                {
                    claims.Add(new Claim(roleClaim.Type, roleClaim.Value));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken
            (
                 jwtSettings.ValidIssuer,
                 jwtSettings.ValidAudience,
                 claims,
                 expires: DateTime.UtcNow.AddDays(7),
                 signingCredentials: signIn
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = tokenHandler.WriteToken(token);
            this.responseResult.Success = true;

            this.responseResult.Entity = tokenKey;
            return responseResult;

        }
    }
}


