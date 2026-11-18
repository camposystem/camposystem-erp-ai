
using CampoSystem.ErpAI.Domain.Identity.TenantMemberships;
using CampoSystem.ErpAI.Domain.Identity.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampoSystem.ErpAI.Infrastructure.Persistence.Configurations.Identity;

public sealed class TenantMembershipConfiguration
    : IEntityTypeConfiguration<TenantMembership>
{
    public void Configure(EntityTypeBuilder<TenantMembership> builder)
    {
        builder.ToTable("TenantMemberships");

        builder.HasKey(membership => membership.Id);

        builder.HasAlternateKey(membership => new
        {
            membership.Id,
            membership.TenantId
        });

        builder.Property(membership => membership.Id)
            .ValueGeneratedNever();

        builder.Property(membership => membership.UserId)
            .IsRequired();

        builder.Property(membership => membership.TenantId)
            .IsRequired();

        builder.Property(membership => membership.CreatedAt)
            .IsRequired();

        builder.HasIndex(membership => new
        {
            membership.UserId,
            membership.TenantId
        })
            .IsUnique();

        builder.HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(membership => membership.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(membership => membership.Roles);

        builder.Navigation(membership => membership.MembershipRoles)
    .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
