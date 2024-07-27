namespace SharpOutcome.Helpers;

/// <summary>
/// Represents a bad outcome with a payload of type <typeparamref name="TPayload"/>.
/// </summary>
/// <typeparam name="TPayload">The type of the payload.</typeparam>
/// <param name="Tag">The unique tag of the bad outcome.</param>
/// <param name="Payload">The payload that carries contextual data.</param>
/// <param name="Reason">The reason for the bad outcome (optional).</param>
public readonly record struct BadOutcomeWithPayload<TPayload>(
    BadOutcomeTag Tag,
    TPayload Payload,
    string? Reason = null
) : IBadOutcomeWithPayload<TPayload>;

/// <summary>
/// Represents a bad outcome with a payload of type <typeparamref name="TPayload"/>.
/// </summary>
/// <typeparam name="TOutcomeTag">The type of the bad outcome.</typeparam>
/// <param name="Tag">The unique tag of the bad outcome.</param>
/// <param name="Payload">The payload that carries contextual data.</param>
/// <typeparam name="TPayload">The type of the payload.</typeparam>
/// <param name="Reason">The reason for the bad outcome (optional).</param>
public readonly record struct BadOutcomeWithPayload<TPayload, TOutcomeTag>(
    TOutcomeTag Tag,
    TPayload Payload,
    string? Reason = null
) : IBadOutcomeWithPayload<TPayload, TOutcomeTag>;