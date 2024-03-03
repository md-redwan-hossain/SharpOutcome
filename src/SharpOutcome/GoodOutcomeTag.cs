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
        Found,
        NoConflict,
        NotNull,
        Ok,
        Matched,
        Positive,
        Resolved,
        Success,
        True,
        Unique,
        Valid,
        Verified
    }
}