using ChatService.Domain.Entities.Messages;
using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Core.Repositories.EntitiesRepositories.MessagesRepository;

public sealed class UserMessageRepository : IUserMessageRepository
{
    private IUnitOfWork<ChatDbContext> UnitOfWork { get; }

    private DbSet<UserMessage> Entity { get; set; }

    public UserMessageRepository(IUnitOfWork<ChatDbContext> unitOfWork)
    {
        UnitOfWork = unitOfWork;
        Entity = UnitOfWork.Context.Set<UserMessage>();
    }

    public Task<bool> AddAsync(UserMessage item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid identifier)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserMessage>?> ListAsync(Guid identifier)
    {
        throw new NotImplementedException();
    }
}