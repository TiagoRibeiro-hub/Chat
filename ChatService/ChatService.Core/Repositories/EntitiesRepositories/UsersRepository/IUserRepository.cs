using ChatService.Core.Services.UserServices;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;

namespace ChatService.Core.Repositories.EntitiesRepositories.UsersRepositories;

public interface IUserRepository
{
    Task<List<Group>?> GetGroupsAsync(UserKey key);
}

