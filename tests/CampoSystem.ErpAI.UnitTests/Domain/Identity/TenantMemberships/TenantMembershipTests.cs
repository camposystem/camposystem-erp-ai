using CampoSystem.ErpAI.Domain.Identity.Permissions;
using CampoSystem.ErpAI.Domain.Identity.Roles;
using CampoSystem.ErpAI.Domain.Identity.TenantMemberships;
using CampoSystem.ErpAI.Domain.Identity.Users;

namespace CampoSystem.ErpAI.UnitTests.Domain.Identity.TenantMemberships;

public class TenantMembershipTests
{
    [Fact]
    public void CreateMembership_ShouldInitializeIds()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tenantId = Guid.NewGuid();

        // Act
        var membership = new TenantMembership(userId, tenantId);

        // Assert
        Assert.Equal(userId, membership.UserId);
        Assert.Equal(tenantId, membership.TenantId);
    }

    [Fact]
    public void AddRole_ShouldAddRoleToMembership()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        var role = new Role("Test Role");

        // Act
        var membership = new TenantMembership(userId, tenantId);
        membership.AddRole(role);

        // Assert
        Assert.Contains(role, membership.Roles);
    }

    [Fact]
    public void AddRole_ShouldNotAddRole_WhenRoleIsAlreadyAssigned()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        var role = new Role("Test Role");
        // Act
        var membership = new TenantMembership(userId, tenantId);
        membership.AddRole(role);
        membership.AddRole(role);
        // Assert
        Assert.Single(membership.Roles);
    }

    [Fact]
    public void RemoveRole_ShouldRemoveRoleFromMembership()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        var role = new Role("Test Role");

        // Act
        var membership = new TenantMembership(userId, tenantId);
        membership.AddRole(role);
        membership.RemoveRole(role);

        // Assert
        Assert.DoesNotContain(role, membership.Roles);
    }

    [Fact]
    public void AddTenantMembership_ShouldNotAddMembership_WhenUserAlreadyBelongsToTenant()
    {
        // Arrange
        var user = new User("external-id-123");
        var tenantId = Guid.NewGuid();

        var membership1 = new TenantMembership(user.Id, tenantId);
        var membership2 = new TenantMembership(user.Id, tenantId);

        // Act
        user.AddTenantMembership(membership1);
        user.AddTenantMembership(membership2);

        // Assert
        Assert.Single(user.TenantMemberships);
    }

    [Fact]
    public void HasPermission_ShouldReturnTrue_WhenRoleHasPermission()
    {
        // Arrange
        var membership = new TenantMembership(Guid.NewGuid(), Guid.NewGuid());
        var role = new Role("Administrator");
        var permission = new Permission("products.create");

        role.AddPermission(permission);
        membership.AddRole(role);

        // Act
        var hasPermission = membership.HasPermission("products.create");

        // Assert
        Assert.True(hasPermission);
    }

    [Fact]
    public void HasPermission_ShouldReturnFalse_WhenNoRoleHasPermission()
    {
        // Arrange
        var membership = new TenantMembership(Guid.NewGuid(), Guid.NewGuid());
        var role = new Role("Viewer");

        role.AddPermission(new Permission("products.list"));
        membership.AddRole(role);

        // Act
        var hasPermission = membership.HasPermission("products.create");

        // Assert
        Assert.False(hasPermission);
    }

    [Fact]
    public void HasPermission_ShouldReturnFalse_WhenMembershipHasNoRoles()
    {
        // Arrange
        var membership = new TenantMembership(Guid.NewGuid(), Guid.NewGuid());

        // Act
        var hasPermission = membership.HasPermission("products.create");

        // Assert
        Assert.False(hasPermission);
    }

}