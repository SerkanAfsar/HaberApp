using AutoMapper;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Models.Enums;
using HaberApp.Core.Services;
using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ResponseResult<RoleResponseDto>> CreateRoleWithClaimsAsync(CreateRoleRequestDto requestDto, CancellationToken cancellationToken = default)
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

        public async Task<ResponseResult<RoleResponseDto>> DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default)
        {
            var role = await this.roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                throw new NotFoundException($"Role with {roleId} not Found");
            }
            var identityResult = await roleManager.DeleteAsync(role);
            if (!identityResult.Succeeded)
            {
                throw new CustomAppException(identityResult.Errors.Select(a => a.Description).ToList());
            }
            this.responseResult.Entity = this.mapper.Map<RoleResponseDto>(role);
            return this.responseResult;

        }

        public async Task<ResponseResult<RoleResponseDto>> GetRoleAsync(string roleId, CancellationToken cancellationToken = default)
        {
            var role = await this.roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                throw new NotFoundException($"Role with {roleId} not Found");
            }
            var claims = await roleManager.GetClaimsAsync(role);
            var roleResponseDto = new RoleResponseDto()
            {
                RoleId = role.Id,
                RoleName = role.Name,
                PermissionList = claims.ToList().Select(a => new PermissionList()
                {
                    PermissionValue = a.Value
                }).ToList()
            };

            this.responseResult.Entity = roleResponseDto;
            return this.responseResult;
        }

        public async Task<ResponseResult<RoleResponseDto>> GetRolesAsync(CancellationToken cancellationToken = default)
        {
            this.responseResult.Entities = this.mapper.Map<List<RoleResponseDto>>(await roleManager.Roles.ToListAsync(cancellationToken));
            return responseResult;
        }

        public async Task<ResponseResult<RoleResponseDto>> UpdateRoleWithClaimsAsync(string roleId, CreateRoleRequestDto requestDto, CancellationToken cancellationToken = default)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                throw new NotFoundException($"Role with {roleId} not Found");
            }
            var allClaims = await roleManager.GetClaimsAsync(role);

            foreach (var item in allClaims)
            {
                await roleManager.RemoveClaimAsync(role, item);

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
