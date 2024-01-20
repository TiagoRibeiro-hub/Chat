using ChatService.Infrastructure.Hubs.Chat;
using Microsoft.AspNetCore.Builder;

namespace ChatService.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    ///  Configure the HTTP request
    /// </summary>
    /// <param name="app"><see cref="WebApplication"/></param>
    /// <remarks> 
    ///  Path to user conversation is "/Chat";
    ///  Path to group conversation is "/ChatGroup" 
    /// </remarks>
    public static void UseChatService(this WebApplication app)
    {
        app.MapHub<ChatUserHub>("/Chat");
        app.MapHub<ChatGroupHub>("/ChatGroup");
    }
}
