using SharpOutcome.Helpers.Contracts;
using SharpOutcome.Helpers.Enums;

namespace SharpOutcome.Helpers;

/// <summary>
/// Represents an HTTP good outcome.
/// </summary>
/// <param name="Tag">The unique tag of the good outcome.</param>
/// <param name="Reason">The reason for the good outcome (optional).</param>
public readonly record struct HttpGoodOutcome(HttpGoodOutcomeTag Tag, string? Reason)
    : IGoodOutcome<HttpGoodOutcomeTag>;

/// <summary>
/// Represents an HTTP good outcome with a payload of type <typeparamref name="TPayload"/>.
/// </summary>
/// <param name="Tag">The unique tag of the good outcome.</param>
/// <typeparam name="TPayload">The type of the payload.</typeparam>
/// <param name="Payload">The payload that carries contextual data.</param>
/// <param name="Reason">The reason for the good outcome (optional).</param>
public readonly record struct HttpGoodOutcome<TPayload>(HttpGoodOutcomeTag Tag, TPayload Payload, string? Reason)
    : IGoodOutcomeWithPayload<HttpGoodOutcomeTag, TPayload>;