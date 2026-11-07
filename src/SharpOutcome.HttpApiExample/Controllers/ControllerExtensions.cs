using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SharpOutcome.Helpers;
using SharpOutcome.Helpers.Enums;
using SharpOutcome.HttpApiExample.Utils;

namespace SharpOutcome.HttpApiExample.Controllers;

public static class ControllerExtensions
{
    public static IActionResult MakeResponse(this ActionContext context, HttpGoodOutcome success)
    {
        var code = success.Tag switch
        {
            HttpGoodOutcomeTag.Ok => StatusCodes.Status200OK,
            HttpGoodOutcomeTag.Created => StatusCodes.Status201Created,
            HttpGoodOutcomeTag.NoContent => StatusCodes.Status204NoContent,
            _ => StatusCodes.Status200OK
        };

        return context.MakeResponse(code, null, success.Reason);
    }


    public static IActionResult MakeResponse(this ActionContext context, HttpBadOutcome error)
    {
        var code = error.Tag switch
        {
            HttpBadOutcomeTag.InternalServerError => StatusCodes.Status500InternalServerError,
            HttpBadOutcomeTag.BadRequest => StatusCodes.Status400BadRequest,
            HttpBadOutcomeTag.Conflict => StatusCodes.Status409Conflict,
            HttpBadOutcomeTag.NotFound => StatusCodes.Status404NotFound,
            HttpBadOutcomeTag.Unauthorized => StatusCodes.Status401Unauthorized,
            HttpBadOutcomeTag.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return context.MakeResponse(code, null, error.Reason);
    }


    public static IActionResult MakeResponse(this ActionContext context, int code, object? data = null,
        string? message = null)
    {
        if (code == StatusCodes.Status204NoContent) return new NoContentResult();

        var isSuccess = code is >= 200 and < 300;
        var res = new ApiResponse
        {
            Success = isSuccess,
            Message = message ?? ReasonPhrases.GetReasonPhrase(code),
            Code = code,
            Data = data
        };

        return new ObjectResult(res) { StatusCode = code };
    }
}