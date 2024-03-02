using System.Net;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SharpOutcome.HttpApiExample.Utils;

namespace SharpOutcome.HttpApiExample.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected async Task<IActionResult> ResponseMakerAsync<TEntity, TResponse>(HttpStatusCode code, TEntity data)
    {
        return ResponseMaker(code, await data.BuildAdapter().AdaptToTypeAsync<TResponse>(), null);
    }


    protected IActionResult ResponseMaker(IGoodOutcome success)
    {
        var code = success.GoodOutcomeType switch
        {
            GoodOutcomeType.Ok => HttpStatusCode.OK,
            GoodOutcomeType.Created => HttpStatusCode.Created,
            GoodOutcomeType.Deleted => HttpStatusCode.NoContent,
            _ => HttpStatusCode.OK,
        };

        return ResponseMaker(code, null, success.Reason);
    }


    protected IActionResult ResponseMaker(IBadOutcome error)
    {
        var code = error.BadOutcomeType switch
        {
            BadOutcomeType.Failure => HttpStatusCode.InternalServerError,
            BadOutcomeType.Unexpected => HttpStatusCode.InternalServerError,
            BadOutcomeType.Validation => HttpStatusCode.BadRequest,
            BadOutcomeType.Conflict => HttpStatusCode.Conflict,
            BadOutcomeType.NotFound => HttpStatusCode.NotFound,
            BadOutcomeType.Unauthorized => HttpStatusCode.Unauthorized,
            BadOutcomeType.Forbidden => HttpStatusCode.Forbidden,
            _ => HttpStatusCode.InternalServerError,
        };

        return ResponseMaker(code, null, error.Reason);
    }


    protected IActionResult ResponseMaker(HttpStatusCode code, object? data = null, string? message = null)
    {
        if (code == HttpStatusCode.NoContent) return NoContent();

        var castedCode = (int)code;
        var isSuccess = castedCode is >= 200 and < 300;
        var res = new ApiResponse
        {
            Success = isSuccess,
            Message = message ?? ReasonPhrases.GetReasonPhrase(castedCode),
            Code = castedCode,
            Data = data
        };

        return StatusCode(castedCode, res);
    }
}