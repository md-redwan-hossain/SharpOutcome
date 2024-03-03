namespace SharpOutcome
{
    /// <summary>
    /// Represents a bad outcome.
    /// </summary>
    public interface IBadOutcome
    {
        /// <summary>
        /// Gets the type of the bad outcome.
        /// </summary>
        BadOutcomeType BadOutcomeType { get; }

        /// <summary>
        /// Gets the reason for the bad outcome.
        /// </summary>
        string? Reason { get; }
    }
}