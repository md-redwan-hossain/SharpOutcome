namespace SharpOutcome
{
    public enum BadOutcomeType : byte
    {
        Failure = 1,
        Unexpected,
        Validation,
        Conflict,
        NotFound,
        Unauthorized,
        Forbidden,
        False
    }
}