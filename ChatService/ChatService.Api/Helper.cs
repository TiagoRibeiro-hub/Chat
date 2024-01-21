using ChatService.Api.DTOS;
using System.Runtime.CompilerServices;

namespace ChatService.Api;
internal static class Helper
{
    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "ValidateKeys")]
    internal static extern ref Action<List<D>?> ValidateKeys<D>();

    internal static void ValidateKeys<D, E>(this List<D>? list) where D : BaseDTO<D, E>
    {
        try
        {
            Helper.ValidateKeys<D>().Invoke(list);
        }
        catch (Exception ex)
        {
            throw new Exception("ValidateKeys UnsafeAccessor", ex);
        }
    }
}

