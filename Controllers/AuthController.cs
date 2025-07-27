using Microsoft.AspNetCore.Mvc;
using account_web.Models.Dtos;
using account_web.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace account_web.Controllers;

public class AuthController : Controller
{
    private readonly UserServices _userServices;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;

    public AuthController(UserServices userServices, ILogger<AuthController> logger, IConfiguration configuration)
    {
        _userServices = userServices;
        _logger = logger;
        _configuration = configuration;
    }

    // GET: /Auth/Login
    public IActionResult Login()
    {
        // This view will now be responsible for handling the JWT token returned by the POST action
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
                    _logger.LogInformation($"User {user.UserId} logged in successfully");
                    var tokenString = GenerateJwtToken(user);
                    return Ok(new { token = tokenString });
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

        // If we got this far, something failed, return to the view
        return View(model);
    }

    private string GenerateJwtToken(Models.User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim("id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // GET: /Auth/Dashboard
    // [Authorize] // This will now use JWT authentication
    // public IActionResult Dashboard()
    // {
    //     // User identity is now retrieved from the token's claims
    //     var userName = User.Identity.Name;
    //     var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //
    //     ViewBag.UserName = userName;
    //     ViewBag.UserId = userId;
    //
    //     return View();
    // }

    // POST: /Auth/Logout
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public IActionResult Logout()
    // {
    //     // For JWT, logout is typically handled client-side by deleting the token.
    //     // Server-side logout is not necessary unless you implement a token blacklist.
    //     return Ok(new { message = "Logged out" });
    // }
}