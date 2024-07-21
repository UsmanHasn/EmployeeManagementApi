using BusinesObjectLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using BusinesObjectLayer.Enums;
using DataAccessLayer;
using BusinessObjectLayer.Dtos;
using BusinessLogicLayer.Helper;

namespace BusinessLogicLayer
{
    public class BLL_Notification : IBLL_Notification
    {
        private readonly IDAL_Notification _IDAL_Notification;

        private readonly IGeneralFunctions _IGeneralFunctions;

        private readonly IDAL_Auth _IDAL_Auth;

        public BLL_Notification(IDAL_Notification iDAL_Notification, IGeneralFunctions iGeneralFunctions, IDAL_Auth iDAL_Auth)
        {
            _IDAL_Notification = iDAL_Notification;
            _IGeneralFunctions = iGeneralFunctions;
            _IDAL_Auth = iDAL_Auth;
        }

        public async Task<BOL_ApiResponse<bool>> GenerateNotification(int TypeId, int NotifyTo, Dictionary<Placeholder, string> placeholders)
        {
            var response = new BOL_ApiResponse<bool>();
            try
            {
                var template = await _IDAL_Notification.GetNotificationTemplate(TypeId);
                if (template != null)
                {
                    var message = ReplacePlaceholders(template.Body, placeholders);
                    var notification = new Notification
                    {
                        Message = message,
                        NotifyTo = NotifyTo,
                        NotificationTypeId = TypeId,

                    };
                    var save = await _IDAL_Notification.SaveNotification(notification);
                    if (save > 0)
                    {
                        response.Data = true;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = "Notification sent";
                    }
                    else
                    {
                        response.Data = false;
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.Message = "Something Went Wrong";
                    }
                }
                else
                {
                    response.Data = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Template not Found";

                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;

        }

        public static string ReplacePlaceholders(string messageTemplate, Dictionary<Placeholder, string> replacements)
        {
            foreach (var entry in replacements)
            {
                string placeholder = $"[{entry.Key}]";
                messageTemplate = messageTemplate.Replace(placeholder, entry.Value);
            }
            return messageTemplate;
        }


        public async Task<BOL_ApiResponse<IEnumerable<BOL_NotificationViewModel>>> GetAllNotificationByUser(int userid)
        {
            var response = new BOL_ApiResponse<IEnumerable<BOL_NotificationViewModel>>();
            try
            {
                response.Data = await _IDAL_Notification.GetAllNotificationByUser(_IGeneralFunctions.GetLoggedInUserId());
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

        public async Task<BOL_ApiResponse<int>> MarkAllNotificationsAsRead()
        {
            var response = new BOL_ApiResponse<int>();
            try
            {
                response.Data = await _IDAL_Notification.MarkAllNotificationsAsRead(_IGeneralFunctions.GetLoggedInUserId());
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Mark Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BOL_ApiResponse<BOL_LatestNotifications>> GetLatestNotifications(int rows)
        {
            var response = new BOL_ApiResponse<BOL_LatestNotifications>();

            try
            {
                response.Data = await _IDAL_Notification.GetLatestNotifications(rows, _IGeneralFunctions.GetLoggedInUserId());
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Row Count Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<bool> LeaveRequestApproveOrRejected(int notifyto, int requeststatus)
        {
            var user = await _IDAL_Auth.GetUserByUserId(notifyto);
            var placeholder = new Dictionary<Placeholder, string>();
            placeholder.Add(Placeholder.UserName, user.Name);
            placeholder.Add(Placeholder.DateTime,DateTime.Now.ToString());
            
            if (requeststatus == 2)
            {
                await GenerateNotification((int)NotificationsTemplate.RequestApproved, notifyto, placeholder);
            }
            else
            {
                await GenerateNotification((int)NotificationsTemplate.RequestRejected, notifyto, placeholder);


            }
            return true;
        }

    }
}
