using ChatService.Api;
using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Messages;
using ChatService.Api.DTOS.Users;
using ChatService.Domain.Entities.Messages;
using ChatService.Infrastructure.Hubs.Notifications;
using Microsoft.AspNetCore.SignalR;
using System.Text;

namespace ChatService.EndpointFilters;
public class CreateEndpointFilter : IEndpointFilter
{
    public IHubContext<NotificationHub, INotificationHub> _notificationHubContext { get; }
    public readonly CoreServices _coreService;

    public CreateEndpointFilter(
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

        if (result is ResultDTO<UserDTO> userDto &&
            userDto.StatusCode == System.Net.HttpStatusCode.Created &&
            ResultDTO<UserDTO>.HasData(userDto.Data)
            )
        {
            _ = _notificationHubContext.Clients.All
                    .JoinAsync(new UserMessageDTO(userDto.Data.Key.Identifier, $"{userDto.Data.Key.Name} has join the chat.", DateTime.UtcNow));

        }
        else if (result is ResultDTO<GroupDTO> groupDto &&
            groupDto.StatusCode == System.Net.HttpStatusCode.Created &&
            ResultDTO<GroupDTO>.HasData(groupDto.Data) &&
            !groupDto.Data.IsPrivate
            )
        {
            var date = DateTime.UtcNow;

            StringBuilder message = new();
            message.Append(groupDto.Data.Founder.Key.Name).Append("has created the group");

            GroupMessages groupMessages = new(groupDto.Data.Key.Identifier, new()
            {
                new UserMessage(
                    groupDto.Data.Founder.Key.Identifier,
                    message.ToString(),
                    date
                    )
            });

            _ = _coreService.GroupMessageService.AddAsync(groupMessages);

            message.Append(" ").Append(groupDto.Data.Key.Name);

            _ = _notificationHubContext.Clients.All.JoinAsync(
                    new UserMessageDTO(
                        groupDto.Data.Founder.Key.Identifier,
                        message.ToString(),
                        date)
                    );

        }
        return result;
    }
}