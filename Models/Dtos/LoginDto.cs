using System.ComponentModel.DataAnnotations;

namespace account_web.Models.Dtos;

public class LoginDto
{
    [Required(ErrorMessage = "請輸入帳號")]
    [Display(Name = "帳號")]
    public string UserId { get; set; } = string.Empty;

    [Required(ErrorMessage = "請輸入密碼")]
    [Display(Name = "密碼")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "記住我")]
    public bool RememberMe { get; set; }
} 