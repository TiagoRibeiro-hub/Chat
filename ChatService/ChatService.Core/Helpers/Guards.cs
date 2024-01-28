using System.Diagnostics.CodeAnalysis;

namespace ChatService.Core.Helpers;

public class Guards
{
    public static void IsTypeOf<T>(Type obj)
    {
        if (!obj.Equals(typeof(T)))
        {
            throw new Exception();
        }
    }
    public static void IsNotNullObject<T>([NotNull] T? obj)
    {
        if (obj == null)
        {
            throw new Exception();
        }
    }    

    public static bool IsNull<T>([NotNullWhen(false)] T? obj)
    {
        return obj == null ? true : false;
    }

    public static bool IsNotNullOrEmptyCollection<T>([NotNullWhen(true)] IEnumerable<T>? obj)
    {
        return obj != null && obj.Any() ? true : false;
    }

    public static void IsNotNullOrEmptyGuid([NotNull] Guid? obj)
    {
        if (obj == null || obj == Guid.Empty)
        {
            throw new Exception();
        }
    }
}