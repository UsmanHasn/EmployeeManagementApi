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
    public class DAL_Attendence : IDAL_Attendence
    {
        private readonly Dbcontext _dbcontext;

        public DAL_Attendence(Dbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<int> TimeIn(int Id)
        {
            var attendence = new Attendence
            {
                EmployeeId = Id
            };
            _dbcontext.Attendences.Add(attendence);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<int> TimeOut(int Id)
        {
            var Timelog = await _dbcontext.Attendences.FirstOrDefaultAsync(x => x.EmployeeId == Id && x.CreatedOn.Date == DateTime.UtcNow.Date);
            Timelog.TimeOut = DateTime.UtcNow;

            // Calculate total hours worked
            if (Timelog.TimeOut.HasValue && Timelog.TimedIn != null)
            {
                TimeSpan timeWorked = Timelog.TimeOut.Value - Timelog.TimedIn;
                Timelog.TotalHours = (decimal)timeWorked.TotalHours;
            }

            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Attendence>> GetAttendanceByUserId(int userId)
        {
            return await _dbcontext.Attendences
                .Where(x => x.EmployeeId == userId)
                .ToListAsync();
        }




    }
}
