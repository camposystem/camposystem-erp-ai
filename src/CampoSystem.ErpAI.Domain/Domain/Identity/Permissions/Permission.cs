namespace CampoSystem.ErpAI.Domain.Domain.Identity.Permissions;

public sealed class Permission : Entity
{
    public string Name { get; private set; }

    public Permission(string name)
    {
        Name = name;
    }
}
