using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Users;

namespace ChatService.Infrastructure.Hubs.Chat.Services;

public interface IChatUserHub : IChaHub
{
    Task PreviousMessageAsync(UserDTO userDTO, IEnumerable<MessageDTO> messagesListDTO);
}
