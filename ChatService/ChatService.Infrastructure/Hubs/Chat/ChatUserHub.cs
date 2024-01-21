using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Users;
using ChatService.Infrastructure.Hubs.Chat.Services;
using ChatService.Infrastructure.Utils;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Infrastructure.Hubs.Chat;

internal sealed class ChatUserHub : Hub<IChatUserHub>
{
    private readonly IDictionary<string, UserDTO>? _connections;

    private readonly IDictionary<Guid, List<MessageDTO>>? _connectionsMessages;

    public async Task JoinChatAsync(UserDTO user)
    {
        user.Key.ValidateIdentifier();

        if (string.IsNullOrEmpty(user.Key.Name))
        {
            // TODO get name from bd
        }
        _connections![Context.ConnectionId] = user;

        List<MessageDTO>? messagesListDTO = null; // TODO Bd see if already had started a conversation 
        if (messagesListDTO == null)
        {
            var messageDTO = new MessageDTO(user, "Has started a conversation", DateTime.UtcNow);
            await Clients.Caller.JoinAsync(messageDTO);
            _connectionsMessages![user.Key.Identifier] = _connectionsMessages.GetMessagesDTO(user.Key.Identifier, messageDTO);
            // TODO Bd start a conversation
        }
        else
        {
            await Clients.Caller.PreviousMessageAsync(user, messagesListDTO);
            _connectionsMessages![user.Key.Identifier] = messagesListDTO;
        }
    }

    public void LeftChat(UserDTO user)
    {
        user.Key.ValidateIdentifier();
        // TODO Bd remove from user id
        _ = _connections!.Remove(Context.ConnectionId);
        _ = _connectionsMessages!.Remove(user.Key.Identifier);
    }

    public async Task SendMessageAsync(string message)
    {
        if (_connections!.TryGetValue(Context.ConnectionId, out UserDTO? userDTO))
        {
            var messagesDTO = new MessageDTO(userDTO, message, DateTime.UtcNow);

            _connectionsMessages![userDTO.Key.Identifier] = _connectionsMessages.GetMessagesDTO(userDTO.Key.Identifier, messagesDTO);

            await Clients.Caller.ReceiveMessageAsync(userDTO, messagesDTO);

            // TODO Bd update messages
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (_connections!.TryGetValue(Context.ConnectionId, out UserDTO? userDTO))
        {
            _ = _connections!.Remove(Context.ConnectionId);
            _ = _connectionsMessages!.Remove(userDTO.Key.Identifier);
        }
        return base.OnDisconnectedAsync(exception);
    }
}