using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Users;

namespace ChatService.Infrastructure.Hubs.Chat.Services;

public interface IChaHub
{
    Task JoinAsync(MessageDTO messageDTO);
    Task ReceiveMessageAsync(UserDTO user, MessageDTO messageDTO);
}
