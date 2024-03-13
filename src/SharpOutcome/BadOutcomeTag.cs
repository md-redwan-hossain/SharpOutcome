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
        Rejected,
        Stalled,
        Timeout,
        Unauthorized,
        Unavailable,
        Unexpected,
        Unknown,
        Unprocessable,
        ValidationFailure,
        Violation,
        Warning
    }
}