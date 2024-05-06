using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

[Table("UserType", Schema = "Auth")]
public partial class UserType
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [InverseProperty("Usertype")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
