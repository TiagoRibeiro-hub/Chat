using ChatService.Api.DTOS;
using ChatService.Domain.Models.Users;

namespace ChatService.Infrastructure.Utils;
internal static class ChatHubUtils
{
    public static List<MessageDTO> GetMessagesDTO<T>(T connectionsMessages, Guid identifier, MessageDTO? messageDTO)
        where T : IDictionary<Guid, List<MessageDTO>>
    {
        if (!connectionsMessages.TryGetValue(identifier, out List<MessageDTO>? messagesListDTO))
        {
            messagesListDTO = new();
        }
        if (messageDTO != null)
        {
            messagesListDTO.Add(messageDTO);
        }
        return messagesListDTO;
    }
}

