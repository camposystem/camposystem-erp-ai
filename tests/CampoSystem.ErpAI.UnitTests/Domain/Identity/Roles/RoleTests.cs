
using CampoSystem.ErpAI.Domain.Domain.Identity.Permissions;
using CampoSystem.ErpAI.Domain.Domain.Identity.Roles;

namespace CampoSystem.ErpAI.UnitTests.Domain.Identity.Roles;

public  class RoleTests 
{

    [Fact]
    public void CreateRole_ShouldInitializeProperties()
    {
        // Arrange
        var roleName = "Admin";
        // Act
        var role = new Role(roleName);
        // Assert
        Assert.Equal(roleName, role.Name);
    }

    [Fact]
    public void AddPermission_ShouldNotAddPermission_WhenPermissionIsAlreadyAssigned()
    {
        // Arrange
        var role = new Role("Admin");
        var permission = new Permission("ManageUsers");
        // Act
        role.AddPermission(permission);
        
        // Assert
        Assert.Contains(permission, role.Permissions);

        role.AddPermission(permission);

        Assert.Single(role.Permissions);
    }

    [Fact]

    public void RemovePermission_ShouldRemovePermissionFromRole()
    {
        // Arrange
        var role = new Role("Admin");
        var permission = new Permission("ManageUsers");
        role.AddPermission(permission);
        // Act
        role.RemovePermission(permission);
        // Assert
        Assert.DoesNotContain(permission, role.Permissions);
    }
}

