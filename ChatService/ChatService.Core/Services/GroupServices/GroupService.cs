using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;

namespace ChatService.Core.Services.GroupServices;

internal sealed class GroupService : IGroupService
{
    public Task<User> AddUserAsync(GroupKey key, User user, GroupRolesTypes groupRolesType)
    {
        throw new NotImplementedException();
    }

    public Task<Group> CreateAsync(Group item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<Group?> GetAsync(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetFounderAsync(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetNameAsync(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>?> GetUsersAsync(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<Group>> ListAsync(bool complete)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveUserAsync(GroupKey key, UserKey userKey)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Group item)
    {
        throw new NotImplementedException();
    }

    public Task<List<GroupRoles>> UpdateRoleAsync(List<GroupRoles> groupRoles)
    {
        throw new NotImplementedException();
    }
}
