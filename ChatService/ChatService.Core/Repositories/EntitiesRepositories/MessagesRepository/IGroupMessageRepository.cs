using ChatService.Domain.Entities.Messages;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Repositories.EntitiesRepositories.MessagesRepository;

public interface IGroupMessageRepository : IBaseMessageService<GroupMessages>
{
    Task<GroupMessages?> GetAsync(Guid identifier);
}
