using System.ComponentModel.DataAnnotations;

namespace account_web.Models;

public class BaseModel
{
    // 為pk 自動生成
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
