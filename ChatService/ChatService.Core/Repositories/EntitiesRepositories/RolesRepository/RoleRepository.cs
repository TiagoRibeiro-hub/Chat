using ChatService.Core.Abstractions;
using ChatService.Core.Helpers;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;
using ChatService.Infrastructure.Data;

namespace ChatService.Core.Repositories.EntitiesRepositories.RolesRepositories;

public sealed class RoleRepository : RepositoryBaseService, IRoleRepository
{
    public RoleRepository(
        IBaseRepository<ChatDbContext> baseRepository) : base(baseRepository)
    {
    }

    public Task<List<GroupRoles>> GetGroupRolesAsync(GroupKey key)
    {
        try
        {
            return Task.FromResult(_baseRepository.UnitOfWork
                                    .Context
                                    .Set<User>()
                                    .Where(x => x.Roles != null && x.Roles.Any(r => r.Group == key))
                                    .Select(x => x.Groups)
                                    .OfType<GroupRoles>()
                                    .ToList()
                                    );
        }
        catch (Exception)
        {
            throw new Exception(ErrorMessages.SomethingWentWrong);
        }
    }

    public async Task<List<GroupRoles>> GetUserRolesAsync(UserKey key)
    {
        var user = await _baseRepository.GetAsync<User, UserKey>(key);
        if (Guards.IsNull(user))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(User)));
        }
        return user.Roles ?? new();
    }

    public Task<List<GroupRoles>> UpdateGroupRoleAsync(GroupKey key, List<GroupRoles> groupRoles)
    {
        var users = _baseRepository.UnitOfWork
                                .Context
                                .Set<User>()
                                .Where(x => x.Roles != null && x.Roles.Any(r => r.Group == key))
                                .OfType<User>();

        if (!Guards.IsNotNullOrEmptyCollection(users))
        {
            throw new Exception(ErrorMessages.GroupHasNoUsers);
        }
        try
        {
            _baseRepository.UnitOfWork.CreateTransaction();

            var usersToUpdate = new List<User>();

            var usersEnumerator = users.GetEnumerator();
            while (usersEnumerator.MoveNext())
            {
                var user = usersEnumerator.Current;
                if (!Guards.IsNull(user))
                {
                    user.Roles = groupRoles.Where(x => x.Group == user.Key)
                                            .ToList();
                    usersToUpdate.Add(user);
                }
            }

            _baseRepository.UnitOfWork
                                .Context
                                .Set<User>()
                                .UpdateRange(usersToUpdate);

            _baseRepository.UnitOfWork.SaveChanges();
            return Task.FromResult(groupRoles);
        }
        catch (Exception)
        {
            _baseRepository.UnitOfWork.Rollback();
            throw new Exception(ErrorMessages.SomethingWentWrong);
        }
    }
}
