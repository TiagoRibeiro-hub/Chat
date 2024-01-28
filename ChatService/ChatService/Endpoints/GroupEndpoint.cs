﻿using ChatService.Api;
using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Users;
using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;
using ChatService.EndpointFilters;
using ChatService.Infrastructure.Utils;
using System.Net;

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
        app.RemoveUser();
    }

    private static void AddUser(this WebApplication app)
    {
        app.MapPut("/group/user/{identifier}:guid/{groupRolesType}",
            async Task<ResultDTO<UserDTO>>
            (Guid identifier, GroupRolesTypesDTO groupRolesType, UserDTO user, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.AddUserAsync(
                new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>(),
                user.ToDomain<UserDTO, User>(),
                (GroupRolesTypes)groupRolesType
                );

            return new ResultDTO<UserDTO>()
            {
                Data = res == null ? null : res.ToDto<UserDTO, User>()
            };
        })
        .GroupConfig("AddUser")
        .AddEndpointFilter<AssociationUserToGroupEndpointFilter>();
    }

    private static void Create(this WebApplication app)
    {
        app.MapPost("/group/create",
            async Task<ResultDTO<GroupDTO>>
            (GroupDTO group, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.CreateAsync(group.ToDomain<GroupDTO, Group>());
            var result = new ResultDTO<GroupDTO>()
            {
                Data = res == null ? null : res.ToDto<GroupDTO, Group>(),
            };

            if (ResultDTO<GroupDTO>.HasData(result.Data))
            {
                result.Message = $"/user/create/{result.Data.Key.Identifier}";
                result.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                result.Message = "Failed to create";
                result.StatusCode = HttpStatusCode.BadRequest;
            }

            return result;
        })
        .GroupConfig("CreateGroup")
        .AddEndpointFilter<CreateEndpointFilter>();
    }

    private static void Delete(this WebApplication app)
    {
        app.MapDelete("/group/delete/{identifier}:guid",
            async Task<ResultDTO<bool>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.DeleteAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
            return new ResultDTO<bool>()
            {
                Data = res
            };
        })
        .GroupConfig("DeleteGroup");
    }

    private static void Get(this WebApplication app)
    {
        app.MapGet("/group/{identifier}:guid",
            async Task<ResultDTO<GroupDTO>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.GetAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
            return new ResultDTO<GroupDTO>()
            {
                Data = res == null ? null : res.ToDto<GroupDTO, Group>()
            };
        })
        .GroupConfig("GetGroup");
    }

    private static void GetFounder(this WebApplication app)
    {
        app.MapGet("/group/founder/{identifier}:guid",
            async Task<ResultDTO<UserDTO>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.GetFounderAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
            return new ResultDTO<UserDTO>()
            {
                Data = res == null ? null : res.ToDto<UserDTO, User>()
            };
        })
        .GroupConfig("GetGroupFounder");
    }

    private static void GetName(this WebApplication app)
    {
        app.MapGet("/group/name/{identifier}:guid",
            async Task<ResultDTO<string>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.GetNameAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
            return new ResultDTO<string>()
            {
                Data = res
            };
        })
        .GroupConfig("GetGroupName");
    }

    private static void GetUsers(this WebApplication app)
    {
        app.MapGet("/group/users/{identifier}:guid",
            async Task<ResultDTO<List<UserDTO>>>
            (Guid identifier, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.GetUsersAsync(new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>());
            return new ResultDTO<List<UserDTO>>()
            {
                Data = res == null ? null : res.ToDto<UserDTO, User>()
            };
        })
        .GroupConfig("GetGroupUsers");
    }

    private static void List(this WebApplication app)
    {
        app.MapGet("/group",
            async Task<ResultDTO<List<GroupDTO>>>
            (bool complete, CoreServices coreServices) =>
        {
            var res = await coreServices.GroupService.ListAsync(complete);
            return new ResultDTO<List<GroupDTO>>()
            {
                Data = res == null ? null : res.ToDto<GroupDTO, Group>()
            };
        })
        .GroupConfig("ListGroups");
    }

    private static void RemoveUser(this WebApplication app)
    {
        app.MapDelete("/group/user/{identifier}:guid/{userIdentifier}:guid",
            async Task<ResultDTO<bool>>
            (Guid identifier, Guid userIdentifier, CoreServices coreServices) =>
            {
                var res = await coreServices.GroupService.RemoveUserAsync(
                    new GroupKeyDTO(identifier).ToDomainKey<GroupKeyDTO, GroupKey>(),
                    new UserKeyDTO(userIdentifier).ToDomainKey<UserKeyDTO, UserKey>()
                    );

                return new ResultDTO<bool>()
                {
                    Data = res
                };
            })
        .GroupConfig("RemoveUser")
        .AddEndpointFilter<DisassociationUserToGroupEndpointFilter>();
    }

    private static RouteHandlerBuilder GroupConfig(this RouteHandlerBuilder route, string name)
    {
        return route.WithName(name)
                    .WithTags("Groups")
                    .WithOpenApi();
    }
}

