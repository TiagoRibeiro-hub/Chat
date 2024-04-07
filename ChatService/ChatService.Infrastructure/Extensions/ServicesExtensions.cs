using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Messages;
using ChatService.Api.DTOS.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ChatService.Infrastructure.Extensions;

public static class ServicesExtensions
{
    /// <summary>
    /// Add SignalR and services that are necessary for the chat service
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    public static void AddChatServices(this IServiceCollection services)
    {
        services.AddSignalR(options =>
        {
            options.DisableImplicitFromServicesParameters = true;
        });

        services.AddSingleton<IDictionary<string, UserDTO>>(opt => new Dictionary<string, UserDTO>());
        services.AddSingleton<IDictionary<string, GroupUserDTO>>(opt => new Dictionary<string, GroupUserDTO>());

        services.AddSingleton<IDictionary<Guid, List<UserMessageDTO>>>(opt => new Dictionary<Guid, List<UserMessageDTO>>());
    }
}
