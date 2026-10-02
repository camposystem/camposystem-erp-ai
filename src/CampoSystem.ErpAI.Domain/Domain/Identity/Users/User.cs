using CampoSystem.ErpAI.Domain.Domain.Identity.Roles;

namespace CampoSystem.ErpAI.Domain.Domain.Identity.User;

public sealed class User : Entity
{
    public string ExternalIdentityId { get; private set; }

    private readonly List<Role> _roles = new();

    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    public User(string externalIdentityId)
    {
        ExternalIdentityId = externalIdentityId;
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
