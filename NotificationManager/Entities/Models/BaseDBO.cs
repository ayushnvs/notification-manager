
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationManager.Entities.Models;

public class BaseDBO
{
    [Key, Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("modified_on")]
    public DateTime ModifiedOn { get; set; }

    [Column("create_on")]
    public DateTime CreatedOn { get; set; }
}
