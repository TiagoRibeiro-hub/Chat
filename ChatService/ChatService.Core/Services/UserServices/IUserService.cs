using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;

namespace ChatService.Core.Services.UserServices;

public interface IUserService : IBaseService<User, UserKey>
{
    Task<List<Group>> GetGroups(UserKey key);
}
