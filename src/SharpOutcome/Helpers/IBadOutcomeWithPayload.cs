namespace SharpOutcome.Helpers;

/// <summary>
/// Enforces a contract for bad outcome with payload of type <typeparamref name="TPayload"/>.
/// </summary>
public interface IBadOutcomeWithPayload<out TPayload>
{
    /// <summary>
    /// Gets the unique tag of the bad outcome.
    /// </summary>
    BadOutcomeTag Tag { get; }


    /// <summary>
    /// Gets the Payload with the bad outcome.
    /// </summary>
    TPayload Payload { get; }

    /// <summary>
    /// Gets the reason for the bad outcome.
    /// </summary>
    string? Reason { get; }
}


/// <summary>
/// Enforces a contract for bad outcome with payload of type <typeparamref name="TPayload"/>.
/// </summary>
public interface IBadOutcomeWithPayload<out TPayload, out TOutcomeTag>
{
    /// <summary>
    /// Gets the unique tag of the bad outcome.
    /// </summary>
    TOutcomeTag Tag { get; }

    /// <summary>
    /// Gets the Payload with the bad outcome.
    /// </summary>
    TPayload Payload { get; }

    /// <summary>
    /// Gets the reason for the bad outcome.
    /// </summary>
    string? Reason { get; }
}