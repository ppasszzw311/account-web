using System.ComponentModel.DataAnnotations;

namespace account_web.Models.Dtos;

public class UserDto
{
    [Required(ErrorMessage = "使用者ID為必填項目")]
    public string UserId { get; set; } = string.Empty;
    [Required(ErrorMessage = "密碼為必填項目")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "姓名為必填項目")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "工廠ID為必填項目")]
    public string FactoryId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class UserResponseDto
{
    public int Id { get; set; }
    [Display(Name = "使用者帳號")]
    public string UserId { get; set; } = string.Empty;
    [Display(Name = "姓名")]
    public string Name { get; set; } = string.Empty;
    [Display(Name = "廠區ID")]
    public string? FactoryId { get; set; }
    [Display(Name = "建立時間")]
    public DateTime CreatedAt { get; set; }
    [Display(Name = "修改時間")]
    public DateTime UpdatedAt { get; set; }
}

public class UserCreateDto
{
    [Required(ErrorMessage = "使用者ID為必填項目")]
    public string UserId { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "密碼為必填項目")]
    [MinLength(6, ErrorMessage = "密碼至少需要6個字符")]
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "姓名為必填項目")]
    public string Name { get; set; } = string.Empty;
    
    public string? FactoryId { get; set; }
}

public class UserUpdateDto
{
    [Required(ErrorMessage = "姓名為必填項目")]
    public string Name { get; set; } = string.Empty;
    
    public string? FactoryId { get; set; }
}
