namespace SharpOutcome.Helpers;

/// <summary>
/// Enforces a contract for bad outcome.
/// </summary>
public interface IBadOutcome
{
    /// <summary>
    /// Gets the type of the bad outcome.
    /// </summary>
    BadOutcomeTag Tag { get; }

    /// <summary>
    /// Gets the reason for the bad outcome.
    /// </summary>
    string? Reason { get; }
}

/// <summary>
/// Enforces a contract for bad outcome.
/// </summary>
/// <typeparam name="TOutcomeTag">The type of the unique tag of the bad outcome.</typeparam>
public interface IBadOutcome<out TOutcomeTag>
{
    /// <summary>
    /// Gets the unique tag of the bad outcome.
    /// </summary>
    TOutcomeTag Tag { get; }

    /// <summary>
    /// Gets the unique tag for the bad outcome.
    /// </summary>
    string? Reason { get; }
}