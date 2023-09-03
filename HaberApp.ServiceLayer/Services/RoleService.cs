using AutoMapper;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Models.Enums;
using HaberApp.Core.Services;
using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HaberApp.ServiceLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly ResponseResult<RoleResponseDto> responseResult;
        private readonly IMapper mapper;
        public RoleService(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.responseResult = new ResponseResult<RoleResponseDto>();
            this.mapper = mapper;
        }
        public async Task<ResponseResult<RoleResponseDto>> CreateRoleWithClaimsAsync(CreateRoleRequestDto requestDto)
        {

            var role = new AppRole()
            {
                Name = requestDto.RoleName,
                CreationDate = DateTime.UtcNow,
                RoleType = RoleTypes.CustomType
            };
            var roleResult = await roleManager.CreateAsync(role);
            if (!roleResult.Succeeded)
            {
                throw new CustomAppException(roleResult.Errors.Select(a => a.Description).ToList());
            }

            try
            {
                foreach (var item in requestDto.PermissionList)
                {
                    var claimResult = await roleManager.AddClaimAsync(role, new Claim("Permission", item.PermissionValue));

                    if (!claimResult.Succeeded)
                    {
                        throw new Exception(claimResult.Errors.FirstOrDefault()?.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                await roleManager.DeleteAsync(role);
                throw new CustomAppException(ex.Message);
            }
            this.responseResult.Entity = this.mapper.Map<RoleResponseDto>(role);
            return this.responseResult;

        }
    }
}
