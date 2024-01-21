
using ChatService.Core.Repositories;
using ChatService.Core.Repositories.EntitiesRepositories.GroupsRepositories;
using ChatService.Core.Repositories.EntitiesRepositories.RolesRepositories;
using ChatService.Core.Repositories.EntitiesRepositories.UsersRepositories;
using ChatService.Core.Services.GroupServices;
using ChatService.Core.Services.RolesServices;
using ChatService.Core.Services.UserServices;
using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Data.Abstractions;
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

    public static void AddRepositoriesServices(this IServiceCollection services)
    {
        services.AddSingleton<IBaseRepository<ChatDbContext>, BaseRepository<ChatDbContext>>();
        services.AddSingleton<IUnitOfWork<ChatDbContext>, UnitOfWork<ChatDbContext>>();

        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IGroupRepository, GroupRepository>();
        services.AddSingleton<IRoleRepository, RoleRepository>();
    }
}
