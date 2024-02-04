using ChatService.Core.Services.GroupServices;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;

namespace ChatService.Core.Repositories.EntitiesRepositories.GroupsRepositories;

public interface IGroupRepository
{
    Task<User> AddUserAsync(GroupKey key, User user, GroupRolesTypes groupRolesType);

    Task<bool> RemoveUserAsync(GroupKey key, UserKey userKey);
}

