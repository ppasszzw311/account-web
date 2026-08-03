using System.ComponentModel.DataAnnotations;

namespace account_web.Models.Dtos;

public class FactoryDtos
{
}


public class CreateFactoryDto
{
    [Required]
    public string FactoryId { get; set; } = string.Empty;
    [Required]
    public string FactoryName { get; set; } = string.Empty;
    
}

public class EditFactoryResponseDto
{
    public string FactoryId { get; set; }
    public string FactoryName { get; set; }
    public EditFactoryResponseDto(Factory factory)
    {
        FactoryId = factory.FactoryId ?? string.Empty;
        FactoryName = factory.FactoryName ?? string.Empty;
    }
}