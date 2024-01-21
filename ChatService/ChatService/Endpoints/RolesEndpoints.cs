using ChatService.Api;
using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Users;
using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Infrastructure.Utils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatService.Endpoints;
public static class RolesEndpoints
{
    public static void MapRolesEndpoints(this WebApplication app)
    {
        app.GetUserRoles();
        app.GetGroupRoles();
        app.UpdateGroupRole();
    }
    private static void GetUserRoles(this WebApplication app)
    {
        app.MapGet("/roles/user/{identifier}",
            async Task<ResultDTO<List<GroupRolesDTO>>>
            (Guid identifier, CoreServices coreServices) =>
            {
                var res = await coreServices.RolesService.GetUserRolesAsync(new UserKeyDTO(identifier).ToDomainKey<UserKeyDTO, UserKey>());
                return new ResultDTO<List<GroupRolesDTO>>()
                {
                    Data = res == null ? null : res.ToDto<GroupRolesDTO, GroupRoles>()
                };
            })
        .RolesConfig("GetUserRoles");
    }

    private static void GetGroupRoles(this WebApplication app)
    {
        app.MapGet("/roles/group/{identifier}",
            async Task<ResultDTO<List<GroupRolesDTO>>>
            (Guid identifier, CoreServices coreServices) =>
            {
                var res = await coreServices.RolesService.GetGroupRolesAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
                return new ResultDTO<List<GroupRolesDTO>>()
                {
                    Data = res == null ? null : res.ToDto<GroupRolesDTO, GroupRoles>()
                };
            })
        .RolesConfig("GetGroupRoles");
    }

    private static void UpdateGroupRole(this WebApplication app)
    {
        app.MapPut("/roles/group/{identifier}",
            async Task<ResultDTO<List<GroupRolesDTO>>>
            (Guid identifier, List<GroupRolesDTO> groupRoles, CoreServices coreServices) =>
            {
                var res = await coreServices.RolesService.UpdateGroupRoleAsync(
                    new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>(),
                    groupRoles.ToDomain<GroupRolesDTO, GroupRoles>()
                    );
                return new ResultDTO<List<GroupRolesDTO>>()
                {
                    Data = res == null ? null : res.ToDto<GroupRolesDTO, GroupRoles>()
                };
            })
        .RolesConfig("UpdateGroupRole");
    }

    private static RouteHandlerBuilder RolesConfig(this RouteHandlerBuilder route, string name)
    {
        return route.WithName(name)
                    .WithTags("Roles")
                    .WithOpenApi();
    }
}

