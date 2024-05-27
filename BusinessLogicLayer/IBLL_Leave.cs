using BusinesObjectLayer.Dtos;
using BusinessObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IBLL_Leave
    {
        Task<BOL_ApiResponse<int>> AddLeaveRequest(BOL_AddLeave model);

        Task<BOL_ApiResponse<IEnumerable<BOL_DropdownModel>>> GetLeaveRequestTypes();

        Task<BOL_ApiResponse<IEnumerable<BOL_LeaveRequestViewModel>>> GetAllMyLeaveRequests(int userid);
    }
}
