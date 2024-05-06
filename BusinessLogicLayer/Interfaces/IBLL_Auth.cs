using BusinesObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBLL_Auth
    {
        Task<BOL_ApiResponse<int>> RegisterUser(BOL_RegisterUser model);


        Task<BOL_ApiResponse<BOL_UserDto>> LoginUser(BOL_LoginRequest model);
    }
}
