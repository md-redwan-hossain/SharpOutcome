namespace SharpOutcome
{
    /// <summary>
    /// Represents a good outcome.
    /// </summary>
    public interface IGoodOutcome
    {
        /// <summary>
        /// Gets the type of the good outcome.
        /// </summary>
        GoodOutcomeType GoodOutcomeType { get; }

        /// <summary>
        /// Gets the reason for the good outcome.
        /// </summary>
        string? Reason { get; }
    }
}