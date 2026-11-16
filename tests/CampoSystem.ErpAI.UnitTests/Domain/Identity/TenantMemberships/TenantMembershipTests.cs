
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
        var userId = Guid.NewGuid();
        var tenantId = Guid.NewGuid();

        var membership = new TenantMembership(userId, tenantId);

        Assert.Equal(userId, membership.UserId);
        Assert.Equal(tenantId, membership.TenantId);
    }

    [Fact]
    public void AddRole_ShouldAddRole_WhenRoleBelongsToSameTenant()
    {
        var tenantId = Guid.NewGuid();
        var membership = new TenantMembership(Guid.NewGuid(), tenantId);
        var role = new Role(tenantId, "Test Role");

        var result = membership.AddRole(role);

        Assert.True(result.IsSuccess);
        Assert.Contains(role, membership.Roles);
    }

    [Fact]
    public void AddRole_ShouldNotAddRole_WhenRoleBelongsToDifferentTenant()
    {
        var membershipTenantId = Guid.NewGuid();
        var roleTenantId = Guid.NewGuid();

        var membership = new TenantMembership(
            Guid.NewGuid(), membershipTenantId);
        var role = new Role(roleTenantId, "Test Role");

        var result = membership.AddRole(role);

        Assert.True(result.IsFailure);
        Assert.Contains(
            result.Errors,
            error => error.Code == "IDENTITY.ROLE.TENANT_MISMATCH");
        Assert.Empty(membership.Roles);
    }

    [Fact]
    public void AddRole_ShouldNotDuplicateRole_WhenRoleIsAlreadyAssigned()
    {
        var tenantId = Guid.NewGuid();
        var membership = new TenantMembership(Guid.NewGuid(), tenantId);
        var role = new Role(tenantId, "Test Role");

        var firstResult = membership.AddRole(role);
        var secondResult = membership.AddRole(role);

        Assert.True(firstResult.IsSuccess);
        Assert.True(secondResult.IsSuccess);
        Assert.Single(membership.Roles);
    }

    [Fact]
    public void RemoveRole_ShouldRemoveRoleFromMembership()
    {
        var tenantId = Guid.NewGuid();
        var membership = new TenantMembership(Guid.NewGuid(), tenantId);
        var role = new Role(tenantId, "Test Role");

        membership.AddRole(role);
        membership.RemoveRole(role);

        Assert.DoesNotContain(role, membership.Roles);
    }

    [Fact]
    public void AddTenantMembership_ShouldNotAddMembership_WhenUserAlreadyBelongsToTenant()
    {
        var user = new User("external-id-123");
        var tenantId = Guid.NewGuid();

        var membership1 = new TenantMembership(user.Id, tenantId);
        var membership2 = new TenantMembership(user.Id, tenantId);

        user.AddTenantMembership(membership1);
        user.AddTenantMembership(membership2);

        Assert.Single(user.TenantMemberships);
    }

    [Fact]
    public void HasPermission_ShouldReturnTrue_WhenRoleHasPermission()
    {
        var tenantId = Guid.NewGuid();
        var membership = new TenantMembership(Guid.NewGuid(), tenantId);
        var role = new Role(tenantId, "Administrator");
        var permission = new Permission("products.create");

        role.AddPermission(permission);
        var result = membership.AddRole(role);

        Assert.True(result.IsSuccess);
        Assert.True(membership.HasPermission("products.create"));
    }

    [Fact]
    public void HasPermission_ShouldReturnFalse_WhenRoleDoesNotHaveRequestedPermission()
    {
        var tenantId = Guid.NewGuid();
        var membership = new TenantMembership(Guid.NewGuid(), tenantId);
        var role = new Role(tenantId, "Viewer");

        role.AddPermission(new Permission("products.list"));
        membership.AddRole(role);

        Assert.False(membership.HasPermission("products.create"));
    }

    [Fact]
    public void HasPermission_ShouldReturnFalse_WhenMembershipHasNoRoles()
    {
        var membership = new TenantMembership(Guid.NewGuid(), Guid.NewGuid());

        Assert.False(membership.HasPermission("products.create"));
    }

    [Fact]
    public void HasPermission_ShouldReturnFalse_WhenPermissionIsInactive()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var membership = new TenantMembership(Guid.NewGuid(), tenantId);
        var role = new Role(tenantId, "Administrator");
        var permission = new Permission("products.create");

        permission.Deactivate();
        role.AddPermission(permission);
        membership.AddRole(role);

        // Act
        var hasPermission = membership.HasPermission("products.create");

        // Assert
        Assert.False(hasPermission);
    }

    [Fact]
    public void HasPermission_ShouldReturnTrue_WhenPermissionIsActivated()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var membership = new TenantMembership(Guid.NewGuid(), tenantId);
        var role = new Role(tenantId, "Administrator");
        var permission = new Permission("products.create");

        permission.Deactivate();
        permission.Activate();

        role.AddPermission(permission);
        membership.AddRole(role);

        // Act
        var hasPermission = membership.HasPermission("products.create");

        // Assert
        Assert.True(hasPermission);
    }

    [Fact]
    public void HasPermissions_ShouldReturnTrue_WhenAllPermissionsExist()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var membership = new TenantMembership(Guid.NewGuid(), tenantId);
        var role = new Role(tenantId, "Administrator");

        role.AddPermission(new Permission("products.create"));
        role.AddPermission(new Permission("products.update"));

        membership.AddRole(role);
        // Act
        var result = membership.HasPermissions(
            new[] { "products.create", "products.update" });

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void HasPermissions_ShouldReturnFalse_WhenOnePermissionIsMissing()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var membership = new TenantMembership(Guid.NewGuid(), tenantId);
        var role = new Role(tenantId, "Operator");

        role.AddPermission(new Permission("products.create"));

        membership.AddRole(role);
        // Act
        var result = membership.HasPermissions(
            new[] { "products.create", "products.delete" });

        // Assert
        Assert.False(result);
    }
    [Fact]
    public void HasPermissions_ShouldReturnFalse_WhenNoPermissionsExist()
    {
        // Arrange
        var membership = new TenantMembership(Guid.NewGuid(), Guid.NewGuid());

        // Act
        var result = membership.HasPermissions(
            new[] { "products.create", "products.update" });

        // Assert
        Assert.False(result);
    }

}
