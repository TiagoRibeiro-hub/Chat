using ChatService.Domain.Entities;
using System.Text.Json.Serialization;

namespace ChatService.Api.DTOS.Groups;

public sealed class GroupKeyDTO : KeyDTO<GroupKeyDTO, GroupKey>
{
    [JsonConstructor]
    public GroupKeyDTO(Guid identifier)
    {
        Identifier = identifier;
    }

    public GroupKeyDTO(Guid identifier, string name)
    {
        Identifier = identifier;
        Name = name;
    }

    public override void DTOValidation()
    {
        ValidateIdentifier();
    }
}