using ChatService.Endpoints;

namespace ChatService.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapWhetherEndpoint();
        app.MapUserEndpoints();
        app.MapGroupEndpoints();
        app.MapRolesEndpoints();
    }
}
