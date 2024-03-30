using ChatService.Core.Abstractions;
using ChatService.Infrastructure.Data;

namespace ChatService.Core.Repositories.EntitiesRepositories.GroupsRepositories;

public sealed class GroupRepository : RepositoryBase, IGroupRepository
{
    public GroupRepository(IBaseRepository<ChatDbContext> baseRepository) : base(baseRepository)
    {
    }
}

