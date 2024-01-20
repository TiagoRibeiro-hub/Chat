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
    public static void IsNotNull<T>([NotNull] T? obj)
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

    public static bool IsNotNullOrEmpty<T>([NotNullWhen(true)] IEnumerable<T>? obj)
    {
        return obj != null && obj.Any() ? true : false;
    }

    public static void IsNotNullOrEmpty([NotNull] Guid? obj)
    {
        if (obj == null || obj == Guid.Empty)
        {
            throw new Exception();
        }
    }
}

public class Parses
{
    public static Guid TryParseGuid(string? obj)
    {
        if (!Guid.TryParse(obj, out Guid guid))
        {
            throw new Exception();
        }
        return guid;
    }
}