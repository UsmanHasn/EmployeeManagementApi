using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace BusinessLogicLayer
{
    public class BLL_LoggedInUser : IBLL_LoggedInUser
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public BLL_LoggedInUser(IHttpContextAccessor IhttpContextAccessor)
        {
            _IHttpContextAccessor = IhttpContextAccessor;
        }

        public int GetLoggedinUserId()
        {
            try
            {
                return Convert.ToInt32(_IHttpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).FirstOrDefault());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string GetLoggedInUserEmail()
        {
            try
            {
                return _IHttpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }

        public string GetLoggedInUserName()
        {
            try
            {
                return _IHttpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).FirstOrDefault();
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }
    }
}
