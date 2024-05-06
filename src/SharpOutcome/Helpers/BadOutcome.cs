namespace SharpOutcome.Helpers;

/// <summary>
/// Represents a bad outcome.
/// </summary>
/// <param name="Tag">The unique tag of the bad outcome.</param>
/// <param name="Reason">The reason for the bad outcome (optional).</param>
public record BadOutcome(BadOutcomeTag Tag, string? Reason = null) : IBadOutcome;


/// <summary>
/// Represents a bad outcome.
/// </summary>
/// <typeparam name="TOutcomeTag">The type of the bad outcome.</typeparam>
/// <param name="Tag">The unique tag of the bad outcome.</param>
/// <param name="Reason">The reason for the bad outcome (optional).</param>
public record BadOutcome<TOutcomeTag>(TOutcomeTag Tag, string? Reason = null) : IBadOutcome<TOutcomeTag>;