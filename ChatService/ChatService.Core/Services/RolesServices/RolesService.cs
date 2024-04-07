using ChatService.Core.Abstractions;
using ChatService.Core.Helpers;
using ChatService.Core.Repositories.EntitiesRepositories.RolesRepositories;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Infrastructure.Data;

namespace ChatService.Core.Services.RolesServices;
internal class RolesService : RepositoryBaseService, IRolesService
{
    private readonly IRoleRepository _roleRepository;
    public RolesService(
        IBaseRepository<ChatDbContext> baseRepository,
        IRoleRepository roleRepository) : base(baseRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<GroupRoles>> GetGroupRolesAsync(GroupKey key)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));
        return await _roleRepository.GetGroupRolesAsync(key);
    }

    public async Task<List<GroupRoles>> GetUserRolesAsync(UserKey key)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));
        return await _roleRepository.GetUserRolesAsync(key);
    }

    public async Task<List<GroupRoles>> UpdateGroupRoleAsync(GroupKey key, List<GroupRoles> groupRoles)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));
        if (groupRoles.Count > 0)
        {
            return await _roleRepository.UpdateGroupRoleAsync(key, groupRoles);
        }
        return groupRoles;
    }
}
