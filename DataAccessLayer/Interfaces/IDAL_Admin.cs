using BusinesObjectLayer.Dtos;
using BusinessObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDAL_Admin
    {
        Task<IEnumerable<BOL_LeaveRequestViewModel>> GetAllLeaveRequests();

        Task<int> ApproveOrRejectLeave(BOL_ApproveOrRejectLeave model);

        Task<IEnumerable<BOL_UserViewModel>> GetAllEmployees();

        Task<int> MarkUserAsIsActiveOrInActive(BOL_ToggleStatus model);

        Task<int> MarkUserAsDeleted(string Identifier);

        Task<BOL_UserViewModel> GetEmployeeByIdentifier(string identifier);

        Task<int> UpdateEmployee(BOL_UserViewModel model);
    }
}
