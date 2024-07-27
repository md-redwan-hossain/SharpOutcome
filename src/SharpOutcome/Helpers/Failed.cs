namespace SharpOutcome.Helpers;

/// <summary>
/// Represents a generalized bad outcome.
/// </summary>
public readonly record struct Failed(string? Reason = null);