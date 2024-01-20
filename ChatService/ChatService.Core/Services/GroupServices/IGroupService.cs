using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;

namespace ChatService.Core.Services.GroupServices;
public interface IGroupService : IBaseService<Group, GroupKey>
{
    Task<User> AddUser(GroupKey key, User user);
    Task<User> GetFounder(GroupKey key);
    Task<List<User>> GetUsers(GroupKey key);
}
