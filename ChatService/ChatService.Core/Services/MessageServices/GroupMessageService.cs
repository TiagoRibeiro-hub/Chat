using ChatService.Domain.Entities.Messages;

namespace ChatService.Core.Services.MessageServices;

public sealed class GroupMessageService : IGroupMessageService
{
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