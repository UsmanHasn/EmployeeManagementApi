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

        Task<BOL_ApiResponse<IEnumerable<BOL_UserViewModel>>> GetAllEmployees();

        Task<BOL_ApiResponse<int>> MarkUserAsIsActiveOrInActive(BOL_ToggleStatus model);

        Task<BOL_ApiResponse<int>> MarkUserAsDeleted(string identifier);

        Task<BOL_ApiResponse<BOL_UserViewModel>> GetEmployeeByIdentifier(string identifier);

        Task<BOL_ApiResponse<int>> UpdateEmployee(BOL_UserViewModel model);

    }
}
