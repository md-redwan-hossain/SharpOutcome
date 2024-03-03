namespace SharpOutcome

{
    /// <summary>
    /// Represents the types of good outcomes.
    /// </summary>
    public enum GoodOutcomeTag : byte
    {
        Accepted = 1,
        Authorized,
        Authenticated,
        Completed,
        Confirmed,
        Created,
        Deleted,
        NotNull,
        Ok,
        Matched,
        Positive,
        Resolved,
        Success,
        True,
        Valid,
        Verified
    }
}