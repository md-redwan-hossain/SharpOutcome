namespace SharpOutcome
{
    public record BadOutcome(BadOutcomeType BadOutcomeType, string? Reason = null) : IBadOutcome;
}