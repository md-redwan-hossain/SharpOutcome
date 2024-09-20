namespace SharpOutcome.Helpers;

/// <summary>
/// Represents a generalized good outcome.
/// </summary>
public readonly record struct Successful(string? Reason = null);
public readonly record struct Successful<TReason>(TReason? Reason = default);