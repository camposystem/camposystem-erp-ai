using CampoSystem.ErpAI.Domain.Finances.Enums;

namespace CampoSystem.ErpAI.Domain.Domain.Finances.Enums.Extensions;

public static class TimeUnitExtensions
{

    extension(TimeUnit timeUnit)
    {
        public int ToFactor()
        {
            return (int)timeUnit;
        }
    }
}