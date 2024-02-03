using ChatService.Core.Repositories.EntitiesRepositories.UsersRepositories;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;

namespace ChatService.Core.Services.UserServices;

internal sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User> CreateAsync(User item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetAsync(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<Group>?> GetGroupsAsync(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetNameAsync(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> ListAsync(bool complete)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(User item)
    {
        throw new NotImplementedException();
    }
}
