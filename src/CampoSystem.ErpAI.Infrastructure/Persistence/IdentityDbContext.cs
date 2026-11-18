
using CampoSystem.ErpAI.Domain.Identity.Permissions;
using CampoSystem.ErpAI.Domain.Identity.Roles;
using CampoSystem.ErpAI.Domain.Identity.TenantMemberships;
using CampoSystem.ErpAI.Domain.Identity.Tenants;
using CampoSystem.ErpAI.Domain.Identity.Users;
using CampoSystem.ErpAI.Infrastructure.Persistence.Configurations.Identity;
using Microsoft.EntityFrameworkCore;

namespace CampoSystem.ErpAI.Infrastructure.Persistence;

public sealed class IdentityDbContext : DbContext
{
    public IdentityDbContext(
        DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Tenant> Tenants => Set<Tenant>();

    public DbSet<TenantMembership> TenantMemberships =>
        Set<TenantMembership>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<Permission> Permissions => Set<Permission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("identity");

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TenantConfiguration());
        modelBuilder.ApplyConfiguration(new TenantMembershipConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new MembershipRoleConfiguration());
    }
}
