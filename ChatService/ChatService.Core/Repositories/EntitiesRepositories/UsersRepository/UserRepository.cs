using ChatService.Core.Helpers;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;
using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Repositories.EntitiesRepositories.UsersRepositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IBaseRepository<ChatDbContext> _baseRepository;

    public UserRepository(IBaseRepository<ChatDbContext> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<User> CreateAsync(User item)
    {
        return await _baseRepository.CreateAsync<User, UserKey>(item);
    }

    public async Task<bool> DeleteAsync(UserKey key)
    {
        return await _baseRepository.DeleteAsync<User, UserKey>(key);
    }

    public async Task<User?> GetAsync(UserKey key)
    {
        return await _baseRepository.GetAsync<User, UserKey>(key);
    }

    public async Task<List<Group>?> GetGroupsAsync(UserKey key)
    {
        var user = await GetAsync(key);
        if (Guards.IsNull(user))
        {
            throw new Exception("User not found");
        }
        return user.Groups;
    }

    public async Task<string> GetNameAsync(UserKey key)
    {
        var user = await GetAsync(key);
        if (Guards.IsNull(user))
        {
            throw new Exception("User not found");
        }
        return user.Key.Name;
    }

    public async Task<List<User>> ListAsync(bool complete)
    {
        return await _baseRepository.ListAsync<User, UserKey>(complete);
    }

    public async Task<bool> UpdateAsync(User item)
    {
        return await _baseRepository.Update<User, UserKey>(item);
    }
}

