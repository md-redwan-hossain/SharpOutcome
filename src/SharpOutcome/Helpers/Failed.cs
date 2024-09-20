namespace SharpOutcome.Helpers;

/// <summary>
/// Represents a generalized bad outcome.
/// </summary>
public readonly record struct Failed(string? Reason = null);

public readonly record struct Failed<TReason>(TReason? Reason = default);