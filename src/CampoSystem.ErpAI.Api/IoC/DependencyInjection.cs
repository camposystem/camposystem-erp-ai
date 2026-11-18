using CampoSystem.ErpAI.Api.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;

namespace CampoSystem.ErpAI.Api.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Auth0:Authority"];
                options.Audience = configuration["Auth0:Audience"];
                options.IncludeErrorDetails = false;
            });
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes ??=
                    new Dictionary<string, IOpenApiSecurityScheme>();

                document.Components.SecuritySchemes["Bearer"] =
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "Access Token emitido pelo Auth0."
                    };

                return Task.CompletedTask;
            });
        });

        services.AddScoped<IAuthorizationHandler,PermissionAuthorizationHandler>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Products.Create", policy =>
            {
                policy.RequireAuthenticatedUser();

                policy.Requirements.Add(
                    new PermissionRequirement("products.create"));
            });
        });

        services.AddSingleton<IAuthorizationMiddlewareResultHandler,
            CustomAuthorizationMiddlewareResultHandler>();

        return services;
    }
}