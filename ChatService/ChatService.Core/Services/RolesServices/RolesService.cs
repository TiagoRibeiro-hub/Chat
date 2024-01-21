using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;

namespace ChatService.Core.Services.RolesServices;
internal class RolesService : IRolesService
{
    public Task<List<GroupRoles>> GetGroupRolesAsync(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<GroupRoles>> GetUserRolesAsync(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<GroupRoles>> UpdateGroupRoleAsync(GroupKey key, List<GroupRoles> groupRoles)
    {
        throw new NotImplementedException();
    }
}
