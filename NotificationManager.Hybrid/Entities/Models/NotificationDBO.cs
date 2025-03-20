using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationManager.Hybrid.Entities.Models;

[Table("notification")]
public class NotificationDBO
{
    [Key, Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("notif_title")]
    public string? NotificationTitle { get; set; }

    [Column("notif_text")]
    public string? NotificationText { get; set; }

    [Column("notif_app")]
    public required string NotificationApp { get; set; }

    [Column("notification_id")]
    public int NotificationId { get; set; }

    [Column("notif_link")]
    public string? NotificationLink { get; set; }

    [Column("recieved_on")]
    public DateTime RecievedOn { get; set; }

    [Column("fk_application_id")]
    public Guid ApplicationId { get; set; }

    [ForeignKey(nameof(ApplicationId))]
    public ApplicationDBO? Application { get; set; }
}