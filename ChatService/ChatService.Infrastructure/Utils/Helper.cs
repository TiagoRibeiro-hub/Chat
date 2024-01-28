using ChatService.Api.DTOS.Messages;
using System.Runtime.CompilerServices;

namespace ChatService.Infrastructure.Utils;

public static class Helper
{
    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "ToDomain")]
    internal static extern ref E ToDomain<D, E>(D dto);

    internal static List<UserMessageDTO> GetMessagesDTO<T>(this T connectionsMessages, Guid identifier, UserMessageDTO? messageDTO)
        where T : IDictionary<Guid, List<UserMessageDTO>>
    {
        if (!connectionsMessages.TryGetValue(identifier, out List<UserMessageDTO>? messagesListDTO))
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
