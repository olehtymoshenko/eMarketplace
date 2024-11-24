namespace Catalog.Common.Result;
public record struct ResultVoid
{
    public bool IsSuccess { get; private set; }

    public bool IsFailure => !IsSuccess;

    public Error? Error { get; private set; }


    private ResultVoid(bool isSuccess, Error? error)
    {
        IsSuccess = false;
        Error = error;  
    }


    public static ResultVoid SuccessVoid() => new ResultVoid(true, null);

    public static ResultVoid FailureVoid(Error error) => new ResultVoid(false, error);
}
