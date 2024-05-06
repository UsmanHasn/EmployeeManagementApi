using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesObjectLayer.Dtos
{
    public class BOL_RegisterUser
    {
        //Data Annotation
        public string Name { get; set; } = null!;

        [StringLength(100)]
        [Unicode(false)]
        public string Email { get; set; } = null!;

        [StringLength(100)]
        public string Password { get; set; } = null!;

        [StringLength(100)]
        public string ConfirmPassword { get; set; } = null!;

        [StringLength(200)]
        [Unicode(false)]
        public string Address { get; set; } = null!;

        [StringLength(20)]
        [Unicode(false)]
        public string PhoneNo { get; set; } = null!;

        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Dob { get; set; }

        [StringLength(200)]
        [Unicode(false)]
        public string? ProfilePictureUrl { get; set; }

    }
}
