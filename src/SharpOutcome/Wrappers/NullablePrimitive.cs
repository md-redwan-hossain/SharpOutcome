namespace SharpOutcome.Wrappers;

/// <summary>
/// Can be used to wrap nullable value type data.
/// </summary>
/// <param name="Data">The underlying data.</param>
/// <typeparam name="T">The type of underlying data.</typeparam>
public readonly record struct NullablePrimitive<T>(T? Data) where T : struct;