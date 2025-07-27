namespace account_web.Models;

public class Account : BaseModel
{
    public required string AccountId { get; set; } = string.Empty;
    public required string AccountPassword { get; set; } = string.Empty;
    public DomainCategory DomainCategory { get; set; } = DomainCategory.OS;
    public DomainType DomainType { get; set; } = DomainType.Server;
    public string ProjectId { get; set; } = string.Empty;
    public string ServerIp { get; set; } = string.Empty;
    public string ServerPort { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
