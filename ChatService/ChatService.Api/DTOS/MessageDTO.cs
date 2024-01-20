using ChatService.Api.DTOS.Users;

namespace ChatService.Api.DTOS;

public sealed record MessageDTO(UserDTO User, string Text, DateTime Date);
