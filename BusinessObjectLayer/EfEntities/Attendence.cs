using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

[Table("Attendence", Schema = "Attendence")]
public partial class Attendence
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime TimedIn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TimeOut { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int EmployeeId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? TotalHours { get; set; }
}
