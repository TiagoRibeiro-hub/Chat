
using ChatService.Core.Services.GroupServices;
using ChatService.Core.Services.RolesServices;
using ChatService.Core.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;

namespace ChatService.Core.Services;

public static class ServicesExtensions
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<IRolesService, RolesService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IGroupService, GroupService>();
    }
}
