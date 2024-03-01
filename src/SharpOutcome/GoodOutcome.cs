namespace SharpOutcome
{
    public class GoodOutcome : IGoodOutcome
    {
        public GoodOutcome(GoodOutcomeType goodOutcomeType, string? description = null)
        {
            GoodOutcomeType = goodOutcomeType;
            Reason = description;
        }

        public GoodOutcomeType GoodOutcomeType { get; }
        public string? Reason { get; }
    }
}