using ChatService.Api;
using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Users;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;
using ChatService.EndpointFilters;
using ChatService.Infrastructure.Utils;
using System.Net;

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
            async Task<ResultDTO<UserDTO>>
            (UserDTO user, CoreServices coreServices) =>
        {
            try
            {
                var res = await coreServices.UserService.CreateAsync(user.ToDomain<UserDTO, User>());
                var result = new ResultDTO<UserDTO>()
                {
                    Data = res == null ? null : res.ToDto<UserDTO, User>(),
                };

                if (ResultDTO<UserDTO>.HasData(result.Data))
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
            }
            catch (Exception ex)
            {
                return new ResultDTO<UserDTO>()
                {
                    Data = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            };
        })
        .UserConfig("CreateUser")
        .AddEndpointFilter<CreateEndpointFilter>();
    }

    private static void Delete(this WebApplication app)
    {
        app.MapDelete("/user/delete/{identifier}:guid",
            async Task<ResultDTO<bool>>
            (Guid identifier, CoreServices coreServices) =>
        {
            try
            {
                var res = await coreServices.UserService.DeleteAsync(new UserKeyDTO(identifier).ToDomainKey<UserKeyDTO, UserKey>());
                return new ResultDTO<bool>()
                {
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO<bool>()
                {
                    Data = false,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        })
        .UserConfig("DeleteUser");
    }

    private static void Get(this WebApplication app)
    {
        app.MapGet("/user/{identifier}:guid",
            async Task<ResultDTO<UserDTO>>
            (Guid identifier, CoreServices coreServices) =>
        {
            try
            {
                var res = await coreServices.UserService.GetAsync(new UserKeyDTO(identifier).ToDomainKey<UserKeyDTO, UserKey>());
                return new ResultDTO<UserDTO>()
                {
                    Data = res == null ? null : res.ToDto<UserDTO, User>()
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO<UserDTO>()
                {
                    Data = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        })
        .UserConfig("GetUser");
    }

    private static void GetGroups(this WebApplication app)
    {
        app.MapGet("/user/groups/{identifier}:guid",
            async Task<ResultDTO<List<GroupDTO>>>
            (Guid identifier, CoreServices coreServices) =>
        {
            try
            {
                var res = await coreServices.UserService.GetGroupsAsync(new UserKeyDTO(identifier).ToDomainKey<UserKeyDTO, UserKey>());
                return new ResultDTO<List<GroupDTO>>()
                {
                    Data = res == null ? null : res.ToDto<GroupDTO, Group>()
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO<List<GroupDTO>>()
                {
                    Data = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        })
        .UserConfig("GetUserGroups");
    }

    private static void GetName(this WebApplication app)
    {
        app.MapGet("/user/name/{identifier}:guid",
            async Task<ResultDTO<string>>
            (Guid identifier, CoreServices coreServices) =>
        {
            try
            {
                var res = await coreServices.UserService.GetNameAsync(new UserKeyDTO(identifier).ToDomainKey<UserKeyDTO, UserKey>());
                return new ResultDTO<string>()
                {
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO<string>()
                {
                    Data = string.Empty,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        })
        .UserConfig("GetUserName");
    }

    private static void List(this WebApplication app)
    {
        app.MapGet("/user/{complete}",
            async Task<ResultDTO<List<UserDTO>>>
            (bool complete, CoreServices coreServices) =>
        {
            try
            {
                var res = await coreServices.UserService.ListAsync(complete);
                return new ResultDTO<List<UserDTO>>()
                {
                    Data = res == null ? null : res.ToDto<UserDTO, User>()
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO<List<UserDTO>>()
                {
                    Data = new List<UserDTO>(),
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        })
        .UserConfig("ListUsers");
    }

    private static RouteHandlerBuilder UserConfig(this RouteHandlerBuilder route, string name)
    {
        return route.WithName(name)
                    .WithTags("Users")
                    .WithOpenApi();
    }
}

