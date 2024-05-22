using BusinesObjectLayer.Dtos;
using BusinessObjectLayer.Dtos;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
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

        Task<BOL_ApiResponse<string>> UploadProfilePicture(HttpRequest request);

        Task<BOL_ApiResponse<User>> Updateprofile(BOL_UpdateUser model);

        Task<BOL_ApiResponse<int>> ResetUserPassword(BOL_ResetUserPassword model);
    }
}
