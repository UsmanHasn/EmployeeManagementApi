using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IBLL_Admin _IBLL_Admin;

        public AdminController(IBLL_Admin iBLL_Admin)
        {
            _IBLL_Admin = iBLL_Admin;
        }
    }
}
