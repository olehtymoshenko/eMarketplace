using Catalog.Common.Result;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Middleware;

public class GlobalExceptionHandler(IProblemDetailsService _problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetail = exception switch
        {
            Exception => new ProblemDetails()
            {
                Title = Errors.ServerInternalError.Title,
                Detail = Errors.ServerInternalError.Details,
                Type = Errors.MapErrorCodeToUrlReferencingDocsAboutCode(Errors.ServerInternalError.Code),
                Status = (int)HttpStatusCode.InternalServerError
            }
        };


        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetail
        });
    }
}
