
using ChatService.Api;
using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Users;
using ChatService.Core.Helpers;
using ChatService.Domain.Models;
using ChatService.Infrastructure.Hubs.Notifications;
using ChatService.Infrastructure.Hubs.Notifications.Services;
using ChatService.Infrastructure.Utils;
using ChatService.Utils;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.EndpointFilters;

public class DisassociationUserToGroupEndpointFilter : IEndpointFilter
{
    public IHubContext<NotificationHub, INotificationHub> _notificationHubContext { get; }
    public readonly CoreServices _coreService;

    public DisassociationUserToGroupEndpointFilter(
        IHubContext<NotificationHub, INotificationHub> notificationHubContext,
        CoreServices coreService
        )
    {
        _notificationHubContext = notificationHubContext;
        _coreService = coreService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        string groupIdentifierStr = ContextUtils.GetRouteValueGuidAsString(context.HttpContext, "identifier");
        Guid userIdentifier = ContextUtils.GetRouteValueGuid(context.HttpContext, "userIdentifier");

        UserKeyDTO userKey = new(userIdentifier);

        string? userName = await _coreService.UserService.GetNameAsync(
                    userKey.ToDomainKey<UserKeyDTO, UserKey>()
                    );

        if (Guards.IsNull(userName))
        {
            throw new Exception();
        }
        userKey.Name = userName;

        Guards.IsNotNullObject(userKey);

        var result = await next(context);

        if (result is ResultDTO<bool> resultDto &&
            resultDto.StatusCode == System.Net.HttpStatusCode.OK &&
            resultDto.Data == true
            )
        {
            _ = _notificationHubContext.Clients.Group(groupIdentifierStr).LeftAsync(
                    new MessageDTO(
                        new UserDTO(userKey),
                        $"Has left the group",
                        DateTime.UtcNow)
                    );

        }
        return result;
    }
}