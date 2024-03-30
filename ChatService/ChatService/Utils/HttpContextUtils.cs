using ChatService.Core.Helpers;

namespace ChatService.Utils;
public class HttpContextUtils
{
    internal static object GetRouteValueObject(HttpContext httpContext, string name)
    {
        var obj = httpContext.GetRouteValue(name);
        Guards.IsNotNullObject(obj);
        return obj;
    }

    internal static Guid GetRouteValueGuid(HttpContext httpContext, string name)
    {
        object obj = GetRouteValueObject(httpContext, name);

        Guards.IsNotNullObject(obj);
        string value = obj.ToString();

        if (!Guid.TryParse(value, out Guid valueGuid))
        {
            throw new Exception();
        }

        return valueGuid;
    }

    internal static string GetRouteValueGuidAsString(HttpContext httpContext, string name)
    {
        object obj = GetRouteValueObject(httpContext, name);

        string value = obj.ToString();
        Guards.IsNotNullObject(value);

        if (!Guid.TryParse(value, out Guid _))
        {
            throw new Exception();
        }

        return value;
    }
}

