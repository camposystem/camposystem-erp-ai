using System;
using System.Collections.Generic;
using System.Text;

namespace CampoSystem.ErpAI.SharedKernel.Common.Exceptions;

public class MoneyOverflowException : MoneyException
{
    public MoneyOverflowException(string message) : base(message)
    {
    }
}
