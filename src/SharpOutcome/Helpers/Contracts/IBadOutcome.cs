namespace SharpOutcome.Helpers.Contracts;

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