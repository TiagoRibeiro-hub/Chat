using ChatService.Api.DTOS.Messages;

namespace ChatService.Infrastructure.Hubs.Notifications;
public interface INotificationHub
{
    Task JoinAsync(UserMessageDTO messageDTO);
    Task LeftAsync(UserMessageDTO messageDTO);
}
