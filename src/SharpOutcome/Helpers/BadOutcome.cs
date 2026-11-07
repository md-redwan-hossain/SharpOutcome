namespace SharpOutcome.Helpers;

public readonly record struct BadOutcome<T>(T Value) where T : notnull;