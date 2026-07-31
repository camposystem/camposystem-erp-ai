namespace CampoSystem.ErpAI.SharedKernel.Result;

public sealed class Error
{
    public string Code { get; }
    public string? Field { get; }
    public string Message { get; }

    public Error(string code, string? field, string message)
    {
        
            Code = code;
            Field = field;
            Message = message;
        
    }
}
