using ChatService.Infrastructure.Hubs.Chat;
using ChatService.Infrastructure.Hubs.Notifications;
using Microsoft.AspNetCore.Builder;

namespace ChatService.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    ///  Configure the HTTP request
    /// </summary>
    /// <param name="app"><see cref="WebApplication"/></param>
    /// <remarks> 
    ///  Path to user conversation is "/chat";
    ///  Path to group conversation is "/chatGroup" 
    ///  Path to notification is "/notification" 
    /// </remarks>
    public static void UseChatService(this WebApplication app)
    {
        app.MapHub<ChatUserHub>("/chat");
        app.MapHub<ChatGroupHub>("/chatGroup");
        app.MapHub<NotificationHub>("/notification");
    }
}
