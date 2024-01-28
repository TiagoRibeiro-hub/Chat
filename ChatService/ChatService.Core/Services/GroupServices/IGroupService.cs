using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Services.GroupServices;
public interface IGroupService : IBaseService<Group, GroupKey>
{
    Task<User> AddUserAsync(GroupKey key, User user, GroupRolesTypes groupRolesType);
    Task<User> GetFounderAsync(GroupKey key);
    Task<List<User>?> GetUsersAsync(GroupKey key);
    Task<bool> RemoveUserAsync(GroupKey key, UserKey userKey);
}
