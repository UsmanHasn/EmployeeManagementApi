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
                    

                }
                ).ToList();

        }
    }
}
