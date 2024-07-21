using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

[Table("Notifications", Schema = "Notification")]
public partial class Notification
{
    [Key]
    public int Id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Message { get; set; } = null!;

    public int NotifyTo { get; set; }

    public int NotificationTypeId { get; set; }

    public bool IsSeen { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [ForeignKey("NotificationTypeId")]
    [InverseProperty("Notifications")]
    public virtual NotificationTemplate NotificationType { get; set; } = null!;

    [ForeignKey("NotifyTo")]
    [InverseProperty("Notifications")]
    public virtual User NotifyToNavigation { get; set; } = null!;
}
