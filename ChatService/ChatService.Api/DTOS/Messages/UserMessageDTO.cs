namespace ChatService.Api.DTOS.Messages;

public sealed record UserMessageDTO(Guid UserIdentifier, string Text, DateTime Date);
