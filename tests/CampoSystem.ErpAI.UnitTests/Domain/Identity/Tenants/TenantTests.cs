using CampoSystem.ErpAI.Domain.Identity.Tenants;

namespace CampoSystem.ErpAI.UnitTests.Domain.Identity.Tenants;

public class TenantTests
{
    [Fact]
    public void CreateTenant_ShouldCreateTenantSuccessfully()
    {
        // Arrange
        var externalId = Guid.NewGuid().ToString();
        var tenantName = "Test Tenant";
        // Act
        var tenant = new Tenant(externalId, tenantName);
        // Assert
        Assert.Equal(externalId, tenant.ExternalId);
        Assert.Equal(tenantName, tenant.Name);
    }
}