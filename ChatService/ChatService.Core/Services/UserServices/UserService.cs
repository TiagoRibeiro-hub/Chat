using ChatService.Core.Helpers;
using ChatService.Core.Repositories.EntitiesRepositories.UsersRepositories;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;
using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Services.UserServices;

internal sealed class UserService : ServiceBase, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IBaseRepository<ChatDbContext> baseRepository,
        IUserRepository userRepository) : base(baseRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateAsync(User item)
    {
        Guards.IsNotNullOrEmptyGuid(item.Key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(item.Key.Identifier)));
        return await _baseRepository.CreateAsync<User, UserKey>(item);
    }

    public async Task<bool> DeleteAsync(UserKey key)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));
        return await _baseRepository.DeleteAsync<User, UserKey>(key);
    }

    public async Task<User?> GetAsync(UserKey key)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));
        return await _baseRepository.GetAsync<User, UserKey>(key);
    }

    public async Task<List<Group>?> GetGroupsAsync(UserKey key)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));
        return await _userRepository.GetGroupsAsync(key);
    }

    public async Task<string> GetNameAsync(UserKey key)
    {
        var user = await GetAsync(key);
        if (Guards.IsNull(user))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(User)));
        }
        return user.Key.Name;
    }

    public async Task<List<User>> ListAsync(bool complete)
    {
        return await _baseRepository.ListAsync<User, UserKey>(complete);
    }

    public async Task<bool> UpdateAsync(User item)
    {
        Guards.IsNotNullOrEmptyGuid(item.Key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(item.Key.Identifier)));
        return await _baseRepository.Update<User, UserKey>(item);
    }
}
