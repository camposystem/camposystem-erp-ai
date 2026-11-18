using CampoSystem.ErpAI.Application.Identity.Authorization;
using CampoSystem.ErpAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampoSystem.ErpAI.Infrastructure.Identity.Authorization;

public sealed class AuthorizationUserService(
    IdentityDbContext context) : IAuthorizationUserService
{

    public async Task<bool> HasPermissionsAsync(
        string externalIdentityId,
        string tenantExternalId,
        IEnumerable<string> permissionNames,
        CancellationToken cancellationToken = default)
    {
        var membership = await context.TenantMemberships
            .AsNoTracking()
            .Include(membership => membership.MembershipRoles)
             .ThenInclude(membershipRole => membershipRole.Role)
              .ThenInclude(role => role.Permissions)
            .Where(membership => membership.UserId == context.Users
             .Where(user => user.ExternalIdentityId == externalIdentityId)
             .Select(user => user.Id)
             .FirstOrDefault()
             &&
             membership.TenantId == context.Tenants
            .Where(tenant => tenant.ExternalId == tenantExternalId)
            .Select(tenant => tenant.Id)
            .FirstOrDefault())
        .FirstOrDefaultAsync(cancellationToken);

        return membership?.HasPermissions(permissionNames) ?? false;
    }

}