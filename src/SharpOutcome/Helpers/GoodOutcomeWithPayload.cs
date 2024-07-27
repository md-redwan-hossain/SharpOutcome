namespace SharpOutcome.Helpers;

/// <summary>
/// Represents a good outcome with a payload of type <typeparamref name="TPayload"/>.
/// </summary>
/// <param name="Tag">The unique tag of the good outcome.</param>
/// <typeparam name="TPayload">The type of the payload.</typeparam>
/// <param name="Payload">The payload that carries contextual data.</param>
/// <param name="Reason">The reason for the good outcome (optional).</param>
public readonly record struct GoodOutcomeWithPayload<TPayload>(
    GoodOutcomeTag Tag,
    TPayload Payload,
    string? Reason = null
) : IGoodOutcomeWithPayload<TPayload>;

/// <summary>
/// Represents a good outcome with a payload of type <typeparamref name="TPayload"/>.
/// </summary>
/// <typeparam name="TOutcomeTag">The type of the good outcome.</typeparam>
/// <param name="Tag">The unique tag of the good outcome.</param>
/// <typeparam name="TPayload">The type of the payload.</typeparam>
/// <param name="Payload">The payload that carries contextual data.</param>
/// <param name="Reason">The reason for the good outcome (optional).</param>
public readonly record struct GoodOutcomeWithPayload<TPayload, TOutcomeTag>(
    TOutcomeTag Tag,
    TPayload Payload,
    string? Reason = null
) : IGoodOutcomeWithPayload<TPayload, TOutcomeTag>;