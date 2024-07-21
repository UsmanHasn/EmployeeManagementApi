using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class BOL_AdminDashboard
    {
        public int TotalEmployeeCount { get; set; }
        public int PendingLeaveRequests { get; set; }
        public int EmployeeOnLeaves { get; set; }
        public IEnumerable<BOL_LeaveRequestViewModel> LeaveRequests { get; set; }   

    }
}
