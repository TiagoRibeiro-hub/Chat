using ChatService.Infrastructure.Hubs.Notifications.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Infrastructure.Hubs.Notifications;

public sealed class NotificationHub : Hub<INotificationHub>
{

}