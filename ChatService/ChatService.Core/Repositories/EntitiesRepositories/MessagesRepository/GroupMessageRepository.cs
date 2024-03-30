using ChatService.Core.Abstractions;
using ChatService.Domain.Entities.Messages;
using ChatService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Core.Repositories.EntitiesRepositories.MessagesRepository;

public sealed class GroupMessageRepository : IGroupMessageRepository
{
    private IUnitOfWork<ChatDbContext> UnitOfWork { get; }

    private DbSet<GroupMessages> Entity { get; set; }

    public GroupMessageRepository(IUnitOfWork<ChatDbContext> unitOfWork)
    {
        UnitOfWork = unitOfWork;
        Entity = UnitOfWork.Context.Set<GroupMessages>();
    }

    public Task<bool> AddAsync(GroupMessages item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid identifier)
    {
        throw new NotImplementedException();
    }

    public Task<GroupMessages?> GetAsync(Guid identifier)
    {
        throw new NotImplementedException();
    }
}
