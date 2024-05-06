using BusinesObjectLayer.Dtos;
using DataAccessLayer.DbContexts;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DAL_Auth:IDAL_Auth
    {
        private readonly Dbcontext _dbcontext;

        public DAL_Auth(Dbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public async Task<int> RegisterUser(BOL_RegisterUser model)
        {
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                PhoneNo = model.PhoneNo,
                CreatedBy = model.CreatedBy,
                Dob = model.Dob,
                ProfilePictureUrl = model.ProfilePictureUrl,
                Address = model.Address,

            };
            _dbcontext.Users.Add(user);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> IsEmailExists(string email)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false;
            }
            return true;
            //return user != null;
        }

        public async Task<User> GetUserByEmail(string email)
        {
           return  await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    
    
        public async Task<User> VerifyUser(BOL_LoginRequest model)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
        }

        public async Task<Attendence> GetAttendenceByUserId(int Id, DateTime dateTime)
        {
            return await _dbcontext.Attendences.FirstOrDefaultAsync(a => a.EmployeeId == Id && a.CreatedOn.Date == dateTime.Date);

        }
    }

}
