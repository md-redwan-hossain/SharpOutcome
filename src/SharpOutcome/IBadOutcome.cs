namespace SharpOutcome
{
    /// <summary>
    /// Enforces a contract for bad outcome.
    /// </summary>
    public interface IBadOutcome
    {
        /// <summary>
        /// Gets the type of the bad outcome.
        /// </summary>
        BadOutcomeTag Tag { get; }

        /// <summary>
        /// Gets the reason for the bad outcome.
        /// </summary>
        string? Reason { get; }
    }

    /// <summary>
    /// Enforces a contract for bad outcome.
    /// </summary>
    public interface IBadOutcome<out T>
    {
        /// <summary>
        /// Gets the unique tag of the bad outcome.
        /// </summary>
        T Tag { get; }

        /// <summary>
        /// Gets the unique tag for the bad outcome.
        /// </summary>
        string? Reason { get; }
    }
}