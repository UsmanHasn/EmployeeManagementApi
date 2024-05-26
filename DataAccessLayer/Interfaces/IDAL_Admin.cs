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

    }
}
