using BusinesObjectLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IBLL_Notification _IBLL_Notification;

        public NotificationController(IBLL_Notification iBLL_Notification)
        {
            _IBLL_Notification = iBLL_Notification;
        }

        [HttpGet,Route("GetAllNotificationByUser")]
        public async Task<BOL_ApiResponse<IEnumerable<BOL_NotificationViewModel>>> GetAllNotificationByUser(int userid)
        {
            return await _IBLL_Notification.GetAllNotificationByUser(userid);
        }

        [HttpGet,Route("MarkAllNotificationsAsRead")]
        public async Task<BOL_ApiResponse<int>> MarkAllNotificationsAsRead()
        {
            return await _IBLL_Notification.MarkAllNotificationsAsRead();
        }

        [HttpGet,Route("GetLatestNotifications")]

        public async Task<BOL_ApiResponse<BOL_LatestNotifications>> GetLatestNotifications(int rows)
        {
            return await _IBLL_Notification.GetLatestNotifications(rows);
        }
    }
}
