using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

[Table("Users", Schema = "Auth")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Identifier { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string Password { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string Address { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNo { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Dob { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LoggedIn { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? ProfilePictureUrl { get; set; }

    public int UsertypeId { get; set; }

    [InverseProperty("ApprovedByNavigation")]
    public virtual ICollection<Leave> LeaveApprovedByNavigations { get; set; } = new List<Leave>();

    [InverseProperty("RequestedByNavigation")]
    public virtual ICollection<Leave> LeaveRequestedByNavigations { get; set; } = new List<Leave>();

    [ForeignKey("UsertypeId")]
    [InverseProperty("Users")]
    public virtual UserType Usertype { get; set; } = null!;
}
