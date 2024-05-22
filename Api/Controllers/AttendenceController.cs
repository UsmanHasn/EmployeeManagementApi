using BusinesObjectLayer.Dtos;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IBLL_Attendence _IBLL_Attendence;

        private readonly IBLL_Leave _IBLL_Leave;

        public AttendenceController(IBLL_Attendence iBLL_Attendence,IBLL_Leave iBLL_Leave)
        {
            _IBLL_Attendence = iBLL_Attendence;
            _IBLL_Leave = iBLL_Leave;
        }

        [HttpGet, Route("TimeIn")]

        public async Task<BOL_ApiResponse<int>> TimeIn()
        {
            return await _IBLL_Attendence.TimeIn();
        }

        [HttpGet, Route("TimeOut")]

        public async Task<BOL_ApiResponse<int>> TimeOut()
        {
            return await _IBLL_Attendence.TimeOut();
        }

        [HttpGet, Route("GetAttendenceByUserId")]

        public async Task<BOL_ApiResponse<IEnumerable<Attendence>>> GetAttendencebyUserId()
        {
            return await _IBLL_Attendence.GetAttendencebyUserId();
        }

        [HttpPost, Route("AddLeaveRequest")]
        public async Task<BOL_ApiResponse<int>> AddLeaveRequest(BOL_AddLeave model)
        {
            return await _IBLL_Leave.AddLeaveRequest(model);
        }
    }
}
