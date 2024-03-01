namespace SharpOutcome
{
    public record GoodOutcome(GoodOutcomeType GoodOutcomeType, string? Reason = null) : IGoodOutcome;
}