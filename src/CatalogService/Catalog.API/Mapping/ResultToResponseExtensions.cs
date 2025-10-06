using Catalog.Common.Result;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Mapping;

public static class ResultToResponseExtensions
{
    public static IResult MapToResponse(this Error error)
    {
        if (error == null)
        {
            return Results.Ok();
        }

        return error switch
        {
            ValidationError err => Results.ValidationProblem(
                errors: err.ModelStateErrors, 
                detail: err.Details, 
                title: err.Title, 
                type: Errors.MapErrorCodeToUrlReferencingDocsAboutCode(err.Code)),

            RequestError => Results.Problem(new ProblemDetails()
            {
                Title = error.Title,
                Detail = error.Details,
                Type = Errors.MapErrorCodeToUrlReferencingDocsAboutCode(error.Code),
                Status = (int)HttpStatusCode.BadRequest
            }),

            _ => Results.Problem(new ProblemDetails()
            {
                Title = error.Title,
                Detail = error.Details,
                Type = Errors.MapErrorCodeToUrlReferencingDocsAboutCode(error.Code),
                Status = (int)HttpStatusCode.InternalServerError
            })
        };
    }

}
