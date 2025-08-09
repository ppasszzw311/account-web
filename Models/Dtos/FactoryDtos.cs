using System.ComponentModel.DataAnnotations;

namespace account_web.Models.Dtos;

public class FactoryDtos
{
}


public class CreateFactoryDto
{
    [Required]
    public required string FactoryId { get; set; } = string.Empty;
    [Required]
    public required string FactoryName { get; set; } = string.Empty;
}

