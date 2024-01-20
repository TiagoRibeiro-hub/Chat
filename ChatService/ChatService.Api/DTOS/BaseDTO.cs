namespace ChatService.Api.DTOS;

public abstract class BaseDTO<D, E>
{
    internal abstract void ValidateKey();
    internal abstract void ValidateKeys(List<D>? roles);
    public abstract void DTOValidation();
}

