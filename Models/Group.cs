
namespace account_web.Models;

public class Group : BaseModel
{
    public required string GroupName { get; set; } = string.Empty;
    public required string ProjectId { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
}