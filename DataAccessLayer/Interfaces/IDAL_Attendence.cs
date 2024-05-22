using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDAL_Attendence
    {
        Task<int> TimeIn(int Id);

        Task<int> TimeOut(int Id);

        Task<IEnumerable<Attendence>> GetAttendanceByUserId(int userId);
    }
}
