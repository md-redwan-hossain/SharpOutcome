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
        return ResponseMaker(code, await data.BuildAdapter().AdaptToTypeAsync<TResponse>());
    }


    protected IActionResult ResponseMaker(IGoodOutcome success)
    {
        var code = success.Tag switch
        {
            GoodOutcomeTag.Ok => HttpStatusCode.OK,
            GoodOutcomeTag.Created => HttpStatusCode.Created,
            GoodOutcomeTag.Deleted => HttpStatusCode.NoContent,
            _ => HttpStatusCode.OK,
        };

        return ResponseMaker(code, null, success.Reason);
    }


    protected IActionResult ResponseMaker(IBadOutcome error)
    {
        var code = error.Tag switch
        {
            BadOutcomeTag.Failure => HttpStatusCode.InternalServerError,
            BadOutcomeTag.Unexpected => HttpStatusCode.InternalServerError,
            BadOutcomeTag.Validation => HttpStatusCode.BadRequest,
            BadOutcomeTag.Conflict => HttpStatusCode.Conflict,
            BadOutcomeTag.NotFound => HttpStatusCode.NotFound,
            BadOutcomeTag.Unauthorized => HttpStatusCode.Unauthorized,
            BadOutcomeTag.Forbidden => HttpStatusCode.Forbidden,
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