using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationManager.Entities.Models;

public class ApplicationDBO : BaseDBO
{
    [Column("name")]
    public string? name { get; set; }

    [Column("package")]
    public string? package {  get; set; }

    [Column("icon")]
    public byte[]? icon { get; set; }
}
