using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Repositories.EntitiesRepositories.RolesRepositories;

public sealed class RoleRepository : IRoleRepository
{
    private readonly IBaseRepository<ChatDbContext> _baseRepository;

    public RoleRepository(IBaseRepository<ChatDbContext> baseRepository)
    {
        _baseRepository = baseRepository;
    }

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
