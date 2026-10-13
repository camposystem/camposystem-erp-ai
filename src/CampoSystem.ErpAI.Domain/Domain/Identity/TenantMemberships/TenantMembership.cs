using CampoSystem.ErpAI.Domain.Identity.Roles;

namespace CampoSystem.ErpAI.Domain.Identity.TenantMemberships;

public sealed class TenantMembership : Entity
{
    public Guid UserId { get; private set; }
    public Guid TenantId { get; private set; }

    private readonly List<Role> _roles = new();
    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

    public TenantMembership(Guid userId, Guid tenantId)
    {
        UserId = userId;
        TenantId = tenantId;
    }

    public void AddRole(Role role)
    {
        if (!_roles.Contains(role))
        {
            _roles.Add(role);
        }
    }
    public void RemoveRole(Role role)
    {
        if (_roles.Contains(role))
        {
            _roles.Remove(role);
        }
    }
}