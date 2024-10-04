using SharpOutcome.Helpers.Contracts;
using SharpOutcome.Helpers.Enums;

namespace SharpOutcome.Helpers;

/// <summary>
/// Represents an HTTP bad outcome.
/// </summary>
/// <param name="Tag">The unique tag of the bad outcome.</param>
/// <param name="Reason">The reason for the bad outcome (optional).</param>
public readonly record struct HttpBadOutcome(HttpBadOutcomeTag Tag, string? Reason) : IBadOutcome<HttpBadOutcomeTag>;

/// <summary>
/// Represents an HTTP bad outcome with a payload of type <typeparamref name="TPayload"/>.
/// </summary>
/// <param name="Tag">The unique tag of the bad outcome.</param>
/// <typeparam name="TPayload">The type of the payload.</typeparam>
/// <param name="Payload">The payload that carries contextual data.</param>
/// <param name="Reason">The reason for the bad outcome (optional).</param>
public readonly record struct HttpBadOutcome<TPayload>(HttpBadOutcomeTag Tag, TPayload Payload, string? Reason)
    : IBadOutcomeWithPayload<HttpBadOutcomeTag, TPayload>;