using ChatService.Api.DTOS;

namespace ChatService.Infrastructure.Hubs.Notifications.Services;
public interface INotificationHub
{
    Task JoinAsync(MessageDTO messageDTO);
    Task LeftAsync(MessageDTO messageDTO);
}
