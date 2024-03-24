using ChatService.Core;
using ChatService.Core.Helpers;

namespace ChatService.Api.DTOS;

public abstract class KeyDTO<D, E>
{
    public Guid Identifier { get; set; }
    public string? Name { get; set; }

    public void ValidateIdentifier()
    {
        Guards.IsNotNullOrEmptyGuid(this.Identifier, string.Format(ErrorMessages.NotFound, nameof(D)));
    }

    public abstract void DTOValidation();
}