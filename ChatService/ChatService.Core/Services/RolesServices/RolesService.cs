using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;

namespace ChatService.Core.Services.RolesServices;
internal class RolesService : IRolesService
{
    public Task<List<GroupRoles>> GetGroupRoles(GroupKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<GroupRoles>> GetUserRoles(UserKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<GroupRoles>> UpdateGroupRole(GroupKey key, List<GroupRoles> groupRoles)
    {
        throw new NotImplementedException();
    }
}
