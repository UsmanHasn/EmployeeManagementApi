using BusinesObjectLayer.Dtos;
using BusinessObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBLL_Admin
    {
        Task<BOL_ApiResponse<IEnumerable<BOL_LeaveRequestViewModel>>> GetAllLeaveRequests();

        Task<BOL_ApiResponse<int>> ApproveOrRejectLeave(BOL_ApproveOrRejectLeave model);

    }
}
