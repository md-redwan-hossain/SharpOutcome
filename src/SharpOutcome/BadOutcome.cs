namespace SharpOutcome
{
    public class BadOutcome : IBadOutcome
    {
        public BadOutcomeType BadOutcomeType { get; }
        public string? Reason { get; }

        public BadOutcome(BadOutcomeType badOutcomeType, string? reason = null)
        {
            BadOutcomeType = badOutcomeType;
            Reason = reason;
        }
    }
}