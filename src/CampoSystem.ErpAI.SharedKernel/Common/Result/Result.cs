namespace CampoSystem.ErpAI.SharedKernel.Common.Result;

public class Result
{
    private readonly List<Error> _errors;

    public bool IsSuccess { get; }
    public bool IsFailure => !this.IsSuccess;
    public IReadOnlyCollection<Error> Errors => _errors.AsReadOnly();

    protected Result(bool isSuccess, IEnumerable<Error> errors)
    {
      
            ArgumentNullException.ThrowIfNull(errors);
 
            var errorList = errors.ToList();

            if (isSuccess && errorList.Any() ||
                !isSuccess && !errorList.Any())
            {
                throw new InvalidOperationException("Objeto com valores inconsistentes.");
            }

            IsSuccess = isSuccess;
            _errors = errorList;
        
    }

    public static Result Success()
    {
        return new Result(true, []);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, [error]);
    }

}


public class Result<T> : Result
{
    private readonly T _value;

    public T Value => IsSuccess
                    ? _value
                    : throw new InvalidOperationException("Você tentou acessar Value quando este Result está em Failure.");

    protected Result(T value, bool isSuccess, IEnumerable<Error> errors)
        : base(isSuccess, errors)
    {

        if (isSuccess && value is null)
        {
            throw new InvalidOperationException("Objeto com valores inconsistentes.");
        }

        _value = value;

    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, true, []);
    }

    public static Result<T> Failure(IEnumerable<Error> errors)
    {
        return new Result<T>(default!, false, errors);
    }
}
