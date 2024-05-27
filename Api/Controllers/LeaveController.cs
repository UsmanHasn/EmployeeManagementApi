using BusinesObjectLayer.Dtos;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IBLL_Leave _IBLL_Leave;

        public LeaveController(IBLL_Leave iBLL_Leave)
        {
            _IBLL_Leave = iBLL_Leave;
        }

        [HttpPost, Route("AddLeaveRequest")]
        public async Task<BOL_ApiResponse<int>> AddLeaveRequest(BOL_AddLeave model)
        {
            return await _IBLL_Leave.AddLeaveRequest(model);
        }

        [HttpGet, Route("GetLeaveRequestTypes")]
        public async Task<BOL_ApiResponse<IEnumerable<BOL_DropdownModel>>> GetLeaveRequestTypes()
        {
            return await _IBLL_Leave.GetLeaveRequestTypes();
        }

        [HttpGet,Route("GetAllMyLeaveRequests")]

        public async Task<BOL_ApiResponse<IEnumerable<BOL_LeaveRequestViewModel>>> GetAllMyLeaveRequests(int userid)
        {
            return await _IBLL_Leave.GetAllMyLeaveRequests(userid);
        }
    }
}

