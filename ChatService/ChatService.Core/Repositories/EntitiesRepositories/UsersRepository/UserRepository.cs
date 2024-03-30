using ChatService.Core.Abstractions;
using ChatService.Core.Helpers;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;
using ChatService.Infrastructure.Data;

namespace ChatService.Core.Repositories.EntitiesRepositories.UsersRepositories;

public sealed class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(
        IBaseRepository<ChatDbContext> baseRepository) : base(baseRepository)
    {
    }

    public async Task<List<Group>?> GetGroupsAsync(UserKey key)
    {
        var user = await _baseRepository.GetAsync<User, UserKey>(key);
        if (Guards.IsNull(user))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(User)));
        }
        return user.Groups;
    }
}

