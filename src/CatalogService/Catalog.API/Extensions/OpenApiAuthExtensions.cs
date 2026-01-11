using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Reflection;

namespace Catalog.API.Extensions;

public static class OpenApiAuthExtensions
{
    public static void AddOpenApiGeneration(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition(SecuritySchemeType.OAuth2.ToString(), new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(builder.Configuration["OpenApi:OAuth2:AuthorizationUrl"] ?? string.Empty),
                        TokenUrl = new Uri(builder.Configuration["OpenApi:OAuth2:TokenUrl"] ?? string.Empty),
                        Extensions = new Dictionary<string, IOpenApiExtension>()
                        {
                            ["x-usePkce"] = new OpenApiString("SHA-256")
                        }
                    }
                }
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SecuritySchemeType.OAuth2.ToString()
                        }
                    },
                    new List<string>()
                }
            });
        });
    }


    public static void UseScalar(this WebApplication app)
    {
        app.UseSwagger(opt =>
        {
            opt.RouteTemplate = "/openapi/{documentName}.json";
        });
        app.MapScalarApiReference(opt =>
        {
            opt.Title = Assembly.GetExecutingAssembly().GetName().Name ?? "eMarketplace.Catalog.API";
            opt.DarkMode = true;
            opt.EnabledTargets = [ScalarTarget.CSharp, ScalarTarget.Http, ScalarTarget.Shell, ScalarTarget.JavaScript, ScalarTarget.Python];
            opt.Theme = ScalarTheme.Moon;
            opt.AddAuthorizationCodeFlow(SecuritySchemeType.OAuth2.ToString(), flow =>
            {
                flow.ClientId = app.Configuration["OpenApi:OAuth2:ClientId"] ?? string.Empty;
                flow.Pkce = Pkce.Sha256;
                flow.SelectedScopes = ["email", "profile", "openid"];
            });
        });
    }
}
