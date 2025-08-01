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
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? FactoryId { get; set; }
    public DateTime CreatedAt { get; set; }
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
