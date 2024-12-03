namespace Catalog.Common.Result;

public record Error(int Code, string Title, string Details) { }

public record RequestError(int Code, string Title, string Details) : Error(Code, Title, Details);

public record ValidationError(int Code, string Title, string Details, IDictionary<string, string[]> ModelStateErrors) : Error(Code, Title, Details);




public static class Errors
{
    // Internal errors start with 100
    public static readonly Error ServerInternalError = new(100, "Unexpected internal server error", "Unexpected internal server error occured. Please contact our support team via our website");


    // Request-related errors start with 1000
    public static RequestError RequestGenericError(string details = "The request caused an unexpected error on the server") => new(1000, "Unexpected request", details);
    
    public static ValidationError RequestValidationError(IDictionary<string, string[]> modelStateErrors, string details = "Please refer to the errors property for details") => 
        new ValidationError(2001, "Validation error", details, modelStateErrors);



    #region Util methods

    public static string MapErrorCodeToUrlReferencingDocsAboutCode(int code) => $"https://domain.dev.com/help/errors/{code}";

    #endregion
}