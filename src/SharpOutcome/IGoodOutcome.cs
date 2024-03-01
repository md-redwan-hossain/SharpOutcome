namespace SharpOutcome
{
    public interface IGoodOutcome
    {
        GoodOutcomeType GoodOutcomeType { get; }
        string? Reason { get; }
    }
}