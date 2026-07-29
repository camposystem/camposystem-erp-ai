namespace CampoSystem.ErpAI.SharedKernel.Result;

public class Error
{
    public static Error None { get; } = new Error(string.Empty, null, string.Empty);

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
