using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Users;
using ChatService.Infrastructure.Hubs.Notifications;
using ChatService.Infrastructure.Hubs.Notifications.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.EndpointFilters;
public class CreateEndpointFilter : IEndpointFilter
{
    public IHubContext<NotificationHub, INotificationHub> _notificationHubContext { get; }

    public CreateEndpointFilter(IHubContext<NotificationHub, INotificationHub> notificationHubContext)
    {
        _notificationHubContext = notificationHubContext;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        if (result is ResultDTO<UserDTO> userDto &&
            userDto.StatusCode == System.Net.HttpStatusCode.Created &&
            ResultDTO<UserDTO>.HasData(userDto.Data)
            )
        {
            _ = _notificationHubContext.Clients.All.JoinAsync(
                    new MessageDTO(
                        userDto.Data,
                        $"{userDto.Data.Key.Name} has join the chat.",
                        DateTime.UtcNow)
                    );

        }
        else if (result is ResultDTO<GroupDTO> groupDto &&
            groupDto.StatusCode == System.Net.HttpStatusCode.Created &&
            ResultDTO<GroupDTO>.HasData(groupDto.Data) &&
            !groupDto.Data.IsPrivate
            )
        {
            _ = _notificationHubContext.Clients.All.JoinAsync(
                    new MessageDTO(
                        groupDto.Data.Founder,
                        $"{groupDto.Data.Founder.Key.Name} has created the group {groupDto.Data.Key.Name}",
                        DateTime.UtcNow)
                    );

        }
        return result;
    }
}