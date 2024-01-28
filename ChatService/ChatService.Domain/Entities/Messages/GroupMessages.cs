namespace ChatService.Domain.Entities.Messages;

public sealed record GroupMessages(Guid GroupIdentifier, List<UserMessage> Messages) : BaseMessage;
