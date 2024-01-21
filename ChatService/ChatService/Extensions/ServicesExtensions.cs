using ChatService.Core.Services;
using ChatService.Domain.Constants;
using ChatService.Infrastructure.Extensions;
using System.Text.Json.Serialization;

namespace ChatService.Api.Extensions;

public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<CoreServices>();
        services.AddCoreServices();

        services.AddChatServices();

        services.AddCorsPolicies();

        services.AddRepositoriesServices();
    }

    public static void AddCorsPolicies(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(Constant.AllowAnyOrigins, policy =>
            {
                policy.SetIsOriginAllowed(_ => true) // allow any origin
                        .WithMethods("GET", "POST", "PUT", "DELETE")
                        .AllowAnyHeader()
                        .AllowCredentials();
            });
        });
    }

    public static void AddJsonOptions(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.WriteIndented = true;
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.NumberHandling = JsonNumberHandling.Strict;
        });
    }

}
