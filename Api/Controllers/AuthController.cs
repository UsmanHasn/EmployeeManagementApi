using BusinesObjectLayer.Dtos;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
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



    }
}
