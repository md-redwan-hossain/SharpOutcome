namespace SharpOutcome
{
    public interface IBadOutcome
    {
        BadOutcomeType BadOutcomeType { get; }
        string? Reason { get; }
    }
}