using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Messages;

namespace ChatService.Infrastructure.Hubs.Chat.Services;

public interface IChatGroupHub : IChaHub
{
    Task ConnectedUsers(IEnumerable<GroupUserDTO> users);
    Task LeftGroupAsync(UserMessageDTO message);
    Task PreviousMessageAsync(GroupDTO groupDTO, IEnumerable<UserMessageDTO>? messagesListDTO);
}
