using ChatService.Api;
using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Users;
using ChatService.Api.Utils;
using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
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
            async Task<Ok<List<GroupRolesDTO>>>
            (Guid identifier, CoreServices coreServices) =>
            {
                var res = await coreServices.RolesService.GetUserRolesAsync(new UserKeyDTO(identifier).ToDomainKey<UserKeyDTO, UserKey>());
                var dtos = res.ToDto<GroupRolesDTO, GroupRoles>();
                return TypedResults.Ok(dtos);
            })
        .RolesConfig("GetUserRoles");
    }

    private static void GetGroupRoles(this WebApplication app)
    {
        app.MapGet("/roles/group/{identifier}",
            async Task<Ok<List<GroupRolesDTO>>>
            (Guid identifier, CoreServices coreServices) =>
            {
                var res = await coreServices.RolesService.GetGroupRolesAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
                var dtos = res.ToDto<GroupRolesDTO, GroupRoles>();
                return TypedResults.Ok(dtos);
            })
        .RolesConfig("GetGroupRoles");
    }

    private static void UpdateGroupRole(this WebApplication app)
    {
        app.MapPut("/roles/group/{identifier}",
            async Task<Ok<List<GroupRolesDTO>>>
            (Guid identifier, List<GroupRolesDTO> groupRoles, CoreServices coreServices) =>
            {
                var res = await coreServices.RolesService.UpdateGroupRoleAsync(
                    new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>(),
                    groupRoles.ToDomain<GroupRolesDTO, GroupRoles>()
                    );
                var dtos = res.ToDto<GroupRolesDTO, GroupRoles>();
                return TypedResults.Ok(dtos);

            })
        .RolesConfig("UpdateGroupRole");
    }

    private static void RolesConfig(this RouteHandlerBuilder route, string name)
    {
        route.WithName(name)
            .WithTags("Roles")
            .WithOpenApi();
    }
}

