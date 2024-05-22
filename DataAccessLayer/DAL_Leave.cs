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

namespace DataAccessLayer
{
    public class DAL_Leave : IDAL_Leave
    {
        private readonly Dbcontext _dbcontext;

        public DAL_Leave(Dbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<int> AddLeaveRequest(BOL_AddLeave model)
        {
            var leave = new Leave
            {
                RequestedBy = model.RequestedBy,
                LeaveTypeId = model.LeaveTypeId,

            };
            _dbcontext.Leaves.Add(leave);
            return await _dbcontext.SaveChangesAsync();

        }



        public async Task<IEnumerable<BOL_DropdownModel>> GetLeaveRequestTypes()
        {
            var leaveTypes = await _dbcontext.LeaveTypes.ToListAsync();
            return leaveTypes.Select(lt => new BOL_DropdownModel
            {
                Id = lt.Id,
                Title = lt.Title
            });
        }

        

    }
}
