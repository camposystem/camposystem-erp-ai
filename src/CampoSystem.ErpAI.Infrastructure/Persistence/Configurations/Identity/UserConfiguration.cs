
using CampoSystem.ErpAI.Domain.Identity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampoSystem.ErpAI.Infrastructure.Persistence.Configurations.Identity;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedNever();

        builder.Property(user => user.ExternalIdentityId)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(user => user.ExternalIdentityId)
            .IsUnique();

        builder.Property(user => user.CreatedAt)
            .IsRequired();

        builder.HasMany(user => user.TenantMemberships)
            .WithOne()
            .HasForeignKey(membership => membership.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
