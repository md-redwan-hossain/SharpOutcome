using System;
using System.Diagnostics.CodeAnalysis;
using SharpOutcome.Helpers.Contracts;
using SharpOutcome.Helpers.Enums;

namespace SharpOutcome.Helpers;

/// <summary>
/// Represents an HTTP bad outcome.
/// </summary>
public readonly record struct HttpBadOutcome : IBadOutcome<HttpBadOutcomeTag>
{
    /// <summary>The unique tag of the bad outcome.</summary>
    public required HttpBadOutcomeTag Tag { get; init; }

    /// <summary>The reason for the bad outcome (optional).</summary>
    public string? Reason { get; init; }


    /// <summary>Creates a bad outcome.</summary>
    /// <param name="tag">The unique tag of the bad outcome.</param>
    /// <param name="reason">The reason for the bad outcome (optional).</param>
    [SetsRequiredMembers]
    public HttpBadOutcome(HttpBadOutcomeTag tag, string? reason = null)
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
/// Represents an HTTP bad outcome with a payload of type <typeparamref name="TPayload"/>.
/// </summary>
/// <typeparam name="TPayload">The type of the payload.</typeparam>
public readonly record struct HttpBadOutcome<TPayload> : IBadOutcomeWithPayload<HttpBadOutcomeTag, TPayload>
{
    /// <summary>The unique tag of the bad outcome.</summary>
    public required HttpBadOutcomeTag Tag { get; init; }

    /// <summary>The payload that carries contextual data.</summary>
    public required TPayload Payload { get; init; }

    /// <summary>The reason for the bad outcome (optional).</summary>
    public string? Reason { get; init; }


    /// <summary>Creates a bad outcome with a payload.</summary>
    /// <param name="tag">The unique tag of the bad outcome.</param>
    /// <param name="payload">The payload that carries contextual data.</param>
    /// <param name="reason">The reason for the bad outcome (optional).</param>
    [SetsRequiredMembers]
    public HttpBadOutcome(HttpBadOutcomeTag tag, TPayload payload, string? reason = null)
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