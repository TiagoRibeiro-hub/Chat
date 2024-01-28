using ChatService.Api.DTOS.Messages;
using ChatService.Api.DTOS.Users;

namespace ChatService.Infrastructure.Hubs.Chat.Services;

public interface IChaHub
{
    Task JoinAsync(UserMessageDTO messageDTO);
    Task ReceiveMessageAsync(UserDTO user, UserMessageDTO messageDTO);
}
