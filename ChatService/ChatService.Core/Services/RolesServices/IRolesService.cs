using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;

namespace ChatService.Core.Services.RolesServices;

public interface IRolesService
{
    Task<List<GroupRoles>> GetGroupRolesAsync(GroupKey key);
    Task<List<GroupRoles>> GetUserRolesAsync(UserKey key);
    Task<List<GroupRoles>> UpdateGroupRoleAsync(GroupKey key, List<GroupRoles> groupRoles);
}