using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;
using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Data.Abstractions;
using ChatService.Core.Repositories.EntitiesRepositories.GroupsRepositories;
using ChatService.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Core.Services.GroupServices;

internal sealed class GroupService : IGroupService
{
    private readonly IBaseRepository<ChatDbContext> _baseRepository;
    private readonly IGroupRepository _groupRepository;

    public GroupService(
        IBaseRepository<ChatDbContext> baseRepository,
        IGroupRepository groupRepository
        )
    {
        _baseRepository = baseRepository;
        _groupRepository = groupRepository;
    }

    public async Task<User> AddUserAsync(GroupKey key, User user, GroupRolesTypes groupRolesType)
    {
        if (groupRolesType == GroupRolesTypes.Founder)
        {
            throw new Exception();
        }
        return await _groupRepository.AddUserAsync(key, user, groupRolesType);
    }

    public async Task<Group> CreateAsync(Group item)
    {
        Guards.IsNotNullOrEmptyGuid(item.Founder);

        var hasFounder = item.Users?.Any(x => x.Identifier == item.Founder);
        if (hasFounder != true)
        {
            item.Users = item.Users ?? new();

            User? user = await _baseRepository.UnitOfWork.Context.Set<User>().FirstOrDefaultAsync(x => x.Key.Identifier == item.Founder);
            if (Guards.IsNull(user))
            {
                throw new Exception("Founder not found");
            }

            item.Users.Add(user.Key);
        }
        return await _baseRepository.CreateAsync<Group, GroupKey>(item);
    }

    public async Task<bool> DeleteAsync(GroupKey key)
    {
        return await _baseRepository.DeleteAsync<Group, GroupKey>(key);
    }

    public async Task<Group?> GetAsync(GroupKey key)
    {
        return await _baseRepository.GetAsync<Group, GroupKey>(key);
    }

    public async Task<User> GetFounderAsync(GroupKey key)
    {
        var group = await GetAsync(key);
        if (Guards.IsNull(group))
        {
            throw new Exception("Group not found");
        }
        User? user = await _baseRepository.UnitOfWork.Context.Set<User>().FirstOrDefaultAsync(x => x.Key.Identifier == group.Founder);
        if (Guards.IsNull(user))
        {
            throw new Exception("Founder not found");
        }
        return user;
    }

    public async Task<string> GetNameAsync(GroupKey key)
    {
        var group = await GetAsync(key);
        if (Guards.IsNull(group))
        {
            throw new Exception("Group not found");
        }
        return group.Key.Name;
    }

    public async Task<List<User>?> GetUsersAsync(GroupKey key)
    {
        var group = await GetAsync(key);
        if (Guards.IsNull(group))
        {
            throw new Exception("Group not found");
        }
        return await _baseRepository.UnitOfWork.Context.Set<User>().ToListAsync();
    }

    public async Task<List<Group>> ListAsync(bool complete)
    {
        return await _baseRepository.ListAsync<Group, GroupKey>(complete);
    }

    public async Task<bool> RemoveUserAsync(GroupKey key, UserKey userKey)
    {
        User? user = await _baseRepository.UnitOfWork.Context.Set<User>().FirstOrDefaultAsync(x => x.Key.Identifier == userKey.Identifier);
        if (Guards.IsNull(user) || !Guards.IsNotNullOrEmptyCollection(user.Groups))
        {
            throw new Exception("User not found");
        }
        
        if (user.Groups.Any(x => x.Founder == userKey.Identifier))
        {
            throw new Exception("Founder can not be removed");
        }

        return await _groupRepository.RemoveUserAsync(key, userKey);
    }

    public async Task<bool> UpdateAsync(Group item)
    {
        return await _baseRepository.Update<Group, GroupKey>(item);
    }

    public Task<List<GroupRoles>> UpdateRoleAsync(List<GroupRoles> groupRoles)
    {
        throw new NotImplementedException();
    }
}
