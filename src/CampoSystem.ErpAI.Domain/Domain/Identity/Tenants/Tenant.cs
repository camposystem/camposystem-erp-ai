namespace CampoSystem.ErpAI.Domain.Identity.Tenants;

public sealed class Tenant : Entity
{
    public string ExternalId { get; private set; }
    public string Name { get; private set; }

    public Tenant(string externalId, string name)
    {
        ExternalId = externalId;
        Name = name;
    }
}