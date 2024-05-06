using BusinesObjectLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IBLL_Attendence _IBLL_Attendence;

        public AttendenceController(IBLL_Attendence iBLL_Attendence)
        {
            _IBLL_Attendence = iBLL_Attendence;
        }

        [HttpGet, Route("TimeIn")]

        public async Task<BOL_ApiResponse<int>> TimeIn(int Id)
        {
            return await _IBLL_Attendence.TimeIn(Id);
        }

        [HttpGet, Route("TimeOut")]

        public async Task<BOL_ApiResponse<int>> TimeOut(int Id)
        {
            return await _IBLL_Attendence.TimeOut(Id);
        }

    }
}
