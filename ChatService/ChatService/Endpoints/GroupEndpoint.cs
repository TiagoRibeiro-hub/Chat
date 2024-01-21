using ChatService.Api;
using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Users;
using ChatService.Api.Utils;
using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatService.Endpoints;
public static class GroupEndpoint
{
    public static void MapGroupEndpoints(this WebApplication app)
    {
        app.AddUser();
        app.Create();
        app.Delete();
        app.Get();
        app.GetFounder();
        app.GetName();
        app.GetUsers();
        app.List();
    }

    private static void AddUser(this WebApplication app)
    {
        app.MapPut("/group/user/{identifier}",
            async Task<Ok<UserDTO>>
            (Guid identifier, UserDTO user, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.AddUserAsync(
                new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>(),
                user.ToDomain<UserDTO, User>()
                );

            var dto = res.ToDto<UserDTO, User>();
            return TypedResults.Ok(dto);
        })
        .GroupConfig("AddUser");
    }

    private static void Create(this WebApplication app)
    {
        app.MapPost("/group/create",
            async Task<Created<GroupDTO>>
            (GroupDTO group, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.CreateAsync(group.ToDomain<GroupDTO, Group>());
            var dto = res.ToDto<GroupDTO, Group>();
            return TypedResults.Created($"/user/create/{dto.Key.Identifier}", dto);
        })
        .GroupConfig("CreateGroup");
    }

    private static void Delete(this WebApplication app)
    {
        app.MapDelete("/group/delete/{identifier}",
            async Task<Results<Ok, BadRequest>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.DeleteAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
            return res
            ? TypedResults.Ok()
            : TypedResults.BadRequest();
        })
        .GroupConfig("DeleteGroup");
    }

    private static void Get(this WebApplication app)
    {
        app.MapGet("/group/{identifier}",
            async Task<Ok<GroupDTO>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.GetAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
            var dto = res.ToDto<GroupDTO, Group>();
            return TypedResults.Ok(dto);
        })
        .GroupConfig("GetGroup");
    }

    private static void GetFounder(this WebApplication app)
    {
        app.MapGet("/group/founder/{identifier}",
            async Task<Ok<UserDTO>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.GetFounderAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
            var dto = res.ToDto<UserDTO, User>();
            return TypedResults.Ok(dto);
        })
        .GroupConfig("GetGroupFounder");
    }

    private static void GetName(this WebApplication app)
    {
        app.MapGet("/group/name/{identifier}",
            async Task<Ok<string>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.GetNameAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
            return TypedResults.Ok(res);
        })
        .GroupConfig("GetGroupName");
    }

    private static void GetUsers(this WebApplication app)
    {
        app.MapGet("/group/users/{identifier}",
            async Task<Ok<List<UserDTO>>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.GetUsersAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
            var dtos = res.ToDto<UserDTO, User>();
            return TypedResults.Ok(dtos);
        })
        .GroupConfig("GetGroupUsers");
    }

    private static void List(this WebApplication app)
    {
        app.MapGet("/group",
            async Task<Ok<List<GroupDTO>>>
            (bool complete, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.ListAsync(complete);
            var dtos = res.ToDto<GroupDTO, Group>();
            return TypedResults.Ok(dtos);
        })
        .GroupConfig("ListGroups");
    }

    private static void GroupConfig(this RouteHandlerBuilder route, string name)
    {
        route.WithName(name)
            .WithTags("Groups")
            .WithOpenApi();
    }
}

