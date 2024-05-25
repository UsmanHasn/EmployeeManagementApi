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
    public class DAL_Admin:IDAL_Admin
    {
        private readonly Dbcontext _Dbcontext;

        public DAL_Admin(Dbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        //public async Task<BOL_LeaveRequestViewModel> GetAllRequest()
        //{
        //}
    }
}
