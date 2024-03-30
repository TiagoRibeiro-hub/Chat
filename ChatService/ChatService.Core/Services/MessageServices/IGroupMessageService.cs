using ChatService.Core.Abstractions;
using ChatService.Domain.Entities.Messages;

namespace ChatService.Core.Services.MessageServices;

public interface IGroupMessageService : IBaseMessageService<GroupMessages>
{
    Task<GroupMessages?> GetAsync(Guid identifier);
}