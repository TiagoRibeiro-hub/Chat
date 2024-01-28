using ChatService.Domain.Entities.Messages;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Repositories.EntitiesRepositories.MessagesRepository;
public interface IUserMessageRepository : IBaseMessageService<UserMessage>
{
    Task<List<UserMessage>?> ListAsync(Guid identifier);
}
