using BusinesObjectLayer.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesObjectLayer.Dtos
{
    public class BOL_UserDto
    {

        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNo { get; set; }


        public string Adress { get; set; } = null!;


        public string AuthToken { get; set; } = null!;

        public string Identifier { get; set; } = null!;

        public int UserTypeId { get; set; }

        public DateTime? TimedIn { get; set; }
        public DateTime? TimedOut { get; set; }

        public string ProfilePic {  get; set; }

        public DateTime CreatedOn { get; set;}
    }
}

