
using CampoSystem.ErpAI.Domain.Identity.Permissions;
using CampoSystem.ErpAI.Domain.Identity.Roles;
using CampoSystem.ErpAI.Domain.Identity.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampoSystem.ErpAI.Infrastructure.Persistence.Configurations.Identity;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(role => role.Id);

        builder.HasAlternateKey(role => new
        {
            role.Id,
            role.TenantId
        });

        builder.Property(role => role.Id)
            .ValueGeneratedNever();

        builder.Property(role => role.TenantId)
            .IsRequired();

        builder.Property(role => role.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(role => role.CreatedAt)
            .IsRequired();

        builder.HasIndex(role => new
        {
            role.TenantId,
            role.Name
        })
        .IsUnique();

        builder.HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(role => role.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(role => role.Permissions)
    .WithMany()
    .UsingEntity<Dictionary<string, object>>(
        "RolePermissions",
        right => right
            .HasOne<Permission>()
            .WithMany()
            .HasForeignKey("PermissionId")
            .OnDelete(DeleteBehavior.Restrict),
        left => left
            .HasOne<Role>()
            .WithMany()
            .HasForeignKey("RoleId")
            .OnDelete(DeleteBehavior.Cascade),
        join =>
        {
            join.ToTable("RolePermissions");
            join.HasKey("RoleId", "PermissionId");
        });
    }
}
