using BusinessObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDAL_Notification
    {
        Task<NotificationTemplate> GetNotificationTemplate(int id);

        Task<int> SaveNotification(Notification model);

        Task<IEnumerable<BOL_NotificationViewModel>> GetAllNotificationByUser(int userid);

        Task<int> MarkAllNotificationsAsRead(int userId);

        Task<BOL_LatestNotifications> GetLatestNotifications(int rows, int userid);
    }
}
