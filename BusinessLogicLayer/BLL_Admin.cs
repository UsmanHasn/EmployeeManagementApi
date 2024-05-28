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
using BusinessLogicLayer.Helper;

namespace BusinessLogicLayer
{
    public class BLL_Admin:IBLL_Admin
    {
        private readonly IDAL_Admin _IDAL_Admin;

        private readonly IGeneralFunctions _IGeneralFunctions;

        public BLL_Admin(IDAL_Admin iDAL_Admin,IGeneralFunctions iGeneralFunctions)
        {
            _IDAL_Admin = iDAL_Admin;
            _IGeneralFunctions = iGeneralFunctions;
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

        public async Task<BOL_ApiResponse<int>> ApproveOrRejectLeave(BOL_ApproveOrRejectLeave model)
        {
            var response = new BOL_ApiResponse<int>();
            try
            {
                model.UserId = _IGeneralFunctions.GetLoggedInUserId();
                response.Data = await _IDAL_Admin.ApproveOrRejectLeave(model);
                response.StatusCode = HttpStatusCode.OK;
                if (model.StatusId == 2 )
                {
                    response.Message = "Leave Successfully Approved";

                }
                else
                {
                    response.Message = "Leave Rejected";

                }

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
