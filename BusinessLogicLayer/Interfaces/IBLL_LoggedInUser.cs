using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBLL_LoggedInUser
    {
        int GetLoggedinUserId();

        string GetLoggedInUserEmail();

        string GetLoggedInUserName();
    }
}
