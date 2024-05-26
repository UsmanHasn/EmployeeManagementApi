using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

[Table("LeaveStatus", Schema = "Leave")]
public partial class LeaveStatus
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Title { get; set; }

    [InverseProperty("LeaveStatus")]
    public virtual ICollection<Leave> Leaves { get; set; } = new List<Leave>();
}
