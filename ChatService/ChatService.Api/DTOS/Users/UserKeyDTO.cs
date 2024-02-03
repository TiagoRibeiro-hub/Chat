using ChatService.Core.Helpers;
using ChatService.Domain.Entities;
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

    internal static void ValidateKeys(List<UserKeyDTO>? keys)
    {
        if (Guards.IsNotNullOrEmptyCollection(keys))
        {
            for (int i = 0; i < keys.Count; i++)
            {
                keys[i].DTOValidation();
            }
        }
    }
}