
using ChatService.Api;
using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Messages;
using ChatService.Api.DTOS.Users;
using ChatService.Core.Helpers;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Messages;
using ChatService.Infrastructure.Hubs.Notifications;
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
        Guid groupIdentifier = ContextUtils.GetRouteValueGuid(context.HttpContext, "identifier");
        Guid userIdentifier = ContextUtils.GetRouteValueGuid(context.HttpContext, "userIdentifier");

        UserKeyDTO userKey = new(userIdentifier);

        string? userName = await _coreService.UserService.GetNameAsync(userKey.ToDomainKey<UserKeyDTO, UserKey>());

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
            GroupMessages? groupMessages = await _coreService.GroupMessageService.GetAsync(groupIdentifier);

            if (Guards.IsNull(groupMessages))
            {
                throw new Exception();
            }

            var message = $"{userName} has left the group";
            var date = DateTime.UtcNow;

            groupMessages.Messages.Add(new UserMessage(userIdentifier, message, date));

            _ = _coreService.GroupMessageService.AddAsync(groupMessages);

            _ = _notificationHubContext.Clients
                    .Group(groupIdentifier.ToString())
                    .LeftAsync(new UserMessageDTO(userKey.Identifier, message, date));

        }
        return result;
    }
}