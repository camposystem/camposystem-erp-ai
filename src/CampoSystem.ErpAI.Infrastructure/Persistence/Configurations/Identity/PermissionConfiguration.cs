
using CampoSystem.ErpAI.Domain.Identity.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampoSystem.ErpAI.Infrastructure.Persistence.Configurations.Identity;

public sealed class PermissionConfiguration
    : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(permission => permission.Id);

        builder.Property(permission => permission.Id)
            .ValueGeneratedNever();

        builder.Property(permission => permission.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(permission => permission.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(permission => permission.CreatedAt)
            .IsRequired();

        builder.HasIndex(permission => permission.Name)
            .IsUnique();
    }
}
