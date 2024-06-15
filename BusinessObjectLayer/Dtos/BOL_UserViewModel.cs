using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class BOL_UserViewModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNo { get; set; }
        public string Adress { get; set; } = null!;

        public string Identifier { get; set; } = null!;



        public string ProfilePic { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
