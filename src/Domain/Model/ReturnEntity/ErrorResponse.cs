namespace Domain.Model.ReturnEntity;

public class ErrorResponse
{
    public string ErrorName { get; set; } = "Unknown";
    public string? Message { get; set; }
    public Dictionary<string, string>? Details { get; set; }
}