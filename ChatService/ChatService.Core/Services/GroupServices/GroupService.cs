using ChatService.Core.Abstractions;
using ChatService.Core.Helpers;
using ChatService.Core.Repositories.EntitiesRepositories.GroupsRepositories;
using ChatService.Domain.Entities;
using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Users;
using ChatService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Core.Services.GroupServices;

internal sealed class GroupService : RepositoryBaseService, IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(
        IBaseRepository<ChatDbContext> baseRepository,
        IGroupRepository groupRepository) : base(baseRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<User> AddUserAsync(GroupKey key, User user, GroupRolesTypes groupRolesType)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));
        Guards.IsNotNullOrEmptyGuid(user.Key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(user.Key.Identifier)));

        if (groupRolesType == GroupRolesTypes.Founder)
        {
            throw new Exception(ErrorMessages.UserIsTheFounder);
        }

        try
        {
            _baseRepository.UnitOfWork.CreateTransaction();

            #region Add User To Group

            Group? group = await _baseRepository.GetAsync<Group, GroupKey>(key);

            if (Guards.IsNull(group))
            {
                throw new Exception(string.Format(ErrorMessages.NotFound, nameof(Group)));
            }

            if (!Guards.IsNotNullOrEmptyCollection(group.Users) ||
                group.Users.Any(u => u.Identifier == user.Key.Identifier))
            {
                throw new Exception(string.Format(ErrorMessages.UserNotFoundOnGroup, nameof(user.Key.Name), nameof(group.Key.Name)));
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
                throw new Exception(ErrorMessages.GroupHasNoUsers);
            }

            user.Groups.Add(group);

            if (!Guards.IsNotNullOrEmptyCollection(user.Roles))
            {
                user.Roles = new();
            }

            user.Roles.Add(new GroupRoles(groupRolesType, key, user.Key));

            var userTracker = _baseRepository.UnitOfWork.Context.Set<User>().Update(user);
            if (userTracker.State != EntityState.Modified)
            {
                throw new Exception();
            }

            _baseRepository.UnitOfWork.SaveChanges();
            #endregion

            return user;
        }
        catch (Exception ex)
        {
            _baseRepository.UnitOfWork.Rollback();
            throw new Exception(string.Format(ErrorMessages.Adding, nameof(User)), ex);
        }
    }

    public async Task<Group> CreateAsync(Group item)
    {
        Guards.IsNotNullOrEmptyGuid(item.Founder, string.Format(ErrorMessages.NotFound, nameof(item.Founder)));

        var hasFounder = item.Users?.Any(x => x.Identifier == item.Founder);
        if (hasFounder != true)
        {
            item.Users = item.Users ?? new();

            User? user = await _baseRepository.UnitOfWork.Context.Set<User>().FirstOrDefaultAsync(x => x.Key.Identifier == item.Founder);
            if (Guards.IsNull(user))
            {
                throw new Exception(string.Format(ErrorMessages.NotFound, nameof(item.Founder)));
            }

            item.Users.Add(user.Key);
        }
        return await _baseRepository.CreateAsync<Group, GroupKey>(item);
    }

    public async Task<bool> DeleteAsync(GroupKey key)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));
        return await _baseRepository.DeleteAsync<Group, GroupKey>(key);
    }

    public async Task<Group?> GetAsync(GroupKey key)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));
        return await _baseRepository.GetAsync<Group, GroupKey>(key);
    }

    public async Task<User> GetFounderAsync(GroupKey key)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));

        var group = await GetAsync(key);
        if (Guards.IsNull(group))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(Group)));
        }

        User? user = await _baseRepository.UnitOfWork.Context.Set<User>().FirstOrDefaultAsync(x => x.Key.Identifier == group.Founder);
        if (Guards.IsNull(user))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(group.Founder)));
        }
        return user;
    }

    public async Task<string> GetNameAsync(GroupKey key)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));

        var group = await GetAsync(key);
        if (Guards.IsNull(group))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(Group)));
        }
        return group.Key.Name;
    }

    public async Task<List<User>?> GetUsersAsync(GroupKey key)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));

        var group = await GetAsync(key);
        if (Guards.IsNull(group))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(Group)));
        }
        return await _baseRepository.UnitOfWork.Context.Set<User>().ToListAsync();
    }

    public async Task<List<Group>> ListAsync(bool complete)
    {
        return await _baseRepository.ListAsync<Group, GroupKey>(complete);
    }

    public async Task<bool> RemoveUserAsync(GroupKey key, UserKey userKey)
    {
        Guards.IsNotNullOrEmptyGuid(key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(key.Identifier)));
        Guards.IsNotNullOrEmptyGuid(userKey.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(userKey.Identifier)));

        User? user = await _baseRepository.UnitOfWork.Context.Set<User>().FirstOrDefaultAsync(x => x.Key.Identifier == userKey.Identifier);
        if (Guards.IsNull(user))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(User)));
        }

        if (!Guards.IsNotNullOrEmptyCollection(user.Groups))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(user.Groups)));

        }

        if (user.Groups.Any(x => x.Founder == userKey.Identifier))
        {
            throw new Exception(ErrorMessages.FounderCanNotBeRemoved);
        }

        Group? group = await _baseRepository.GetAsync<Group, GroupKey>(key);

        if (Guards.IsNull(group))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(Group)));
        }

        if (Guards.IsNull(group.Users) || !group.Users.Any(u => u.Identifier == user.Key.Identifier))
        {
            throw new Exception(ErrorMessages.GroupHasNoUsers);
        }
        group.Users.Remove(user.Key);

        try
        {
            _baseRepository.UnitOfWork.CreateTransaction();

            #region Remove User From Group

            var groupTracker = _baseRepository.UnitOfWork.Context.Set<Group>().Update(group);
            if (groupTracker.State != EntityState.Modified)
            {
                throw new Exception();
            }

            _baseRepository.UnitOfWork.Commit();

            #endregion

            #region Update User

            if (!Guards.IsNotNullOrEmptyCollection(user.Roles))
            {
                throw new Exception(ErrorMessages.UserHasNoRoles);
            }

            var currentUserRole = user.Roles.FirstOrDefault(ur =>
                                        ur.Group?.Identifier == group.Key.Identifier
                                        );

            if (Guards.IsNull(currentUserRole))
            {
                throw new Exception(string.Format(ErrorMessages.NotFound, nameof(GroupRoles)));
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
            _baseRepository.UnitOfWork.SaveChanges();

            #endregion

            return true;
        }
        catch (Exception ex)
        {
            _baseRepository.UnitOfWork.Rollback();
            throw new Exception(string.Format(ErrorMessages.Removing, nameof(User)), ex);
        }
    }

    public async Task<bool> UpdateAsync(Group item)
    {
        Guards.IsNotNullOrEmptyGuid(item.Key.Identifier, string.Format(ErrorMessages.KeyIsNull, nameof(item.Key.Identifier)));
        return await _baseRepository.Update<Group, GroupKey>(item);
    }
}
