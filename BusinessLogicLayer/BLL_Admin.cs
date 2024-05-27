using BusinesObjectLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using DataAccessLayer.Interfaces;
using System.Net;
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

        public async Task<BOL_ApiResponse<IEnumerable<BOL_LeaveRequestViewModel>>> GetAllLeaveRequests()
        {
            var response = new BOL_ApiResponse<IEnumerable<BOL_LeaveRequestViewModel>>();
            try
            {
                response.Data = await _IDAL_Admin.GetAllLeaveRequests();
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Success";

            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;

            }
            return response;
        }

    }
}
