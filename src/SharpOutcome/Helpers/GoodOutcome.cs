namespace SharpOutcome.Helpers;

public readonly record struct GoodOutcome<T>(T Value) where T : notnull;