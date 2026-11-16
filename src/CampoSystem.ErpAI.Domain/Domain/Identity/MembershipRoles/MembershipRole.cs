using CampoSystem.ErpAI.Domain.Identity.Roles;
using CampoSystem.ErpAI.Domain.Identity.TenantMemberships;

namespace CampoSystem.ErpAI.Domain.Identity.MembershipRoles;

public sealed class MembershipRole
{
    public Guid TenantMembershipId { get; private set; }
    public Guid RoleId { get; private set; }
    public Guid TenantId { get; private set; }

    public TenantMembership TenantMembership { get; private set; } = null!;
    public Role Role { get; private set; } = null!;

    private MembershipRole() { }

    internal MembershipRole(TenantMembership membership, Role role)
    {
        TenantMembership = membership;
        Role = role;

        TenantMembershipId = membership.Id;
        RoleId = role.Id;
        TenantId = membership.TenantId;
    }
}