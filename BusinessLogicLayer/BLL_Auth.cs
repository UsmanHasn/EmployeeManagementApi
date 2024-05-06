using BusinesObjectLayer.Dtos;
using BusinesObjectLayer.Enums;
using BusinessLogicLayer.Helper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLL_Auth : IBLL_Auth
    {
        private readonly IDAL_Auth _IDAL_Auth;
        private readonly IGeneralFunctions _IGeneralFunctions;
        public BLL_Auth(IDAL_Auth iDAL_Auth, IGeneralFunctions IgeneralFunctions)
        {
            _IDAL_Auth = iDAL_Auth;
            _IGeneralFunctions = IgeneralFunctions;
        }

        public async Task<BOL_ApiResponse<int>> RegisterUser(BOL_RegisterUser model)
        {
            var response = new BOL_ApiResponse<int>();
            try
            {
                if (model.Password == model.ConfirmPassword)
                {
                    if (await _IDAL_Auth.IsEmailExists(model.Email) == false)
                    {
                        model.CreatedBy = 1;
                        var rows = await _IDAL_Auth.RegisterUser(model);
                        if (rows > 0)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            response.Message = "Register Successfull";
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.InternalServerError;
                            response.Message = "Error on Processing";
                        }
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.Message = "Email Already Exists";
                    }
                }
                else
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Password and Confirm Password does not matched";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BOL_ApiResponse<BOL_UserDto>> LoginUser(BOL_LoginRequest model)
        {
            var response = new BOL_ApiResponse<BOL_UserDto>();
            try
            {
                var user = await _IDAL_Auth.VerifyUser(model);
                if (user == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid Crendential";
                }
                else
                {
                    var attendence = await _IDAL_Auth.GetAttendenceByUserId(user.Id, DateTime.UtcNow);
                    var userdto = new BOL_UserDto();
                    userdto.Id = user.Id;
                    userdto.FirstName = user.Name;
                    userdto.Email = user.Email;
                    userdto.Adress = user.Address;
                    userdto.UserTypeId = user.UsertypeId;
                    userdto.Identifier = user.Identifier;
                    if (attendence != null)
                    {
                        userdto.TimedIn = attendence.TimedIn;
                        userdto.TimedOut = attendence.TimeOut;
                    }
                    response.Data = GenerateJsonWebToken(userdto);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = "Login Successfull";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        private BOL_UserDto GenerateJsonWebToken(BOL_UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.StreetAddress, user.Adress ?? "" ),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IGeneralFunctions.GetConfigValue("Jwt", "Key") ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _IGeneralFunctions.GetConfigValue("Jwt", "Issuer"),
                _IGeneralFunctions.GetConfigValue("Jwt", "Issuer"),
                claims,
                expires: DateTime.Now.AddDays(1), // Adjust expiration as needed
                signingCredentials: creds
            );

            user.AuthToken = new JwtSecurityTokenHandler().WriteToken(token);
            return user;
        }

        //public async Task<BOL_ApiResponse<bool>> ForgotPasswordSendEmail(string Email)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    ;
        //}
    }
}
