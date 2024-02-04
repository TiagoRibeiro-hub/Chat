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

    public async Task<List<Group>?> GetGroupsAsync(UserKey key)
    {
        var user = await _baseRepository.GetAsync<User, UserKey>(key);
        if (Guards.IsNull(user))
        {
            throw new Exception("User not found");
        }
        return user.Groups;
    }
}

