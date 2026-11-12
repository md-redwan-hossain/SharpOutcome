using System;
using System.Diagnostics.CodeAnalysis;
using SharpOutcome.Helpers.Contracts;
using SharpOutcome.Helpers.Enums;

namespace SharpOutcome.Helpers;

/// <summary>
/// Represents an HTTP good outcome.
/// </summary>
public readonly record struct HttpGoodOutcome : IGoodOutcome<HttpGoodOutcomeTag>
{
    /// <summary>The unique tag of the good outcome.</summary>
    public required HttpGoodOutcomeTag Tag { get; init; }

    /// <summary>The reason for the good outcome (optional).</summary>
    public string? Reason { get; init; }


    /// <summary>Creates a good outcome.</summary>
    /// <param name="tag">The unique tag of the good outcome.</param>
    /// <param name="reason">The reason for the good outcome (optional).</param>
    [SetsRequiredMembers]
    public HttpGoodOutcome(HttpGoodOutcomeTag tag, string? reason = null)
    {
        if (!Enum.IsDefined(tag))
        {
            throw new ArgumentOutOfRangeException(nameof(tag));
        }

        Tag = tag;
        Reason = reason;
    }
}

/// <summary>
/// Represents an HTTP good outcome with a payload of type <typeparamref name="TPayload"/>.
/// </summary>
/// <typeparam name="TPayload">The type of the payload.</typeparam>
public readonly record struct HttpGoodOutcome<TPayload> : IGoodOutcomeWithPayload<HttpGoodOutcomeTag, TPayload>
{
    /// <summary>The unique tag of the good outcome.</summary>
    public required HttpGoodOutcomeTag Tag { get; init; }

    /// <summary>The payload that carries contextual data.</summary>
    public required TPayload Payload { get; init; }

    /// <summary>The reason for the good outcome (optional).</summary>
    public string? Reason { get; init; }


    /// <summary>Creates a good outcome with a payload.</summary>
    /// <param name="tag">The unique tag of the good outcome.</param>
    /// <param name="payload">The payload that carries contextual data.</param>
    /// <param name="reason">The reason for the good outcome (optional).</param>
    [SetsRequiredMembers]
    public HttpGoodOutcome(HttpGoodOutcomeTag tag, TPayload payload, string? reason = null)
    {
        if (!Enum.IsDefined(tag))
        {
            throw new ArgumentOutOfRangeException(nameof(tag));
        }

        Tag = tag;
        Payload = payload;
        Reason = reason;
    }
}