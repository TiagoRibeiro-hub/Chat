using ChatService.Core.Abstractions;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;

namespace ChatService.Core.Services.GroupServices;
public interface IGroupService : IBaseService<Group, GroupKey>
{
    Task<User> AddUserAsync(GroupKey key, User user, GroupRolesTypes groupRolesType);
    Task<User> GetFounderAsync(GroupKey key);
    Task<List<User>?> GetUsersAsync(GroupKey key);
    Task<bool> RemoveUserAsync(GroupKey key, UserKey userKey);
}
