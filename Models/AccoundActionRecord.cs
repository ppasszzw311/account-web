namespace account_web.Models;

public class AccountActionRecord : BaseModel
{
    public required string UserId { get; set; } = string.Empty;
    public required string AccountId { get; set; } = string.Empty;
    public required ActionType ActionType { get; set; } = ActionType.Create;
    public string Detail { get; set; } = string.Empty;
}

public enum ActionType
{
    Create,
    Update,
    Delete,
    View
}