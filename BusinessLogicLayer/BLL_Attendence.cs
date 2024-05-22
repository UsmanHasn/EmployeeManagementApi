using BusinesObjectLayer.Dtos;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using BusinessLogicLayer.Helper;

namespace BusinessLogicLayer
{
    public  class BLL_Attendence: IBLL_Attendence
    {
        private readonly IDAL_Attendence _IDAl_Attendence;
        private readonly IGeneralFunctions _IGeneralFunctions;
        public BLL_Attendence(IDAL_Attendence iDAl_Attendence,IGeneralFunctions IgeneralFunction)
        {
            _IDAl_Attendence = iDAl_Attendence;
            _IGeneralFunctions = IgeneralFunction;
        }   

        public async Task<BOL_ApiResponse<int>> TimeIn()
        {
            var response = new BOL_ApiResponse<int>();
            try
            {
                response.Data =  await _IDAl_Attendence.TimeIn(_IGeneralFunctions.GetLoggedInUserId());
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "TimeIn Successfull";
                

            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BOL_ApiResponse<int>> TimeOut()
        {
            var response = new BOL_ApiResponse<int>();
            try
            {
                response.Data = await _IDAl_Attendence.TimeOut(_IGeneralFunctions.GetLoggedInUserId());
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "TimeOut Successfull";


            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }


        public async Task<BOL_ApiResponse<IEnumerable<Attendence>>> GetAttendencebyUserId()
        {
            var response = new BOL_ApiResponse<IEnumerable<Attendence>>();
            try
            {
                response.Data = await _IDAl_Attendence.GetAttendanceByUserId(_IGeneralFunctions.GetLoggedInUserId());
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Success";

            }
            catch (Exception ex)
            {

                response.StatusCode =HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
