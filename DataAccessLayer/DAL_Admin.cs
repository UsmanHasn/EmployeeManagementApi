using BusinessObjectLayer.Dtos;
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
    }
}
