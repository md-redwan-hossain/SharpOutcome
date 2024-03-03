namespace SharpOutcome
{
    /// <summary>
    /// Enforces a contract for good outcome.
    /// </summary>
    public interface IGoodOutcome
    {
        /// <summary>
        /// Gets the unique tag of the good outcome.
        /// </summary>
        GoodOutcomeTag Tag { get; }

        /// <summary>
        /// Gets the reason for the good outcome.
        /// </summary>
        string? Reason { get; }
    }


    /// <summary>
    /// Enforces a contract for good outcome.
    /// </summary>
    public interface IGoodOutcome<out T>
    {
        /// <summary>
        /// Gets the unique tag of the good outcome.
        /// </summary>
        T Tag { get; }

        /// <summary>
        /// Gets the reason for the good outcome.
        /// </summary>
        string? Reason { get; }
    }
}