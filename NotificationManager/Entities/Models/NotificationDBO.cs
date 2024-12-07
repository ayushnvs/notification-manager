using SQLite;

namespace NotificationManager.Entities.Models;

[Table("notification")]
public class NotificationDBO
{
    [PrimaryKey, Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("notif_title")]
    public string? NotificationTitle { get; set; }

    [Column("notif_text")]
    public string? NotificationText { get; set; }

    [Column("notif_app")]
    public string? NotificationApp { get; set; }

    [Column("notif_link")]
    public string? NotificationLink { get; set; }

    [Column("recieved_on")]
    public DateTime RecievedOn { get; set; }
}