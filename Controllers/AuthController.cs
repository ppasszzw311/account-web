using Microsoft.AspNetCore.Mvc;
using account_web.Models;
using account_web.Models.Dtos;
using account_web.Services;
using Microsoft.AspNetCore.Http;

namespace account_web.Controllers;

public class AuthController : Controller
{
  private readonly UserServices _userServices;
  private readonly ILogger<AuthController> _logger;

  public AuthController(UserServices userServices, ILogger<AuthController> logger)
  {
    _userServices = userServices;
    _logger = logger;
  }

  // GET: /Auth/Login
  public IActionResult Login()
  {
    // 如果已經登入，重定向到儀表板
    if (HttpContext.Session.GetString("UserId") != null)
    {
      return RedirectToAction("Dashboard", "Auth");
    }
    return View();
  }

  // POST: /Auth/Login
  [HttpPost]
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
          // 登入成功，設定Session
          HttpContext.Session.SetString("UserId", user.UserId);
          HttpContext.Session.SetString("UserName", user.Name);
          HttpContext.Session.SetInt32("UserId", user.Id);

          _logger.LogInformation($"User {user.UserId} logged in successfully");
          return RedirectToAction("Dashboard", "Auth");
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

  // POST: /Auth/Logout
  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Logout()
  {
    var userId = HttpContext.Session.GetString("UserId");
    if (!string.IsNullOrEmpty(userId))
    {
      _logger.LogInformation($"User {userId} logged out");
    }

    HttpContext.Session.Clear();
    return RedirectToAction("Login", "Auth");
  }
}