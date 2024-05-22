using BusinesObjectLayer.Dtos;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBLL_Attendence
    {
        Task<BOL_ApiResponse<int>> TimeIn();
        Task<BOL_ApiResponse<int>> TimeOut();

        Task<BOL_ApiResponse<IEnumerable<Attendence>>> GetAttendencebyUserId();
    }
}
