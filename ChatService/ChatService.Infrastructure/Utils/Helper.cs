using ChatService.Api.DTOS;
using System.Runtime.CompilerServices;

namespace ChatService.Infrastructure.Utils;

public static class Helper
{
    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "ToDomain")]
    internal static extern ref E ToDomain<D, E>(D dto);

    internal static List<MessageDTO> GetMessagesDTO<T>(this T connectionsMessages, Guid identifier, MessageDTO? messageDTO)
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
