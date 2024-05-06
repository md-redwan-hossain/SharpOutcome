namespace SharpOutcome.Helpers;

/// <summary>
/// Enforces a contract for good outcome with payload of type <typeparamref name="TPayload"/>.
/// </summary>
public interface IGoodOutcomeWithPayload<out TPayload>
{
    /// <summary>
    /// Gets the unique tag of the good outcome.
    /// </summary>
    GoodOutcomeTag Tag { get; }


    /// <summary>
    /// Gets the Payload with the good outcome.
    /// </summary>
    TPayload Payload { get; }

    /// <summary>
    /// Gets the reason for the good outcome.
    /// </summary>
    string? Reason { get; }
}


/// <summary>
/// Enforces a contract for good outcome with payload of type <typeparamref name="TPayload"/>.
/// </summary>
public interface IGoodOutcomeWithPayload<out TPayload, out TOutcomeTag>
{
    /// <summary>
    /// Gets the unique tag of the good outcome.
    /// </summary>
    TOutcomeTag Tag { get; }

    /// <summary>
    /// Gets the Payload with the good outcome.
    /// </summary>
    TPayload Payload { get; }

    /// <summary>
    /// Gets the reason for the good outcome.
    /// </summary>
    string? Reason { get; }
}