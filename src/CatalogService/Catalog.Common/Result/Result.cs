namespace Catalog.Common.Result;
public record struct Result<TValue>
{
    private readonly bool _success;
    private readonly TValue? _value;
    private readonly Error? _error;

    public bool IsSuccess => _success;
    public bool IsFailure => !_success;

    public TValue? Value
    {
        get => IsSuccess
            ? _value
            : throw new Exception($"Cannot access {nameof(Value)} on failure result");
    }
    public Error Error
    {
        get => IsFailure 
            ? _error ?? throw new Exception("Failure result must contain an error")
            : throw new Exception($"Cannot access {nameof(Error)} on success result");
    }

    private Result(bool isSuccess, TValue? value, Error? error)
    {
        _success = isSuccess;
        if(isSuccess)
        {
            _value = value;
            _error = default;
        }
        else
        {
            _value = default;
            _error = error;
        }
    }


    public static Result<TValue> Success(TValue value) => new Result<TValue>(true, value, default);
    
    public static Result<TValue> Failure(Error error) => new Result<TValue>(false, default, error);

    public static implicit operator Result<TValue>(TValue value) => new Result<TValue>(true, value, default); 
    
    public static implicit operator Result<TValue>(Error error) => new Result<TValue>(false, default, error); 

    public TResult Match<TResult>(Func<TValue?, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(Error);
    }
}
