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
using BusinessLogicLayer.Interfaces;
using BusinesObjectLayer.Enums;

namespace BusinessLogicLayer
{
    public  class BLL_Leave:IBLL_Leave
    {
        private readonly IDAL_Leave _IDAL_Leave;

        private readonly IDAL_Auth _IDAL_Auth;

        private readonly IGeneralFunctions _IGeneralFunctions;

        private readonly IBLL_Notification _IBLL_Notification;
        public BLL_Leave(IDAL_Leave iDAL_Leave,IGeneralFunctions iGeneralFunctions,IBLL_Notification iBLL_Notification,IDAL_Auth iDAL_Auth)
        {
            _IDAL_Leave = iDAL_Leave;
            _IGeneralFunctions = iGeneralFunctions;
            _IBLL_Notification = iBLL_Notification;
            _IDAL_Auth = iDAL_Auth;
        }   
        public async Task<BOL_ApiResponse<int>> AddLeaveRequest(BOL_AddLeave model)
        {

            var response = new BOL_ApiResponse<int>();
            try
            {
                model.RequestedBy = _IGeneralFunctions.GetLoggedInUserId();
                response.Data = await _IDAL_Leave.AddLeaveRequest(model);
                var placeholders = new Dictionary<Placeholder, string>();
                placeholders.Add(Placeholder.UserName, _IGeneralFunctions.GetLoggedInUserName());
                var admins = await _IDAL_Auth.GetAllUsersByUserTypeId((int)UserTypeEnum.Admin);
                foreach (var admin in admins)
                {
                    await _IBLL_Notification.GenerateNotification((int)NotificationsTemplate.NewLeaveRequest, 2, placeholders);

                }

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

        public async Task<BOL_ApiResponse<IEnumerable<BOL_LeaveRequestViewModel>>> GetAllMyLeaveRequests(int userid)
        {
            var response = new BOL_ApiResponse<IEnumerable<BOL_LeaveRequestViewModel>>();
            try
            {
                response.Data = await _IDAL_Leave.GetAllMyLeaveRequests(_IGeneralFunctions.GetLoggedInUserId());
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
