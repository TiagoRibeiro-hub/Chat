using ChatService.Api.Utils;
using ChatService.Domain.Models;
using System.Text.Json.Serialization;

namespace ChatService.Api.DTOS.Users;

public sealed class UserKeyDTO : KeyDTO<UserKeyDTO, UserKey>
{
    [JsonConstructor]
    public UserKeyDTO(Guid identifier)
    {
        Identifier = identifier;
    }

    public UserKeyDTO(Guid identifier, string name)
    {
        Identifier = identifier;
        Name = name;
    }

    public override void DTOValidation()
    {
        ValidateIdentifier();
    }
}