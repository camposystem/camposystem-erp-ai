using CampoSystem.ErpAI.Domain.Domain.Identity.Permissions;

namespace CampoSystem.ErpAI.Domain.Domain.Identity.Roles;

public sealed class Role : Entity
{

    private readonly List<Permission> _permissions = new();
    public string Name { get; private set; }

    public IReadOnlyCollection<Permission> Permissions =>
        _permissions.AsReadOnly();

    public Role(string name)
    {
        Name = name;
    }

    public void AddPermission(Permission permission)
    {
        if (!_permissions.Contains(permission))
        {
            _permissions.Add(permission);
        }
    }

    public void RemovePermission(Permission permission)
    {
        if (_permissions.Contains(permission))
        {
            _permissions.Remove(permission);
        }
    }
}
