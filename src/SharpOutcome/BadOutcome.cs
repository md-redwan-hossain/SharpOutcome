namespace SharpOutcome
{
    /// <summary>
    /// Represents a bad outcome.
    /// </summary>
    /// <param name="BadOutcomeType">The type of the bad outcome.</param>
    /// <param name="Reason">The reason for the bad outcome (optional).</param>
    public record BadOutcome(BadOutcomeType BadOutcomeType, string? Reason = null) : IBadOutcome;
}