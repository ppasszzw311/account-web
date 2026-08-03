using Microsoft.AspNetCore.Mvc;
using account_web.Models.Dtos;
using account_web.Services;
using account_web.Data;

namespace account_web.Controllers;

[Route("[controller]")]
public class AuthController : Controller
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
  public async Task<IActionResult> Logout([FromBody] LogoutDto? logoutDto = null)
  {
    try
    {
      // 如果有提供 RefreshToken，則撤銷它
      if (logoutDto != null && !string.IsNullOrEmpty(logoutDto.RefreshToken))
      {
        var success = await _jwtService.RevokeRefreshTokenAsync(logoutDto.RefreshToken);
        if (!success)
        {
          // 如果是 AJAX 請求，回傳 JSON 錯誤
          if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
          {
            return BadRequest(new { Message = "Refresh token 無效" });
          }
          // 如果是表單提交，設定錯誤訊息並重定向
          TempData["ErrorMessage"] = "登出時發生錯誤";
          return RedirectToAction("Login", "Auth");
        }
      }

      // 清除Session
      HttpContext.Session.Clear();

      // 檢查是否是AJAX請求
      if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
      {
        // AJAX請求，返回JSON
        return Ok(new { Message = "登出成功" });
      }
      else
      {
        // 表單提交，重定向到登入頁面
        return RedirectToAction("Login", "Auth");
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error during logout");
      
      // 檢查是否是AJAX請求
      if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
      {
        return StatusCode(500, new { Message = "登出時發生錯誤" });
      }
      else
      {
        TempData["ErrorMessage"] = "登出時發生錯誤";
        return RedirectToAction("Login", "Auth");
      }
    }
  }

 
}
