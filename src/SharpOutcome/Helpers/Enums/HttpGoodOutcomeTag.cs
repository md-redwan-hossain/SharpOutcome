namespace SharpOutcome.Helpers.Enums;

/// <summary>
/// Represents the types of HTTP good outcomes.
/// </summary>
public enum HttpGoodOutcomeTag : byte
{
    Ok = 1,
    Created,
    Accepted,
    NonAuthoritativeInformation,
    NoContent,
    ResetContent,
    PartialContent,
    MultiStatus,
    AlreadyReported,
    ImUsed
}