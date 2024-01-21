using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;
using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Repositories.EntitiesRepositories.GroupsRepositories;

public sealed class GroupRepository : IGroupRepository
{
    private readonly IBaseRepository<ChatDbContext> _baseRepository;

    public GroupRepository(IBaseRepository<ChatDbContext> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public Task<User> AddUserAsync(GroupKey key, User user)
    {
        throw new NotImplementedException();
    }

    public async Task<Group> CreateAsync(Group item)
    {
        return await _baseRepository.CreateAsync<Group, GroupKey>(item);
    }

    public async Task<bool> DeleteAsync(GroupKey key)
    {
        return await _baseRepository.DeleteAsync<Group, GroupKey>(key);
    }

    public async Task<Group> GetAsync(GroupKey key)
    {
        return await _baseRepository.GetAsync<Group, GroupKey>(key);
    }

    public async Task<User> GetFounderAsync(GroupKey key)
    {
        var group = await GetAsync(key);
        return group.Founder;
    }

    public async Task<string?> GetNameAsync(GroupKey key)
    {
        var group = await GetAsync(key);
        return group.Key.Name;
    }

    public async Task<List<User>?> GetUsersAsync(GroupKey key)
    {
        var group = await GetAsync(key);
        return group.Users;
    }

    public async Task<List<Group>> ListAsync(bool complete)
    {
        return await _baseRepository.ListAsync<Group, GroupKey>(complete);
    }

    public async Task<bool> UpdateAsync(Group item)
    {
        return await _baseRepository.Update<Group, GroupKey>(item);
    }
}

