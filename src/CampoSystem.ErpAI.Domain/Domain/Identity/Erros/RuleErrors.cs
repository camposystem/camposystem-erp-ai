using CampoSystem.ErpAI.Domain.Identity.Roles;
using CampoSystem.ErpAI.SharedKernel.Common.Result;

namespace CampoSystem.ErpAI.Domain.Identity.Erros;

public static class RuleErrors
{

    public static readonly Error InvalidTenant = new(
        code: "IDENTITY.ROLE.TENANT_MISMATCH",
        field: nameof(Role.TenantId),
        message: "A função não pertence à mesma empresa da associação."
    );

}
