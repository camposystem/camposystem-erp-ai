namespace CampoSystem.ErpAI.Application.Identity.Authorization;

public interface IAuthorizationUserService
{
    Task<bool> HasPermissionsAsync(
        string externalIdentityId,
        string tenantExternalId,
        IEnumerable<string> permissionNames,
        CancellationToken cancellationToken = default);
}
