namespace account_web.Models;

public class Project : BaseModel
{
    public required string ProjectId { get; set; } = string.Empty;
    public required string ProjectName { get; set; } = string.Empty;
    public required string FactoryId { get; set; } = string.Empty;
    public required RoleId RoleId { get; set; } = RoleId.Class01;
}

