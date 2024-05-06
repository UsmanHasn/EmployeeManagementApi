using BusinesObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBLL_Attendence
    {
        Task<BOL_ApiResponse<int>> TimeIn(int Id);
        Task<BOL_ApiResponse<int>> TimeOut(int Id);
    }
}
