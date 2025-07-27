
namespace account_web.Models;

public class UserRoleMapping : BaseModel
{
    public required string UserId { get; set; } = string.Empty;
    public required string RoleId { get; set; } = string.Empty;
    public required string ScopeType { get; set; } = string.Empty;
    public required string ScopeId { get; set; } = string.Empty;
}
