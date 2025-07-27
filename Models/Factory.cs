namespace account_web.Models;

public class Factory : BaseModel
{
    public required string FactoryId { get; set; } = string.Empty;
    public required string FactoryName { get; set; } = string.Empty;
}
