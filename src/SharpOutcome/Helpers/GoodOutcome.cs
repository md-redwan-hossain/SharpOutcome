namespace SharpOutcome.Helpers;

/// <summary>
/// Represents a good outcome.
/// </summary>
/// <param name="Tag">The unique tag of the good outcome.</param>
/// <param name="Reason">The reason for the good outcome (optional).</param>
public readonly record struct GoodOutcome(GoodOutcomeTag Tag, string? Reason = null) : IGoodOutcome;

/// <summary>
/// Represents a good outcome.
/// </summary>
/// <typeparam name="TOutcomeTag">The type of the good outcome.</typeparam>
/// <param name="Tag">The unique tag of the good outcome.</param>
/// <param name="Reason">The reason for the good outcome (optional).</param>
public readonly record struct GoodOutcome<TOutcomeTag>(TOutcomeTag Tag, string? Reason = null)
    : IGoodOutcome<TOutcomeTag>;