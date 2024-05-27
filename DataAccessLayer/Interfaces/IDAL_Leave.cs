using BusinesObjectLayer.Dtos;
using BusinessObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDAL_Leave
    {
        Task<int> AddLeaveRequest(BOL_AddLeave model);

        Task<IEnumerable<BOL_DropdownModel>> GetLeaveRequestTypes();

        Task<IEnumerable<BOL_LeaveRequestViewModel>> GetAllMyLeaveRequests(int userId);

    }
}
