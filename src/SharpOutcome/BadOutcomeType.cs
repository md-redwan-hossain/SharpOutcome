namespace SharpOutcome
{
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
        Negative,
        NotFound,
        NotReadyYet,
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