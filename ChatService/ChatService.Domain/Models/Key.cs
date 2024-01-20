namespace ChatService.Domain.Models;

public record Key(Guid Identifier, string Name);

public sealed record UserKey(Guid Identifier, string Name) : Key(Identifier, Name);

public sealed record GroupKey(Guid Identifier, string Name) : Key(Identifier, Name);
