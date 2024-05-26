using BusinesObjectLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
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

        [HttpGet, Route("GetAllLeaveRequests")]
        public async Task<BOL_ApiResponse<IEnumerable<BOL_LeaveRequestViewModel>>> GetAllLeaveRequests()
        {
            return await _IBLL_Admin.GetAllLeaveRequests();

        }

    }
}
