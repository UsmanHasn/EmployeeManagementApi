using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public  class BOL_LatestNotifications
    {
        public int UnReadCount { get; set; }    

        public IEnumerable<BOL_NotificationViewModel> Notifications { get; set; }   
    }
}
