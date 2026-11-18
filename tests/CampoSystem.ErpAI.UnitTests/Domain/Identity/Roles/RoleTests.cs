
using CampoSystem.ErpAI.Domain.Identity.Permissions;
using CampoSystem.ErpAI.Domain.Identity.Roles;

namespace CampoSystem.ErpAI.UnitTests.Domain.Identity.Roles;

public class RoleTests
{
    [Fact]
    public void CreateRole_ShouldInitializeProperties()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var roleName = "Admin";

        // Act
        var role = new Role(tenantId, roleName);

        // Assert
        Assert.Equal(tenantId, role.TenantId);
        Assert.Equal(roleName, role.Name);
    }

    [Fact]
    public void AddPermission_ShouldNotAddPermission_WhenPermissionIsAlreadyAssigned()
    {
        // Arrange
        var role = new Role(Guid.NewGuid(), "Admin");
        var permission = new Permission("ManageUsers");

        // Act
        role.AddPermission(permission);
        role.AddPermission(permission);

        // Assert
        Assert.Single(role.Permissions);
    }

    [Fact]
    public void RemovePermission_ShouldRemovePermissionFromRole()
    {
        // Arrange
        var role = new Role(Guid.NewGuid(), "Admin");
        var permission = new Permission("ManageUsers");
        role.AddPermission(permission);

        // Act
        role.RemovePermission(permission);

        // Assert
        Assert.DoesNotContain(permission, role.Permissions);
    }
}
