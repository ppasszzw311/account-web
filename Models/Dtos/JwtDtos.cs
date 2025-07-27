using System.ComponentModel.DataAnnotations;

namespace account_web.Models.Dtos;

public class JwtTokenDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public string TokenType { get; set; } = "Bearer";
}

public class RefreshTokenDto
{
    [Required(ErrorMessage = "Refresh Token為必填項目")]
    public string RefreshToken { get; set; } = string.Empty;
}

public class TokenValidationDto
{
    public bool IsValid { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public string? ErrorMessage { get; set; }
}

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public JwtTokenDto? Tokens { get; set; }
    public UserResponseDto? User { get; set; }
}

public class LogoutDto
{
    public string RefreshToken { get; set; } = string.Empty;
}

public class RefreshTokenRequestDto
{
    [Required(ErrorMessage = "Access Token為必填項目")]
    public string AccessToken { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Refresh Token為必填項目")]
    public string RefreshToken { get; set; } = string.Empty;
} 