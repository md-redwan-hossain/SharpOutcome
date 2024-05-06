namespace SharpOutcome.Helpers;

/// <summary>
/// Enforces a contract for good outcome.
/// </summary>
public interface IGoodOutcome
{
    /// <summary>
    /// Gets the unique tag of the good outcome.
    /// </summary>
    GoodOutcomeTag Tag { get; }

    /// <summary>
    /// Gets the reason for the good outcome.
    /// </summary>
    string? Reason { get; }
}


/// <summary>
/// Enforces a contract for good outcome.
/// </summary>
/// <typeparam name="TOutcomeTag">The type of the unique tag of the good outcome.</typeparam>
public interface IGoodOutcome<out TOutcomeTag>
{
    /// <summary>
    /// Gets the unique tag of the good outcome.
    /// </summary>
    TOutcomeTag Tag { get; }

    /// <summary>
    /// Gets the reason for the good outcome.
    /// </summary>
    string? Reason { get; }
}