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
    public class AuthController : ControllerBase
    {
        private readonly IBLL_Auth _IBLL_Auth;

        public AuthController(IBLL_Auth IBLL_Auth)
        {
            _IBLL_Auth = IBLL_Auth;
        }

        [HttpPost,Route("RegisterUser")]
        public async Task<BOL_ApiResponse<int>> RegisterUser(BOL_RegisterUser model)
        {
           return await _IBLL_Auth.RegisterUser(model);
        }

        [HttpPost, Route("LoginUser")]
        public async Task<BOL_ApiResponse<BOL_UserDto>> LoginUser(BOL_LoginRequest model)
        {
            return await _IBLL_Auth.LoginUser(model);
        }

        [HttpPost, Route("UploadProfilePicture")]
        public async Task<BOL_ApiResponse<string>> UploadProfilePicture(IFormFile file)
        {
            return await _IBLL_Auth.UploadProfilePicture(HttpContext.Request);
        }


        [HttpPost, Route("UpdateProfile")]
        public async Task<BOL_ApiResponse<User>> Updateprofile(BOL_UpdateUser model)
        {
            return await _IBLL_Auth.Updateprofile(model);
        }



        [HttpPost, Route("ResetUserPassword")]
         public async Task<BOL_ApiResponse<int>> ResetUserPassword(BOL_ResetUserPassword model)
        {
            return await _IBLL_Auth.ResetUserPassword(model);
        }
    }
}
