namespace account_web.Models;

public class ProjectMember : BaseModel
{
    public required string ProjectId { get; set; } = string.Empty;
    public required string UserId { get; set; } = string.Empty;   
}
