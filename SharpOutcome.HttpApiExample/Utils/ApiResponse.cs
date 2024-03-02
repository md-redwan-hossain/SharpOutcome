namespace SharpOutcome.HttpApiExample.Utils;

public class ApiResponse
{
    public required bool Success { get; set; }
    public required int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }
}