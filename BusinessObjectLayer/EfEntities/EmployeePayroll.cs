using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

[Table("EmployeePayroll", Schema = "Payroll")]
public partial class EmployeePayroll
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Identifier { get; set; } = null!;

    public int PayrollFor { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int TotalLeave { get; set; }

    public int TotalLateIn { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal BasicPay { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalDeduction { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalSalary { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Bonus { get; set; }
}
