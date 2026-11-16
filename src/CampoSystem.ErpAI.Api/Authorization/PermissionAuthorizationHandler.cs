
using System.Security.Claims;
using CampoSystem.ErpAI.Application.Identity.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace CampoSystem.ErpAI.Api.Authorization;

public sealed class PermissionAuthorizationHandler(
    IAuthorizationUserService authorizationUserService)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var externalIdentityId = context.User.FindFirstValue(
            ClaimTypes.NameIdentifier);
        var tenantExternalId = context.User.FindFirstValue("org_id");

        if (string.IsNullOrWhiteSpace(externalIdentityId) ||
            string.IsNullOrWhiteSpace(tenantExternalId))
        {
            return;
        }

        var hasPermissions =
            await authorizationUserService.HasPermissionsAsync(
                externalIdentityId,
                tenantExternalId,
                requirement.PermissionNames);

        if (hasPermissions)
            context.Succeed(requirement);
    }
}
