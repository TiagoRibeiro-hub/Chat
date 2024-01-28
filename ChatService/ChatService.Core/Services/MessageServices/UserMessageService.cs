using ChatService.Domain.Entities.Messages;

namespace ChatService.Core.Services.MessageServices;

public sealed class UserMessageService : IUserMessageService
{
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