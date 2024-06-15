using BusinesObjectLayer.Dtos;
using BusinessObjectLayer.Dtos;
using DataAccessLayer.DbContexts;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataAccessLayer
{
    public class DAL_Admin : IDAL_Admin
    {
        private readonly Dbcontext _Dbcontext;

        public DAL_Admin(Dbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        public async Task<IEnumerable<BOL_LeaveRequestViewModel>> GetAllLeaveRequests()
        {
            return _Dbcontext.Leaves.Include(l => l.LeaveType)
                .Include(l => l.LeaveStatus)
                .Include(l => l.ApprovedByNavigation)
                .Include(l => l.RequestedByNavigation)
                .Select(l => new BOL_LeaveRequestViewModel()
                {
                    Identifier = l.Identifier,
                    RequestedBy = l.RequestedBy,
                    ApprovedBy = l.ApprovedBy ?? 0,
                    CreatedOn = l.CreatedOn,
                    LeaveTypeId = l.LeaveTypeId,
                    LeaveStatusId = l.LeaveStatusId,
                    LeaveType = l.LeaveType.Title,
                    LeaveStatus = l.LeaveStatus.Title!,
                    RequestedByName = l.RequestedByNavigation.Name,
                    RequestedByIdentifier = l.RequestedByNavigation.Identifier,

                }
                ).ToList();

        }

        public async Task<int> ApproveOrRejectLeave(BOL_ApproveOrRejectLeave model)
        {
            var leave = await _Dbcontext.Leaves.FirstOrDefaultAsync(l => l.Identifier == model.Identifier);
            if (leave != null)
            {
                leave.LeaveStatusId = model.StatusId;
                leave.AdminRemarks = model.AdminRemarks;
                leave.ApprovedBy = model.UserId;
                leave.UpdatedOn = DateTime.UtcNow;

                return await _Dbcontext.SaveChangesAsync();
            }
            else { return 0; }
        }

        public async Task<IEnumerable<BOL_UserViewModel>> GetAllEmployees()
        {
            return _Dbcontext.Users
                .Where(u => u.UsertypeId == 2 && u.IsDeleted == false)
                .Select(l => new BOL_UserViewModel()
                {
                    Identifier = l.Identifier,
                    FirstName = l.Name,
                    Email = l.Email ,
                    PhoneNo = l.PhoneNo,
                    Adress = l.Address,
                    ProfilePic = l.ProfilePictureUrl,
                    CreatedOn = l.CreatedOn,
                    IsActive = l.IsActive,
                    IsDeleted = l.IsDeleted,

                }
                ).ToList();

        }

        public async Task<int> MarkUserAsIsActiveOrInActive(BOL_ToggleStatus model)
        {
            var user = await _Dbcontext.Users.FirstOrDefaultAsync(u => u.Identifier == model.Identifier);
            if(user != null)
            {
                user.IsActive = model.Status;
                
            }
            return await _Dbcontext.SaveChangesAsync();
        }

        public async Task<int> MarkUserAsDeleted(string Identifier)
        {
            var user = await _Dbcontext.Users.FirstOrDefaultAsync(u => u.Identifier == Identifier);
            if (user != null)
            {
                user.IsDeleted = true;

            }
            return await _Dbcontext.SaveChangesAsync();
        }

        public async Task<BOL_UserViewModel> GetEmployeeByIdentifier(string identifier)
        {
            var user = await _Dbcontext.Users
                .FirstOrDefaultAsync(l => l.Identifier == identifier);

            if (user == null)
            {
                return null;
            }

            return new BOL_UserViewModel()
            {
                Identifier = user.Identifier,
                FirstName = user.Name,
                Email = user.Email,
                PhoneNo = user.PhoneNo,
                Adress = user.Address,
                ProfilePic = user.ProfilePictureUrl,
                CreatedOn = user.CreatedOn,
                IsActive = user.IsActive,
                IsDeleted = user.IsDeleted,
            };
        }

        public async Task<int> UpdateEmployee(BOL_UserViewModel model)
        {
            var employee = await _Dbcontext.Users.FirstOrDefaultAsync(e => e.Identifier == model.Identifier);
            if (employee != null)
            {
                employee.Name = model.FirstName;
                employee.PhoneNo = model.PhoneNo;
                employee.Address = model.Adress;
                employee.ProfilePictureUrl = model.ProfilePic;
                employee.UpdatedOn = DateTime.UtcNow;

                return await _Dbcontext.SaveChangesAsync();

            }
            else
            {
                return 0;
            }
        }

    }
}
