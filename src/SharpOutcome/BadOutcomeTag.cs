namespace SharpOutcome
{
    /// <summary>
    /// Represents the types of bad outcomes.
    /// </summary>
    public enum BadOutcomeTag : byte
    {
        BadRequest = 1,
        Conflict,
        Denied,
        Duplicate,
        Excessive,
        Expired,
        Failure,
        False,
        Forbidden,
        Incomplete,
        Interrupted,
        Invalid,
        Negative,
        NotAuthenticated,
        NotFound,
        NotMatched,
        NotReadyYet,
        NotVerified,
        Null,
        Outdated,
        Overflow,
        Redundant,
        Rejected,
        Repetitive,
        Stalled,
        Timeout,
        Unauthorized,
        Unavailable,
        Underflow,
        Unexpected,
        Unknown,
        Unprocessable,
        ValidationFailure,
        Violation,
        Warning
    }
}