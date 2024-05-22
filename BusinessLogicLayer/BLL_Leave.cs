using BusinesObjectLayer.Dtos;
using BusinessLogicLayer.Helper;
using BusinessObjectLayer.Dtos;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BusinessLogicLayer
{
    public  class BLL_Leave:IBLL_Leave
    {
        private readonly IDAL_Leave _IDAL_Leave;

        private readonly IGeneralFunctions _IGeneralFunctions;
        public BLL_Leave(IDAL_Leave iDAL_Leave,IGeneralFunctions iGeneralFunctions)
        {
            _IDAL_Leave = iDAL_Leave;
            _IGeneralFunctions = iGeneralFunctions;
        }   
        public async Task<BOL_ApiResponse<int>> AddLeaveRequest(BOL_AddLeave model)
        {

            var response = new BOL_ApiResponse<int>();
            try
            {
                model.RequestedBy = _IGeneralFunctions.GetLoggedInUserId();
                response.Data = await _IDAL_Leave.AddLeaveRequest(model);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successfull";
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BOL_ApiResponse<IEnumerable<BOL_DropdownModel>>> GetLeaveRequestTypes()
        {
            var response = new BOL_ApiResponse<IEnumerable<BOL_DropdownModel>>();
            try
            {
                response.Data = await _IDAL_Leave.GetLeaveRequestTypes();
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successfull";

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
