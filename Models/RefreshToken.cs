namespace account_web.Models;

public class RefreshToken : BaseModel
{
  public required string Token { get; set; } = string.Empty;
  public required string UserId { get; set; } = string.Empty;
  public DateTime ExpiresAt { get; set; }
  public bool IsRevoked { get; set; } = false;
  public string? RevokedBy { get; set; }
  public DateTime? RevokedAt { get; set; }
  public string? IpAddress { get; set; }
  public string? UserAgent { get; set; }
}