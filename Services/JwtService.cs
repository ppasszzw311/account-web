using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using account_web.Models;
using account_web.Models.Dtos;
using account_web.Data;
using Microsoft.EntityFrameworkCore;

namespace account_web.Services;

public class JwtService
{
  private readonly IConfiguration _configuration;
  private readonly ApplicationDbContext _context;
  private readonly ILogger<JwtService> _logger;

  public JwtService(IConfiguration configuration, ApplicationDbContext context, ILogger<JwtService> logger)
  {
    _configuration = configuration;
    _context = context;
    _logger = logger;
  }

  public JwtTokenDto GenerateTokens(User user, string? ipAddress = null, string? userAgent = null)
  {
    var accessToken = GenerateAccessToken(user);
    var refreshToken = GenerateRefreshToken();

    // 儲存 RefreshToken 到資料庫
    var refreshTokenEntity = new RefreshToken
    {
      Token = refreshToken,
      UserId = user.UserId,
      ExpiresAt = DateTime.UtcNow.AddDays(7), // 7天後過期
      IpAddress = ipAddress,
      UserAgent = userAgent
    };

    _context.RefreshTokens.Add(refreshTokenEntity);
    _context.SaveChanges();

    return new JwtTokenDto
    {
      AccessToken = accessToken,
      RefreshToken = refreshToken,
      ExpiresAt = DateTime.UtcNow.AddHours(1), // Access Token 1小時後過期
      TokenType = "Bearer"
    };
  }

  private string GenerateAccessToken(User user)
  {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
            new Claim(ClaimTypes.NameIdentifier, user.UserId),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("UserId", user.UserId),
            new Claim("UserName", user.Name),
            new Claim("FactoryId", user.FactoryId ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  private string GenerateRefreshToken()
  {
    var randomNumber = new byte[64];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(randomNumber);
    return Convert.ToBase64String(randomNumber);
  }

  public TokenValidationDto ValidateToken(string token)
  {
    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = _configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = _configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
      }, out SecurityToken validatedToken);

      var jwtToken = (JwtSecurityToken)validatedToken;
      var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;
      var userName = jwtToken.Claims.First(x => x.Type == "UserName").Value;

      return new TokenValidationDto
      {
        IsValid = true,
        UserId = userId,
        UserName = userName,
        ExpiresAt = jwtToken.ValidTo
      };
    }
    catch (Exception ex)
    {
      _logger.LogWarning(ex, "Token validation failed");
      return new TokenValidationDto
      {
        IsValid = false,
        ErrorMessage = "Token is invalid or expired"
      };
    }
  }

  public async Task<JwtTokenDto?> RefreshTokenAsync(string accessToken, string refreshToken)
  {
    try
    {
      // 驗證 Access Token (即使過期也要能解析)
      var tokenValidation = ValidateTokenWithoutExpiration(accessToken);
      if (!tokenValidation.IsValid)
      {
        return null;
      }

      // 檢查 RefreshToken 是否存在且未過期
      var refreshTokenEntity = await _context.RefreshTokens
          .FirstOrDefaultAsync(rt => rt.Token == refreshToken &&
                                    rt.UserId == tokenValidation.UserId &&
                                    !rt.IsRevoked &&
                                    rt.ExpiresAt > DateTime.UtcNow);

      if (refreshTokenEntity == null)
      {
        return null;
      }

      // 獲取用戶資訊
      var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == tokenValidation.UserId);
      if (user == null)
      {
        return null;
      }

      // 撤銷舊的 RefreshToken
      refreshTokenEntity.IsRevoked = true;
      refreshTokenEntity.RevokedAt = DateTime.UtcNow;
      refreshTokenEntity.RevokedBy = "refresh";

      // 生成新的 tokens
      var newTokens = GenerateTokens(user, refreshTokenEntity.IpAddress, refreshTokenEntity.UserAgent);

      await _context.SaveChangesAsync();

      return newTokens;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error refreshing token");
      return null;
    }
  }

  private TokenValidationDto ValidateTokenWithoutExpiration(string token)
  {
    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = _configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = _configuration["Jwt:Audience"],
        ValidateLifetime = false, // 不驗證過期時間
        ClockSkew = TimeSpan.Zero
      }, out SecurityToken validatedToken);

      var jwtToken = (JwtSecurityToken)validatedToken;
      var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;
      var userName = jwtToken.Claims.First(x => x.Type == "UserName").Value;

      return new TokenValidationDto
      {
        IsValid = true,
        UserId = userId,
        UserName = userName,
        ExpiresAt = jwtToken.ValidTo
      };
    }
    catch (Exception ex)
    {
      _logger.LogWarning(ex, "Token validation failed (without expiration check)");
      return new TokenValidationDto
      {
        IsValid = false,
        ErrorMessage = "Token is invalid"
      };
    }
  }

  public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
  {
    try
    {
      var refreshTokenEntity = await _context.RefreshTokens
          .FirstOrDefaultAsync(rt => rt.Token == refreshToken && !rt.IsRevoked);

      if (refreshTokenEntity == null)
      {
        return false;
      }

      refreshTokenEntity.IsRevoked = true;
      refreshTokenEntity.RevokedAt = DateTime.UtcNow;
      refreshTokenEntity.RevokedBy = "logout";

      await _context.SaveChangesAsync();
      return true;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error revoking refresh token");
      return false;
    }
  }

  public async Task RevokeAllUserTokensAsync(string userId)
  {
    try
    {
      var userTokens = await _context.RefreshTokens
          .Where(rt => rt.UserId == userId && !rt.IsRevoked)
          .ToListAsync();

      foreach (var token in userTokens)
      {
        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;
        token.RevokedBy = "admin";
      }

      await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error revoking all user tokens");
    }
  }
}