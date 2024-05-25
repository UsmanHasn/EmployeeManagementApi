using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public  class BOL_LeaveRequestViewModel
    {
        public string? Identifier { get; set; }
        public int  RequestedBy { get; set; }
        public int  ApprovedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public int LeaveTypeId { get; set; }
        public int LeaveStatusId { get; set; }
        





    }
}
