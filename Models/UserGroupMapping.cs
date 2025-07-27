namespace account_web.Models;

public class UserGroupMapping : BaseModel
{
    public required string UserId { get; set; } = string.Empty;
    public required string GroupId { get; set; } = string.Empty;
}
