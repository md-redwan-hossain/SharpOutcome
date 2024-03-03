namespace SharpOutcome

{
    /// <summary>
    /// Represents a good outcome.
    /// </summary>
    /// <param name="GoodOutcomeType">The type of the good outcome.</param>
    /// <param name="Reason">The reason for the good outcome (optional).</param>
    public record GoodOutcome(GoodOutcomeType GoodOutcomeType, string? Reason = null) : IGoodOutcome;
}