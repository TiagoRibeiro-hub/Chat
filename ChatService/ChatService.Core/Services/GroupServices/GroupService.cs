using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;

namespace ChatService.Core.Services.GroupServices;

internal sealed class GroupService : IGroupService
{
    public Task<User> AddUser(GroupKey key, User user)
    {
        throw new NotImplementedException();
    }

    public Task<Group> Create(Group item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<Group> Get(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetFounder(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetName(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetUsers(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<Group>> List(bool complete)
    {
        throw new NotImplementedException();
    }

    public Task<List<GroupRoles>> UpdateRole(List<GroupRoles> groupRoles)
    {
        throw new NotImplementedException();
    }
}
