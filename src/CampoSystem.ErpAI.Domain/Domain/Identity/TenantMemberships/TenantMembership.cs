using CampoSystem.ErpAI.Domain.Identity.Erros;
using CampoSystem.ErpAI.Domain.Identity.MembershipRoles;
using CampoSystem.ErpAI.Domain.Identity.Roles;
using CampoSystem.ErpAI.SharedKernel.Common.Result;


namespace CampoSystem.ErpAI.Domain.Identity.TenantMemberships;

public sealed class TenantMembership : Entity
{
    public Guid UserId { get; private set; }
    public Guid TenantId { get; private set; }
    private readonly List<MembershipRole> _membershipRoles = new();
    public IReadOnlyCollection<Role> Roles =>
        _membershipRoles.Select(x => x.Role).ToList().AsReadOnly();

    public IReadOnlyCollection<MembershipRole> MembershipRoles =>
    _membershipRoles.AsReadOnly();

    public TenantMembership(Guid userId, Guid tenantId)
    {
        UserId = userId;
        TenantId = tenantId;
    }


    public Result AddRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);

        if (role.TenantId != TenantId)
        {
            return Result.Failure(RuleErrors.InvalidTenant);
        }

        if (!_membershipRoles.Any(x => x.RoleId == role.Id))
        {
            _membershipRoles.Add(new MembershipRole(this, role));
        }


        return Result.Success();
    }


    public void RemoveRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);

        var membershipRole = _membershipRoles
            .FirstOrDefault(x => x.RoleId == role.Id);

        if (membershipRole is not null)
        {
            _membershipRoles.Remove(membershipRole);
        }

    }

    public bool HasPermission(string permissionName)
    {
      return _membershipRoles.Any(membershipRole =>
          membershipRole.Role.Permissions.Any(permission =>
                string.Equals(
                    permission.Name,
                    permissionName,
                    StringComparison.OrdinalIgnoreCase)
                && permission.IsActive));

    }

    public bool HasPermissions(IEnumerable<string> permissionNames)
    {
        ArgumentNullException.ThrowIfNull(permissionNames);

        var requiredPermissions = permissionNames.ToList();

        if (requiredPermissions.Count == 0)
            return false;


        return requiredPermissions.All(HasPermission);
    }

}
