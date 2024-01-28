using ChatService.Domain.Entities.Messages;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Services.MessageServices;

public interface IGroupMessageService : IBaseMessageService<GroupMessages>
{
    Task<GroupMessages?> GetAsync(Guid identifier);
}