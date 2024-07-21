using BusinessObjectLayer.Dtos;
using DataAccessLayer.DbContexts;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DAL_Notification : IDAL_Notification
    {
        private readonly Dbcontext _dbcontext;

        public DAL_Notification(Dbcontext dbcontext)
        {
            _dbcontext = dbcontext;

        }

        public async Task<NotificationTemplate> GetNotificationTemplate(int id)
        {
            return await _dbcontext.NotificationTemplates.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<int> SaveNotification(Notification model)
        {
            _dbcontext.Notifications.Add(model);
            return await _dbcontext.SaveChangesAsync();
        }

        //Notification List Worked //
        public async Task<IEnumerable<BOL_NotificationViewModel>> GetAllNotificationByUser(int userid)
        {
            return _dbcontext.Notifications.
                  Include(n => n.NotificationType).
                  Where(n => n.NotifyTo == userid).
                  Select(n => new BOL_NotificationViewModel()
                  {
                      Message = n.Message,
                      IsSeen = n.IsSeen,
                      CreatedOn = n.CreatedOn,
                      NotificationTypeId = n.NotificationTypeId
                  }
                  ).OrderByDescending(l => l.CreatedOn).ToList();


        }

        public async Task<int> MarkAllNotificationsAsRead(int userId)
        {
            var markNotification = await _dbcontext.Notifications.Where(o => o.NotifyTo == userId).ToListAsync();
            foreach (var item in markNotification)
            {
                item.IsSeen = true;

            }
            return _dbcontext.SaveChanges();

        }

        public async Task<BOL_LatestNotifications> GetLatestNotifications(int rows, int userid)
        {
            var model = new BOL_LatestNotifications();
            model.UnReadCount = _dbcontext.Notifications.Where(n => n.NotifyTo == userid && n.IsSeen == false).ToList().Count();
            model.Notifications = _dbcontext.Notifications

                 .Where(l => l.NotifyTo == userid)
                 .Select(l => new BOL_NotificationViewModel()
                 {
                     Message = l.Message,
                     IsSeen = l.IsSeen,
                     CreatedOn = l.CreatedOn,

                 }
                 ).OrderByDescending(l=> l.CreatedOn).Take(5).ToList();
            return model;
        }
    }
}
