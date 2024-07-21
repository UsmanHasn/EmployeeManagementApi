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
using DataAccessLayer;
using Azure;

namespace BusinessLogicLayer
{
    public class BLL_Admin:IBLL_Admin
    {
        private readonly IDAL_Admin _IDAL_Admin;

        private readonly IGeneralFunctions _IGeneralFunctions;

        private readonly IBLL_Notification _IBLL_Notification;

        public BLL_Admin(IDAL_Admin iDAL_Admin,IGeneralFunctions iGeneralFunctions, IBLL_Notification iBLL_Notification)
        {
            _IDAL_Admin = iDAL_Admin;
            _IGeneralFunctions = iGeneralFunctions;
            _IBLL_Notification = iBLL_Notification; 
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
                await _IBLL_Notification.LeaveRequestApproveOrRejected(response.Data, model.StatusId);

                if (model.StatusId == 2  )
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

        public async Task<BOL_ApiResponse<IEnumerable<BOL_UserViewModel>>> GetAllEmployees()
        {
            var response = new BOL_ApiResponse<IEnumerable<BOL_UserViewModel>>();
            try
            {
                response.Data = await _IDAL_Admin.GetAllEmployees();
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

        public async Task<BOL_ApiResponse<int>> MarkUserAsIsActiveOrInActive(BOL_ToggleStatus model)
        {
            var response = new BOL_ApiResponse<int>();
            try
            {
                response.Data = await _IDAL_Admin.MarkUserAsIsActiveOrInActive(model);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Employee Status Updated";
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BOL_ApiResponse<int>> MarkUserAsDeleted(string identifier)
        {
            var response = new BOL_ApiResponse<int>();
            try
            {
                response.Data = await _IDAL_Admin.MarkUserAsDeleted(identifier);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Employee Status Updated";
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }


        public async Task<BOL_ApiResponse<BOL_UserViewModel>> GetEmployeeByIdentifier(string identifier)
        {
            var response = new BOL_ApiResponse<BOL_UserViewModel>();
            try
            {
                response.Data = await _IDAL_Admin.GetEmployeeByIdentifier(identifier);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message =ex.Message;
            }
            return response;
        }

        public async Task<BOL_ApiResponse<int>> UpdateEmployee(BOL_UserViewModel model)
        {
            var response = new BOL_ApiResponse<int>();
            try
            {
                response.Data = await _IDAL_Admin.UpdateEmployee(model);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Employee Updated Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }


        public async Task<BOL_ApiResponse<BOL_AdminDashboard>> GetAdminDashboard()
        {
            var response = new BOL_ApiResponse<BOL_AdminDashboard>();
            try
            {
                response.Data = await _IDAL_Admin.GetAdminDashboard();
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successfully";
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
