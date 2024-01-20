using ChatService.Core.Services.GroupServices;
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

    private S GetService<S>()
    {
        var service = _serviceProvider.GetService(typeof(S));
        if(service == null)
        {
            throw new Exception();
        }
        return (S)service;
    }
}

