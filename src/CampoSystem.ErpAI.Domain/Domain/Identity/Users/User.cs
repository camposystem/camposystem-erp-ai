using CampoSystem.ErpAI.Domain.Identity.TenantMemberships;

namespace CampoSystem.ErpAI.Domain.Identity.Users;

public sealed class User : Entity
{
    public string ExternalIdentityId { get; private set; }

    private readonly List<TenantMembership> _tenantMemberships
        = new();

    public IReadOnlyCollection<TenantMembership> TenantMemberships => _tenantMemberships.AsReadOnly();

    public User(string externalIdentityId)
    {
        ExternalIdentityId = externalIdentityId;
    }

    public void AddTenantMembership(TenantMembership tenantMemberships)
    {
        var alreadyBelongsToTenant = _tenantMemberships.Any(
           membership => membership.TenantId == tenantMemberships.TenantId);

        if (!alreadyBelongsToTenant)
        {
            _tenantMemberships.Add(tenantMemberships);
        }
    }
    public void RemoveTenantMembership(TenantMembership tenantMemberships)
    {
        if (_tenantMemberships.Contains(tenantMemberships))
        {
            _tenantMemberships.Remove(tenantMemberships);
        }
    }

}
