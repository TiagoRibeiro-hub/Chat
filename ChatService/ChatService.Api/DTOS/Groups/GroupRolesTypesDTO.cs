using System.Text.Json.Serialization;

namespace ChatService.Api.DTOS.Groups;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GroupRolesTypesDTO
{
    Founder = 'A',
    Manager = 'M',
    User = 'U'
}