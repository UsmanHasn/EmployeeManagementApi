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

    [Unicode(false)]
    public string? Reason { get; set; }

    [Unicode(false)]
    public string? AdminRemarks { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    [ForeignKey("ApprovedBy")]
    [InverseProperty("LeaveApprovedByNavigations")]
    public virtual User? ApprovedByNavigation { get; set; }

    [ForeignKey("LeaveStatusId")]
    [InverseProperty("Leaves")]
    public virtual LeaveStatus LeaveStatus { get; set; } = null!;

    [ForeignKey("LeaveTypeId")]
    [InverseProperty("Leaves")]
    public virtual LeaveType LeaveType { get; set; } = null!;

    [ForeignKey("RequestedBy")]
    [InverseProperty("LeaveRequestedByNavigations")]
    public virtual User RequestedByNavigation { get; set; } = null!;
}
