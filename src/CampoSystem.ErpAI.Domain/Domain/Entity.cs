namespace CampoSystem.ErpAI.Domain.Domain;

public class Entity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Entity()
    {
        this.Id = Guid.NewGuid();
        this.CreatedAt = DateTime.UtcNow;
    }
}
