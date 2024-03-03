namespace SharpOutcome

{
    /// <summary>
    /// Represents a good outcome.
    /// </summary>
    /// <param name="Tag">The unique tag of the good outcome.</param>
    /// <param name="Reason">The reason for the good outcome (optional).</param>
    public record GoodOutcome(GoodOutcomeTag Tag, string? Reason = null) : IGoodOutcome;


    /// <summary>
    /// Represents a good outcome.
    /// </summary>
    /// <typeparam name="T">The generic type of the good outcome.</typeparam>
    /// <param name="Tag">The unique tag of the good outcome.</param>
    /// <param name="Reason">The reason for the good outcome (optional).</param>
    public record GoodOutcome<T>(T Tag, string? Reason = null) : IGoodOutcome<T>;
}