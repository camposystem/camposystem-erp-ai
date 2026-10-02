using CampoSystem.ErpAI.Domain.Domain.Identity.Permissions;

namespace CampoSystem.ErpAI.UnitTests.Domain.Identity.Permissions;

public  class PermissionTests
{

    [Fact]
    public void Permission_Ctor_Initializes_Name()
    {
        // Arrange
        string name = "TestPermission";

        // Act
        var permission = new Permission(name);

        // Assert
        Assert.Equal(name, permission.Name);
    }
}
