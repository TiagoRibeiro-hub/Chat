using ChatService.Core.Abstractions;
using ChatService.Domain.Entities.Messages;

namespace ChatService.Core.Repositories.EntitiesRepositories.MessagesRepository;
public interface IUserMessageRepository : IBaseMessageService<UserMessage>
{
    Task<List<UserMessage>?> ListAsync(Guid identifier);
}
