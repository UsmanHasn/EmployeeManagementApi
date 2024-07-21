using BusinesObjectLayer.Dtos;
using BusinesObjectLayer.Enums;
using BusinessObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBLL_Notification
    {
        Task<BOL_ApiResponse<bool>> GenerateNotification(int TypeId, int NotifyTo, Dictionary<Placeholder, string> placeholders);

        Task<BOL_ApiResponse<IEnumerable<BOL_NotificationViewModel>>> GetAllNotificationByUser(int userid);

        Task<BOL_ApiResponse<int>> MarkAllNotificationsAsRead();

        Task<BOL_ApiResponse<BOL_LatestNotifications>> GetLatestNotifications(int rows);

        Task<bool> LeaveRequestApproveOrRejected(int notifyto, int requeststatus);
    }
}
