using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Catalog.API.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = configuration["OAuth20:Authority"];
                options.Audience = configuration["OAuth20:Audience"];
                options.RequireHttpsMetadata = configuration.GetValue<bool>("OAuth20:RequireHttpsMetadata");
                options.TokenValidationParameters.ValidateAudience = true;
                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidateLifetime = true;
                options.TokenValidationParameters.ValidateIssuerSigningKey = true;
            });
        services.AddAuthorization();
        return services;
    }

}
