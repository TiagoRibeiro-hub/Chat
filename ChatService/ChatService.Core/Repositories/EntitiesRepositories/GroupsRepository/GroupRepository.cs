using ChatService.Core.Helpers;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;
using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Core.Repositories.EntitiesRepositories.GroupsRepositories;

public sealed class GroupRepository : IGroupRepository
{
    private readonly IBaseRepository<ChatDbContext> _baseRepository;

    public GroupRepository(IBaseRepository<ChatDbContext> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<User> AddUserAsync(GroupKey key, User user, GroupRolesTypes groupRolesType)
    {
        try
        {
            _baseRepository.UnitOfWork.CreateTransaction();

            #region Add User To Group

            Group? group = await _baseRepository.GetAsync<Group, GroupKey>(key);

            if (Guards.IsNull(group))
            {
                throw new Exception();
            }

            if (!Guards.IsNotNullOrEmptyCollection(group.Users) || 
                group.Users.Any(u => u.Identifier == user.Key.Identifier))
            {
                throw new Exception();
            }

            group.Users.Add(user.Key);

            var groupTracker = _baseRepository.UnitOfWork.Context.Set<Group>().Update(group);
            if (groupTracker.State != EntityState.Modified)
            {
                throw new Exception();
            }

            _baseRepository.UnitOfWork.Commit();

            #endregion

            #region Update User

            if (!Guards.IsNotNullOrEmptyCollection(user.Groups))
            {
                throw new Exception();
            }

            user.Groups.Add(group);

            if (!Guards.IsNotNullOrEmptyCollection(user.Roles))
            {
                throw new Exception();
            }

            user.Roles.Add(new GroupRoles(groupRolesType, key));

            var userTracker = _baseRepository.UnitOfWork.Context.Set<User>().Update(user);
            if (userTracker.State != EntityState.Modified)
            {
                throw new Exception();
            }

            _baseRepository.UnitOfWork.Save();
            #endregion

            return user;
        }
        catch (Exception ex)
        {
            _baseRepository.UnitOfWork.Rollback();
            throw new Exception("AddUserAsync", ex);
        }
    }

    public async Task<bool> RemoveUserAsync(GroupKey key, UserKey userKey)
    {
        try
        {
            _baseRepository.UnitOfWork.CreateTransaction();

            #region Remove User From Group

            Group? group = await _baseRepository.GetAsync<Group, GroupKey>(key);

            if (Guards.IsNull(group))
            {
                throw new Exception("Group not found");
            }

            var user = await _baseRepository.UnitOfWork.Context.Set<User>().FirstOrDefaultAsync(x => x.Key.Identifier == userKey.Identifier);
            if (Guards.IsNull(user))
            {
                throw new Exception("User not found");
            }

            if (Guards.IsNull(group.Users) || !group.Users.Any(u => u.Identifier == user.Key.Identifier))
            {
                throw new Exception("Group has no users");
            }

            group.Users.Remove(user.Key);

            var groupTracker = _baseRepository.UnitOfWork.Context.Set<Group>().Update(group);
            if (groupTracker.State != EntityState.Modified)
            {
                throw new Exception();
            }

            _baseRepository.UnitOfWork.Commit();

            #endregion

            #region Update User

            if (!Guards.IsNotNullOrEmptyCollection(user.Groups))
            {
                throw new Exception("Group has no groups");
            }

            if (!Guards.IsNotNullOrEmptyCollection(user.Roles))
            {
                throw new Exception("User has no roles");
            }

            var currentUserRole = user.Roles.FirstOrDefault(ur =>
                                        ur.Group?.Identifier == group.Key.Identifier
                                        );

            if (Guards.IsNull(currentUserRole))
            {
                throw new Exception("User role not found");
            }

            if (!user.Groups.Remove(group) || !user.Roles.Remove(currentUserRole))
            {
                throw new Exception();
            }

            var userTracker = _baseRepository.UnitOfWork.Context.Set<User>().Update(user);
            if (userTracker.State != EntityState.Modified)
            {
                throw new Exception();
            }
            _baseRepository.UnitOfWork.Save();

            #endregion

            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            _baseRepository.UnitOfWork.Rollback();
            throw new Exception("RemoveUserAsync", ex);
        }
    }

}

