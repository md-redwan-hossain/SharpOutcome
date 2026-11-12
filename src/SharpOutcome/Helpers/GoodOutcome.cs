using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpOutcome.Helpers;

/// <summary>Wraps a successful value of type <typeparamref name="T"/>.</summary>
public readonly record struct GoodOutcome<T> where T : notnull
{
    /// <summary>The wrapped value.</summary>
    public required T Value { get; init; }

    /// <summary>Creates a good outcome.</summary>
    /// <param name="value">The wrapped value.</param>
    [SetsRequiredMembers]
    public GoodOutcome(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        Value = value;
    }
}