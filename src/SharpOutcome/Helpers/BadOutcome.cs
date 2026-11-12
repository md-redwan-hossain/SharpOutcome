using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpOutcome.Helpers;

/// <summary>Wraps a failure value of type <typeparamref name="T"/>.</summary>
public readonly record struct BadOutcome<T> where T : notnull
{
    /// <summary>The wrapped value.</summary>
    public required T Value { get; init; }

    /// <summary>Creates a bad outcome.</summary>
    /// <param name="value">The wrapped value.</param>
    [SetsRequiredMembers]
    public BadOutcome(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        Value = value;
    }
}