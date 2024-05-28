using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class BOL_ApproveOrRejectLeave
    {
        public string Identifier { get; set; }
        public int StatusId { get; set; }
        public string AdminRemarks { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
    }
}
