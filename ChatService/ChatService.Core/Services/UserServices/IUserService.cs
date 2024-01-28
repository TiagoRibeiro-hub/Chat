using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Services.UserServices;

public interface IUserService : IBaseService<User, UserKey>
{
    Task<List<Group>?> GetGroupsAsync(UserKey key);
}
