using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

[Table("NotificationTemplate", Schema = "Notification")]
public partial class NotificationTemplate
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Unicode(false)]
    public string Body { get; set; } = null!;

    [Unicode(false)]
    public string? EmailBody { get; set; }

    public bool? IsActive { get; set; }

    [InverseProperty("NotificationType")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
