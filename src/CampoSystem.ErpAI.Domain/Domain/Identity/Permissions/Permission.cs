namespace CampoSystem.ErpAI.Domain.Identity.Permissions;

public sealed class Permission : Entity
{
    public string Name { get; private set; }

    public bool IsActive { get; private set; }

    public Permission(string name)
    {
        Name = name;
        IsActive = true;
    }
    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
