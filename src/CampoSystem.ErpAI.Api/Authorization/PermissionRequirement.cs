using Microsoft.AspNetCore.Authorization;

namespace CampoSystem.ErpAI.Api.Authorization;

public sealed class PermissionRequirement : IAuthorizationRequirement
{
    public IReadOnlyCollection<string> PermissionNames { get; }

    public PermissionRequirement(params string[] permissionNames)
    {
        ArgumentNullException.ThrowIfNull(permissionNames);

        if (permissionNames.Length == 0 ||
            permissionNames.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException(
                "Pelo menos uma permissão válida deve ser informada.",
                nameof(permissionNames));
        }

        PermissionNames = Array.AsReadOnly(permissionNames);
    }
}
