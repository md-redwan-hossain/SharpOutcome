namespace SharpOutcome
{
    /// <summary>
    /// Represents the types of bad outcomes.
    /// </summary>
    public enum BadOutcomeTag : byte
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
        NotAuthenticated,
        NotFound,
        NotMatched,
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