using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class BOL_AddLeave
    {
        [JsonIgnore]
        public int RequestedBy { get; set; }
        public int LeaveTypeId { get; set; }
        public string Reasons { get; set; }

    }
}
