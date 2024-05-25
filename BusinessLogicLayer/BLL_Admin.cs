using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLL_Admin:IBLL_Admin
    {
        private readonly IDAL_Admin _IDAL_Admin;

        public BLL_Admin(IDAL_Admin iDAL_Admin)
        {
            _IDAL_Admin = iDAL_Admin;
        }
    }
}
