
using CampoSystem.ErpAI.Domain.Identity.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampoSystem.ErpAI.Infrastructure.Persistence.Configurations.Identity;

public sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");

        builder.HasKey(tenant => tenant.Id);

        builder.Property(tenant => tenant.Id)
            .ValueGeneratedNever();

        builder.Property(tenant => tenant.ExternalId)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(tenant => tenant.ExternalId)
            .IsUnique();

        builder.Property(tenant => tenant.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(tenant => tenant.CreatedAt)
            .IsRequired();
    }
}
