namespace SharpOutcome
{
    /// <summary>
    /// Represents the types of bad outcomes.
    /// </summary>
    public enum BadOutcomeType : byte
    {
        Conflict = 1,
        Denied,
        Excessive,
        Expired,
        Failure,
        False,
        Forbidden,
        Incomplete,
        Interrupted,
        InValid,
        Negative,
        NotFound,
        NotReadyYet,
        NotVerified,
        Null,
        Rejected,
        Stalled,
        Timeout,
        Unauthorized,
        Unavailable,
        Unexpected,
        Unprocessable,
        Validation,
        Violation,
        Warning
    }
}