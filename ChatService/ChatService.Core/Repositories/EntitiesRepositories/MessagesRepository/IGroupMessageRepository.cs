using ChatService.Core.Abstractions;
using ChatService.Domain.Entities.Messages;

namespace ChatService.Core.Repositories.EntitiesRepositories.MessagesRepository;

public interface IGroupMessageRepository : IBaseMessageService<GroupMessages>
{
    Task<GroupMessages?> GetAsync(Guid identifier);
}
