using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;

namespace ChatService.Domain.Models.GroupUserMessages;

public sealed record GroupUserMessages(
    Group Group,
    List<User> Users,
    List<Message> Messages
    );
