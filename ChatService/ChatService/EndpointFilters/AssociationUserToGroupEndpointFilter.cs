
using ChatService.Api;
using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Messages;
using ChatService.Api.DTOS.Users;
using ChatService.Core.Helpers;
using ChatService.Domain.Entities.Messages;
using ChatService.Infrastructure.Hubs.Notifications;
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

        Guid groupIdentifier = ContextUtils.GetRouteValueGuid(context.HttpContext, "identifier");

        if (result is ResultDTO<UserDTO> userDto &&
            userDto.StatusCode == System.Net.HttpStatusCode.OK &&
            ResultDTO<UserDTO>.HasData(userDto.Data)
            )
        {
            GroupMessages? groupMessages = await _coreService.GroupMessageService.GetAsync(groupIdentifier);

            if (Guards.IsNull(groupMessages))
            {
                throw new Exception();
            }

            var message = $"{userDto.Data.Key.Name} has join the group";
            var date = DateTime.UtcNow;

            groupMessages.Messages.Add(new UserMessage(userDto.Data.Key.Identifier, message, date));

            _ = _coreService.GroupMessageService.AddAsync(groupMessages);

            await _notificationHubContext.Clients
                    .Group(groupIdentifier.ToString())
                    .JoinAsync(new UserMessageDTO(userDto.Data.Key.Identifier, message, date));
        }

        return result;
    }
}
