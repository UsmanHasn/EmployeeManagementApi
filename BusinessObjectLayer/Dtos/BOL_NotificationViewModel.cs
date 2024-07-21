using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public  class BOL_NotificationViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; } = null!;

        public int NotifyTo { get; set; }

        public int NotificationTypeId { get; set; }

        public bool IsSeen { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual NotificationTemplate NotificationType { get; set; } = null!;

        public virtual User NotifyToNavigation { get; set; } = null!;
    }
}
