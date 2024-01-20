using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;

namespace ChatService.Core.Services.RolesServices;

public interface IRolesService
{
    Task<List<GroupRoles>> GetGroupRoles(GroupKey key);
    Task<List<GroupRoles>> GetUserRoles(UserKey key);
    Task<List<GroupRoles>> UpdateGroupRole(GroupKey key, List<GroupRoles> groupRoles);
}