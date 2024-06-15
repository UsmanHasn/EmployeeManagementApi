using BusinesObjectLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

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

        [HttpPost,Route("ApproveOrRejectLeave")]
        public async Task<BOL_ApiResponse<int>> ApproveOrRejectLeave(BOL_ApproveOrRejectLeave model)
        {
            return await _IBLL_Admin.ApproveOrRejectLeave(model);
        }

        [HttpGet, Route("GetAllEmployees")]

        public async Task<BOL_ApiResponse<IEnumerable<BOL_UserViewModel>>> GetAllEmployees()
        {
            return await _IBLL_Admin.GetAllEmployees();
        }

        [HttpPost,Route("MarkUserAsIsActiveOrInActive")]

        public async Task<BOL_ApiResponse<int>> MarkUserAsIsActiveOrInActive(BOL_ToggleStatus model)
        {
            return await _IBLL_Admin.MarkUserAsIsActiveOrInActive(model);
        }

        [HttpGet, Route("MarkUserAsDeleted")]

        public async Task<BOL_ApiResponse<int>> MarkUserAsDeleted(string Identifier)
        {
            return await _IBLL_Admin.MarkUserAsDeleted(Identifier);
        }

        [HttpGet,Route("GetEmployeeByIdentifier")]

        public async Task<BOL_ApiResponse<BOL_UserViewModel>> GetEmployeeByIdentifier(string Identifier)
        {
            return await _IBLL_Admin.GetEmployeeByIdentifier(Identifier);
        }

        [HttpPost, Route("UpdateEmployee")]

        public async Task<BOL_ApiResponse<int>> UpdateEmployee(BOL_UserViewModel model)
        {
            return await _IBLL_Admin.UpdateEmployee(model);
        }

    }
}
