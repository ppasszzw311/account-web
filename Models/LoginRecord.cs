namespace account_web.Models;
public class LoginRecord : BaseModel
{
    public required string UserId { get; set; } = string.Empty;
    public required string LoginTime { get; set; } = string.Empty;
    public required string IpAddress { get; set; } = string.Empty;
}