using Microsoft.AspNetCore.Mvc;
using account_web.Models.Dtos;
using account_web.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace account_web.Controllers;

[Route("[controller]")]
public class AuthController : Controller
{
  private readonly UserServices _userServices;
  private readonly JwtService _jwtService;
  private readonly ILogger<AuthController> _logger;
  private readonly IConfiguration _configuration;

  public AuthController(UserServices userServices, JwtService jwtService, ILogger<AuthController> logger, IConfiguration configuration)
  {
    _userServices = userServices;
    _jwtService = jwtService;
    _logger = logger;
    _configuration = configuration;
  }

  // GET: /Auth/Login
  [HttpGet("Login")]
  public IActionResult Login()
  {
    return View();
  }

  // POST: /Auth/Login
  [HttpPost("Login")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Login(LoginDto model)
  {
    if (ModelState.IsValid)
    {
      try
      {
        var user = await _userServices.ValidateUser(model.UserId, model.Password);
        if (user != null)
        {
          _logger.LogInformation($"User {user.UserId} logged in successfully");

          // 使用JwtService生成tokens
          var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
          var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
          var tokens = _jwtService.GenerateTokens(user, ipAddress, userAgent);

          // 設定Session（無論是否記住我）
          HttpContext.Session.SetString("UserId", user.UserId);
          HttpContext.Session.SetString("UserName", user.Name);

          // 檢查是否是AJAX請求
          if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
          {
            // AJAX請求，返回JSON給前端JavaScript處理
            return Ok(new
            {
              token = tokens.AccessToken,
              refreshToken = tokens.RefreshToken,
              success = true
            });
          }
          else
          {
            // 直接表單提交，重定向到儀表板
            return RedirectToAction("Dashboard", "Auth");
          }
        }
        else
        {
          ModelState.AddModelError("", "帳號或密碼錯誤");
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Login error for user {UserId}", model.UserId);
        ModelState.AddModelError("", "登入時發生錯誤，請稍後再試");
      }
    }

    return View(model);
  }

  // GET: /Auth/Dashboard
  [HttpGet("Dashboard")]
  public IActionResult Dashboard()
  {
    var userId = HttpContext.Session.GetString("UserId");
    if (string.IsNullOrEmpty(userId))
    {
      return RedirectToAction("Login", "Auth");
    }

    var userName = HttpContext.Session.GetString("UserName");
    ViewBag.UserName = userName;
    ViewBag.UserId = userId;

    return View();
  }

  // POST: /Auth/Refresh
  [HttpPost("Refresh")]
  public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto refreshRequest)
  {
    try
    {
      var newTokens = await _jwtService.RefreshTokenAsync(refreshRequest.AccessToken, refreshRequest.RefreshToken);
      if (newTokens == null)
      {
        return BadRequest(new { Message = "Refresh token 無效或已過期" });
      }

      return Ok(new LoginResponseDto
      {
        Success = true,
        Message = "Token 刷新成功",
        Tokens = newTokens
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error refreshing token");
      return StatusCode(500, new { Message = "Token 刷新時發生錯誤" });
    }
  }

  // POST: /Auth/Logout
  [HttpPost("Logout")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Logout([FromBody] LogoutDto logoutDto)
  {
    try
    {
      var success = await _jwtService.RevokeRefreshTokenAsync(logoutDto.RefreshToken);
      if (!success)
      {
        return BadRequest(new { Message = "Refresh token 無效" });
      }

      // 清除Session
      HttpContext.Session.Clear();

      return Ok(new { Message = "登出成功" });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error during logout");
      return StatusCode(500, new { Message = "登出時發生錯誤" });
    }
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

}
