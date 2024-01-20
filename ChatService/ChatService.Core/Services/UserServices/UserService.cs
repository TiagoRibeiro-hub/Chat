using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;

namespace ChatService.Core.Services.UserServices;

internal sealed class UserService : IUserService
{
    public Task<User> Create(User item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<User> Get(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<Group>> GetGroups(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetName(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<GroupRoles>> GetRoles(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> List(bool complete)
    {
        throw new NotImplementedException();
    }
}
