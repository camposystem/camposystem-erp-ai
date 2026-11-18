
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace CampoSystem.ErpAI.Api.Authorization;

public sealed class CustomAuthorizationMiddlewareResultHandler
    : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Forbidden)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            await context.Response.WriteAsJsonAsync(new
            {
                status = StatusCodes.Status403Forbidden,
                title = "Forbidden",
                detail = "Você não possui permissão para executar esta operação."
            });

            return;
        }

        await _defaultHandler.HandleAsync(
            next,
            context,
            policy,
            authorizeResult);
    }
}
