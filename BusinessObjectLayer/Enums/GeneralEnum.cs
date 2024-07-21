using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesObjectLayer.Enums
{
    public enum UserTypeEnum
    {
        
        Admin = 1,
        Employee = 2

    }

    public enum NotificationsTemplate
    {
        LoggedIn = 1,
        RequestApproved = 2,
        RequestRejected = 3,
        NewLeaveRequest = 4,

    }

    public enum Placeholder
    {
        DateTime,
        UserName,
    }
}
