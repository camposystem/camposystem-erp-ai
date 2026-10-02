using CampoSystem.ErpAI.Domain.Domain.Identity.Roles;
using CampoSystem.ErpAI.Domain.Domain.Identity.User;

namespace CampoSystem.ErpAI.UnitTests.Domain.Identity.Users;

public  class UserTests
{
    [Fact]
    public void Constructor_ShouldInitializeUserWithExternalIdentityId()
    {
        // Arrange
        var externalIdentityId = "external-id-123";
        // Act
        var user = new User(externalIdentityId);
        // Assert
        Assert.Equal(externalIdentityId, user.ExternalIdentityId);
    }

    [Fact]
    public void AddRole_ShouldAddRole_WhenRoleIsNotAlreadyAssigned()
    {
        // Arrange
        var user = new User("external-id-123");
        var role = new Role("Admin");
        // Act
        user.AddRole(role);
        // Assert
        Assert.Contains(role, user.Roles);
    }

    [Fact]
    public void AddRole_ShouldNotAddRole_WhenRoleIsAlreadyAssigned()
    {
        // Arrange
        var user = new User("external-id-123");
        var role = new Role("Admin");
        user.AddRole(role);
        // Act
        user.AddRole(role);
        // Assert
        Assert.Single(user.Roles);
    }

    [Fact]
    public void RemoveRole_ShouldRemoveRole_WhenRoleIsAssigned()
    {
        // Arrange
        var user = new User("external-id-123");
        var role = new Role("Admin");
        user.AddRole(role);
        // Act
        user.RemoveRole(role);
        // Assert
        Assert.DoesNotContain(role, user.Roles);
    }   


}
