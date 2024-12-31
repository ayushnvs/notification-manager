using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationManager.Hybrid.Entities.Models;

[Table("setting")]
public class SettingsDBO : BaseDBO
{
    [Column("delete_period_day")]
    public DeletePeriodDay DayDeletePeriod { get; set; }

    [Column("delete_period_num")]
    public DeletePeriodNum NumDeletePeriod { get; set; }

    [Column("theme")]
    public Theme Theme { get; set; }

    [Column("date_format")]
    public DateFormat DateFormat { get; set; }

    [Column("style_format")]
    public StyleFormat StyleFormat { get; set; }
}
