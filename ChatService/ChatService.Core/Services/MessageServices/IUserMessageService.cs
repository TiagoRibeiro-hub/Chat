using ChatService.Core.Abstractions;
using ChatService.Domain.Entities.Messages;

namespace ChatService.Core.Services.MessageServices;

public interface IUserMessageService : IBaseMessageService<UserMessage>
{
    Task<List<UserMessage>?> ListAsync(Guid identifier);
}