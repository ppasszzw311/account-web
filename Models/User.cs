using System.ComponentModel.DataAnnotations;

namespace account_web.Models;

public class User : BaseModel
{
    public required string UserId { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    public required string Name { get; set; } = string.Empty;
    // fk to Factory
    public string? FactoryId { get; set; } = string.Empty;
}

