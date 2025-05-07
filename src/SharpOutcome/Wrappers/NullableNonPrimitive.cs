namespace SharpOutcome.Wrappers;

/// <summary>
/// Can be used to wrap nullable reference type data.
/// </summary>
/// <param name="Data">The underlying data.</param>
/// <typeparam name="T">The type of underlying data.</typeparam>
public readonly record struct NullableNonPrimitive<T>(T? Data) where T : class;