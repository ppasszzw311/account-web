using Microsoft.AspNetCore.Mvc;
using account_web.Models.Dtos;
using account_web.Services;
using account_web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace account_web.Controllers.Api;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly UserServices _userServices;
    private readonly JwtService _jwtService;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    public AuthController(UserServices userServices, JwtService jwtService, ILogger<AuthController> logger, IConfiguration configuration, ApplicationDbContext context)
    {
        _userServices = userServices;
        _jwtService = jwtService;
        _logger = logger;
        _configuration = configuration;
        _context = context;
    }
    
    // GET: /Auth/Validate
    [HttpGet("Validate")]
    [Authorize]
    public IActionResult ValidateToken()
    {
        try
        {
            var userId = User.FindFirst("UserId")?.Value;
            var userName = User.FindFirst("UserName")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new TokenValidationDto
                {
                    IsValid = false,
                    ErrorMessage = "Token 中缺少用戶資訊"
                });
            }

            return Ok(new TokenValidationDto
            {
                IsValid = true,
                UserId = userId,
                UserName = userName
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating token");
            return StatusCode(500, new TokenValidationDto
            {
                IsValid = false,
                ErrorMessage = "Token 驗證時發生錯誤"
            });
        }
    }
    
    // GET: /Auth/Debug
    [HttpGet("Debug")]
    public async Task<IActionResult> Debug()
    {
        try
        {
            var sessionUserId = HttpContext.Session.GetString("UserId");
            var sessionUserName = HttpContext.Session.GetString("UserName");
      
            var activeTokens = await _context.RefreshTokens
                .Where(rt => !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow)
                .Select(rt => new { rt.UserId, rt.Token, rt.ExpiresAt })
                .ToListAsync();

            var revokedTokens = await _context.RefreshTokens
                .Where(rt => rt.IsRevoked)
                .Select(rt => new { rt.UserId, rt.Token, rt.RevokedAt, rt.RevokedBy })
                .ToListAsync();

            return Ok(new
            {
                Session = new
                {
                    UserId = sessionUserId,
                    UserName = sessionUserName
                },
                ActiveTokens = activeTokens,
                RevokedTokens = revokedTokens,
                TotalTokens = activeTokens.Count + revokedTokens.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in debug endpoint");
            return StatusCode(500, new { Error = ex.Message });
        }
    }

}