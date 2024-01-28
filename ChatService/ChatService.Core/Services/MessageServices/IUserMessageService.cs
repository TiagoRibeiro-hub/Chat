using ChatService.Domain.Entities.Messages;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Services.MessageServices;

public interface IUserMessageService : IBaseMessageService<UserMessage>
{
    Task<List<UserMessage>?> ListAsync(Guid identifier);
}