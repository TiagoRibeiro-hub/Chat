using ChatService.Api;
using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Users;
using ChatService.Api.Utils;
using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatService.Endpoints;
public static class UserEndpoint
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.Create();
        app.Delete();
        app.Get();
        app.GetGroups();
        app.GetName();
        app.List();
    }
    private static void Create(this WebApplication app)
    {
        app.MapPost("/user/create",
            async Task<Created<UserDTO>>
            (UserDTO user, CoreServices coreServices) =>
        {
            var res = await coreServices.UserService.Create(user.ToDomain<UserDTO, User>());
            var dto = res.ToDto<UserDTO, User>();
            return TypedResults.Created($"/user/create/{dto.Key.Identifier}", dto);
        })
        .UserConfig("CreateUser");
    }

    private static void Delete(this WebApplication app)
    {
        app.MapDelete("/user/delete/{identifier}",
            async Task<Results<Ok, BadRequest>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.UserService.Delete(new UserKeyDTO(identifier).ToDomainKey<UserKeyDTO, UserKey>());

            return res
            ? TypedResults.Ok()
            : TypedResults.BadRequest();
        })
        .UserConfig("DeleteUser");
    }

    private static void Get(this WebApplication app)
    {
        app.MapGet("/user/{identifier}",
            async Task<Ok<UserDTO>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.UserService.Get(new UserKeyDTO(identifier).ToDomainKey<UserKeyDTO, UserKey>());
            var dto = res.ToDto<UserDTO, User>();
            return TypedResults.Ok(dto);
        })
        .UserConfig("GetUser");
    }

    private static void GetGroups(this WebApplication app)
    {
        app.MapGet("/user/groups/{identifier}",
            async Task<Ok<List<GroupDTO>>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.UserService.GetGroups(new UserKeyDTO(identifier).ToDomainKey<UserKeyDTO, UserKey>());
            var dtos = res.ToDto<GroupDTO, Group>();
            return TypedResults.Ok(dtos);
        })
        .UserConfig("GetUserGroups");
    }

    private static void GetName(this WebApplication app)
    {
        app.MapGet("/user/name/{identifier}",
            async Task<Ok<string>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.UserService.GetName(new UserKeyDTO(identifier).ToDomainKey<UserKeyDTO, UserKey>());
            return TypedResults.Ok(res);
        })
        .UserConfig("GetUserName");
    }

    private static void List(this WebApplication app)
    {
        app.MapGet("/user/{complete}",
            async Task<Ok<List<UserDTO>>>
            (bool complete, CoreServices coreServices) =>
        {
            var res = await coreServices.UserService.List(complete);
            var dtos = res.ToDto<UserDTO, User>();
            return TypedResults.Ok(dtos);
        })
        .UserConfig("ListUsers");
    }

    private static void UserConfig(this RouteHandlerBuilder route, string name)
    {
        route.WithName(name)
            .WithTags("Users")
            .WithOpenApi();
    }
}

