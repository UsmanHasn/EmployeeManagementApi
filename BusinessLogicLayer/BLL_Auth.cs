using BusinesObjectLayer.Dtos;
using BusinesObjectLayer.Enums;
using BusinessLogicLayer.Helper;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
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
                    userdto.PhoneNo = user.PhoneNo;
                    userdto.ProfilePic = user.ProfilePictureUrl;
                    userdto.CreatedOn = user.CreatedOn;

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

        public async Task<BOL_ApiResponse<string>> UploadProfilePicture(HttpRequest request)
        {
            var response = new BOL_ApiResponse<string>();
            try
            {
                response.Data = await _IGeneralFunctions.UploadMediaFile(request, _IGeneralFunctions.GetProfileImageDirectory());
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Image uploaded Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BOL_ApiResponse<User>> Updateprofile(BOL_UpdateUser model)
        {
            var response = new BOL_ApiResponse<User>();
            try
            {
                model.Id = _IGeneralFunctions.GetLoggedInUserId();
                model.UpdatedBy = model.Id;
                var user = await _IDAL_Auth.UpdateProfile(model);
                if (user == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "User Not Found";

                }
                else
                {
                    response.Data = user;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = "Profile Updated Successfully";

                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BOL_ApiResponse<int>> ResetUserPassword(BOL_ResetUserPassword model)
        {
            var response = new BOL_ApiResponse<int>();

            try
            {
                if (model.NewPassword == model.ConfirmPassword)
                {
                    model.Id = _IGeneralFunctions.GetLoggedInUserId();

                    var user = await _IDAL_Auth.ResetUserPassword(model);
                    if (user > 0)
                    {
                        response.Data = user;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = "Password Updated Successfully";
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.Message = "Current Password does not matched";
                    }
                }
                else
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "New Password and Current Password Does Not matched";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;    
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
