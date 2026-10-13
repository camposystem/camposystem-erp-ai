using CampoSystem.ErpAI.Domain.Identity.Roles;
using CampoSystem.ErpAI.Domain.Identity.TenantMemberships;
using CampoSystem.ErpAI.Domain.Identity.Users;

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
    public void AddTenantMembership_ShouldAddMembership_WhenMembershipIsNotAlreadyAssigned()
    {
        // Arrange
        var user = new User("external-id-123");
        var membership = new TenantMembership(Guid.NewGuid(),Guid.NewGuid());
        // Act
        user.AddTenantMembership(membership);
        // Assert
        Assert.Contains(membership, user.TenantMemberships);
    }

    [Fact]
    public void AddTenantMembership_ShouldNotAddMembership_WhenMembershipIsAlreadyAssigned()
    {
        // Arrange
        var user = new User("external-id-123");
        var membership = new TenantMembership(Guid.NewGuid(), Guid.NewGuid());          

        user.AddTenantMembership(membership);
        // Act
        user.AddTenantMembership(membership);
        // Assert
        Assert.Single(user.TenantMemberships);
    }

    [Fact]
    public void RemoveTenantMembership_ShouldRemoveMembership_WhenMembershipIsAssigned()
    {
        // Arrange
        var user = new User("external-id-123");
        var membership = new TenantMembership(Guid.NewGuid(), Guid.NewGuid());
        user.AddTenantMembership(membership);
        // Act
        user.RemoveTenantMembership(membership);
        // Assert
        Assert.DoesNotContain(membership, user.TenantMemberships);
    }   


}
