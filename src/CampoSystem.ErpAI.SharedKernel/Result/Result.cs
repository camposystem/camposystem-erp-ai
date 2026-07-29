namespace CampoSystem.ErpAI.SharedKernel.Result;

public class Result
{

    public bool IsSuccess { get; private set; }
    public bool IsFailure => !this.IsSuccess;
    public IReadOnlyCollection<Error> Errors { get; private set; }

    public bool IsVariant { get; private set; }


    protected Result(bool isSuccess, List<Error> errors)
    {
        SetIsVariant(errors, isSuccess);

        if ((isSuccess ||  !isSuccess) && IsVariant)
        {
            throw new InvalidOperationException("Objeto com valores inconcistentes.");
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true, new List<Error> { Error.None });
    }

    public static Result Failure(Error error)
    {
        return new Result(false, new List<Error> { Error.None });
    }

    private void SetIsVariant(List<Error> errors, bool isSuccess)
    {
        
        if (isSuccess)
            this.IsVariant = errors.Where(e => !string.IsNullOrEmpty(e.Message)).Count() > 0;
        if (!isSuccess)
            this.IsVariant = errors.Where(e => string.IsNullOrEmpty(e.Message)).Count() > 0;

    }
}


public class Result<T> : Result
{
    private readonly T _value;

    public T Value => _value;

    protected Result(T value, bool isSuccess, List<Error> errors)
        : base(isSuccess, errors)
    {
        _value = value;

    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, true, new List<Error> { Error.None });
    }

    public static Result<T> Failure(List<Error> errors)
    {
        return new Result<T>(default!, false, errors);
    }
}
