namespace SharpOutcome

{
    /// <summary>
    /// Represents the types of good outcomes.
    /// </summary>
    public enum GoodOutcomeType : byte
    {
        Completed = 1,
        Confirmed,
        Created,
        Deleted,
        NotNull,
        Ok,
        Positive,
        Resolved,
        Success,
        True,
        Valid,
        Verified
    }
}