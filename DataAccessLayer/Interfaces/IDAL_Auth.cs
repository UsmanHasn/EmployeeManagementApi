using BusinesObjectLayer.Dtos;
using BusinessObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDAL_Auth
    {
        Task<int> RegisterUser(BOL_RegisterUser model);
        Task<bool> IsEmailExists(string email);
        Task<User> GetUserByEmail(string email);

        Task<User> VerifyUser(BOL_LoginRequest model);

        Task<Attendence> GetAttendenceByUserId(int Id, DateTime dateTime);

        Task<User> UpdateProfile(BOL_UpdateUser model);

        Task<int> ResetUserPassword(BOL_ResetUserPassword model);

    }
}
