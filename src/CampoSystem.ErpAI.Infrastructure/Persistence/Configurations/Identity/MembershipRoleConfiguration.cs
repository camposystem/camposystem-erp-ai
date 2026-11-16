using CampoSystem.ErpAI.Domain.Identity.MembershipRoles;
using CampoSystem.ErpAI.Domain.Identity.Roles;
using CampoSystem.ErpAI.Domain.Identity.TenantMemberships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampoSystem.ErpAI.Infrastructure.Persistence.Configurations.Identity;

public sealed class MembershipRoleConfiguration
    : IEntityTypeConfiguration<MembershipRole>
{
    public void Configure(EntityTypeBuilder<MembershipRole> builder)
    {
        builder.ToTable("MembershipRoles");

        builder.HasKey(membershipRole => new
        {
            membershipRole.TenantMembershipId,
            membershipRole.RoleId
        });

        builder.Property(membershipRole => membershipRole.TenantId)
            .IsRequired();

        builder.HasOne(membershipRole => membershipRole.TenantMembership)
            .WithMany(membership => membership.MembershipRoles)
            .HasForeignKey(membershipRole => new
            {
                membershipRole.TenantMembershipId,
                membershipRole.TenantId
            })
            .HasPrincipalKey(membership => new
            {
                membership.Id,
                membership.TenantId
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(membershipRole => membershipRole.Role)
            .WithMany()
            .HasForeignKey(membershipRole => new
            {
                membershipRole.RoleId,
                membershipRole.TenantId
            })
            .HasPrincipalKey(role => new
            {
                role.Id,
                role.TenantId
            })
            .OnDelete(DeleteBehavior.Restrict);
    }
}