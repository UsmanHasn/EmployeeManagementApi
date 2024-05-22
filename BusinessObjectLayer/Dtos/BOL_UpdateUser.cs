using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public  class BOL_UpdateUser
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string ProfilePictureUrl { get; set; }

        [JsonIgnore]
        public int  UpdatedBy { get; set; }
    }
}
