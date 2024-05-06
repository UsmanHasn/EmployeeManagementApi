using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

[Table("Leave", Schema = "Leave")]
public partial class Leave
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Identifier { get; set; } = null!;

    public int RequestedBy { get; set; }

    public int? ApprovedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int LeaveTypeId { get; set; }

    public int LeaveStatusId { get; set; }
}
