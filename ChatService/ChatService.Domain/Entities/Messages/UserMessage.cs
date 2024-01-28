namespace ChatService.Domain.Entities.Messages;

public sealed record UserMessage(Guid UserIdentifier, string Text, DateTime Date) : BaseMessage;
