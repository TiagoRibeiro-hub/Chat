using ChatService.Core.Helpers;

namespace ChatService.Api.DTOS;

public abstract class KeyDTO<D, E>
{
    public Guid Identifier { get; set; }
    public string? Name { get; set; }

    public void ValidateIdentifier()
    {
        Guards.IsNotNullOrEmptyGuid(this.Identifier);
    }

    public abstract void DTOValidation();
}