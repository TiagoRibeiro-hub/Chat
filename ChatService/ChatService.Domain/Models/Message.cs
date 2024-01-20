namespace ChatService.Domain.Models;

public sealed record Message(Guid Identifier, string Text, DateTime Date);
