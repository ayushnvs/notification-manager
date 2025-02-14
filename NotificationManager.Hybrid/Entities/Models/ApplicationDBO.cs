using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationManager.Hybrid.Entities.Models;

public class ApplicationDBO : BaseDBO
{
    [Column("name")]
    public string? Name { get; set; }

    [Column("package")]
    public string? Package {  get; set; }

    [Column("icon")]
    public byte[]? Icon { get; set; }

    [Column("last_updated")]
    public DateTime? LastUpdated { get; set; }
}
