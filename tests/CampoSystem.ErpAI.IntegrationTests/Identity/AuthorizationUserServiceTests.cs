
using CampoSystem.ErpAI.Domain.Identity.Permissions;
using CampoSystem.ErpAI.Domain.Identity.Roles;
using CampoSystem.ErpAI.Domain.Identity.TenantMemberships;
using CampoSystem.ErpAI.Domain.Identity.Tenants;
using CampoSystem.ErpAI.Domain.Identity.Users;
using CampoSystem.ErpAI.Infrastructure.Identity.Authorization;
using CampoSystem.ErpAI.Infrastructure.Persistence;
using CampoSystem.ErpAI.IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CampoSystem.ErpAI.IntegrationTests.Identity;

public class AuthorizationUserServiceTests
    : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _databaseFixture;

    public AuthorizationUserServiceTests(
        DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
    }

    [Fact]
    public async Task Given_A_User_Has_Products_Create_Permission_Should_Return_True()
    {
        // Arrange
        await _databaseFixture.ResetDatabaseAsync();

        var options = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(
                _databaseFixture.ConnectionString,
                npgsql => npgsql.MigrationsHistoryTable(
                    "__EFMigrationsHistory",
                    "identity"))
            .Options;

        await using var context = new IdentityDbContext(options);

        var externalIdentityId = "auth0|6af384f4513f953dd3d19bb9";
        var tenantExternalId = "org_BHjLu9csh8D5qZGc";

        var tenant = new Tenant(
            tenantExternalId,
            "Tenant Integration Test");

        var user = new User(externalIdentityId);

        var membership = new TenantMembership(
            user.Id,
            tenant.Id);

        var role = new Role(
            tenant.Id,
            "Product Administrator");

        var permission = new Permission("products.create");

        role.AddPermission(permission);

        var addRoleResult = membership.AddRole(role);

        Assert.True(addRoleResult.IsSuccess);

        user.AddTenantMembership(membership);

        context.Tenants.Add(tenant);
        context.Users.Add(user);
        context.Roles.Add(role);
        context.Permissions.Add(permission);

        await context.SaveChangesAsync();

        var authorizationUserService =
            new AuthorizationUserService(context);

        // Act
        var result = await authorizationUserService.HasPermissionsAsync(
            externalIdentityId,
            tenantExternalId,
            ["products.create"]);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public async Task Given_A_User_Does_Not_Have_Products_Create_Permission_Should_Return_False()
    {
        // Arrange
        await _databaseFixture.ResetDatabaseAsync();

        var options = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(
                _databaseFixture.ConnectionString,
                npgsql => npgsql.MigrationsHistoryTable(
                    "__EFMigrationsHistory",
                    "identity"))
            .Options;

        await using var context = new IdentityDbContext(options);

        var externalIdentityId = "auth0|user-without-permission";
        var tenantExternalId = "org-without-permission";

        var tenant = new Tenant(tenantExternalId, "Tenant Test");
        var user = new User(externalIdentityId);

        var membership = new TenantMembership(user.Id, tenant.Id);
        var role = new Role(tenant.Id, "Product Viewer");

        var permission = new Permission("products.read");

        role.AddPermission(permission);
        membership.AddRole(role);
        user.AddTenantMembership(membership);

        context.Tenants.Add(tenant);
        context.Users.Add(user);
        context.Roles.Add(role);
        context.Permissions.Add(permission);

        await context.SaveChangesAsync();

        var service = new AuthorizationUserService(context);

        // Act
        var result = await service.HasPermissionsAsync(
            externalIdentityId,
            tenantExternalId,
            ["products.create"]);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Given_A_User_Belongs_To_Another_Tenant_Should_Return_False()
    {
        // Arrange
        await _databaseFixture.ResetDatabaseAsync();

        var options = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(
                _databaseFixture.ConnectionString,
                npgsql => npgsql.MigrationsHistoryTable(
                    "__EFMigrationsHistory",
                    "identity"))
            .Options;

        await using var context = new IdentityDbContext(options);

        var externalIdentityId = "auth0|tenant-isolation-user";

        var tenantA = new Tenant("org-tenant-a", "Tenant A");
        var tenantB = new Tenant("org-tenant-b", "Tenant B");

        var user = new User(externalIdentityId);

        var membership = new TenantMembership(user.Id, tenantA.Id);
        var role = new Role(tenantA.Id, "Product Administrator");

        var permission = new Permission("products.create");

        role.AddPermission(permission);
        membership.AddRole(role);
        user.AddTenantMembership(membership);

        context.Tenants.AddRange(tenantA, tenantB);
        context.Users.Add(user);
        context.Roles.Add(role);
        context.Permissions.Add(permission);

        await context.SaveChangesAsync();

        var service = new AuthorizationUserService(context);

        // Act
        var result = await service.HasPermissionsAsync(
            externalIdentityId,
            tenantB.ExternalId,
            ["products.create"]);

        // Assert
        Assert.False(result);
    }

}
