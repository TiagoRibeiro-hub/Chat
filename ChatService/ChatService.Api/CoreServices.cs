using ChatService.Core.Services.GroupServices;
using ChatService.Core.Services.MessageServices;
using ChatService.Core.Services.RolesServices;
using ChatService.Core.Services.UserServices;

namespace ChatService.Api;
public sealed class CoreServices
{
    private readonly IServiceProvider _serviceProvider;

    public CoreServices(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IUserService UserService
    {
        get
        {
            return GetService<IUserService>();
        }
    }

    public IGroupService GroupService
    {
        get
        {
            return GetService<IGroupService>();
        }
    }

    public IRolesService RolesService
    {
        get
        {
            return GetService<IRolesService>();
        }
    }

    public IUserMessageService UserMessageService
    {
        get
        {
            return GetService<IUserMessageService>();
        }
    }

    public IGroupMessageService GroupMessageService
    {
        get
        {
            return GetService<IGroupMessageService>();
        }
    }

    private S GetService<S>()
    {
        Type type = typeof(S);

        if (!type.IsInterface)
        {
            throw new Exception();
        }

        var service = _serviceProvider.GetService(type);
        if (service == null)
        {
            throw new Exception();
        }
        return (S)service;
    }
}

