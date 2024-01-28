
using ChatService.Api;
using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Users;
using ChatService.Infrastructure.Hubs.Notifications;
using ChatService.Infrastructure.Hubs.Notifications.Services;
using ChatService.Utils;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.EndpointFilters;

public class AssociationUserToGroupEndpointFilter : IEndpointFilter
{
    public IHubContext<NotificationHub, INotificationHub> _notificationHubContext { get; }
    public readonly CoreServices _coreService;

    public AssociationUserToGroupEndpointFilter(
        IHubContext<NotificationHub, INotificationHub> notificationHubContext,
        CoreServices coreService
        )
    {
        _notificationHubContext = notificationHubContext;
        _coreService = coreService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        string groupIdentifierStr = ContextUtils.GetRouteValueGuidAsString(context.HttpContext, "identifier");

        if (result is ResultDTO<UserDTO> userDto &&
            userDto.StatusCode == System.Net.HttpStatusCode.OK &&
            ResultDTO<UserDTO>.HasData(userDto.Data)
            )
        {
            _ = _notificationHubContext.Clients.Group(groupIdentifierStr).JoinAsync(
                    new MessageDTO(
                        userDto.Data,
                        $"Has join the group",
                        DateTime.UtcNow)
                    );

        }

        return result;
    }
}
