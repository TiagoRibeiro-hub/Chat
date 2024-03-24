using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Repositories.EntitiesRepositories.GroupsRepositories;

public sealed class GroupRepository : RepositoryBase, IGroupRepository
{
    public GroupRepository(IBaseRepository<ChatDbContext> baseRepository) : base(baseRepository)
    {
    }
}

