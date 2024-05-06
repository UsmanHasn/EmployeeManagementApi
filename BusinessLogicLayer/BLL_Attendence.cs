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

namespace BusinessLogicLayer
{
    public  class BLL_Attendence: IBLL_Attendence
    {
        private readonly IDAL_Attendence _IDAl_Attendence;

        public BLL_Attendence(IDAL_Attendence iDAl_Attendence)
        {
            _IDAl_Attendence = iDAl_Attendence;
        }   

        public async Task<BOL_ApiResponse<int>> TimeIn(int Id)
        {
            var response = new BOL_ApiResponse<int>();
            try
            {
                response.Data =  await _IDAl_Attendence.TimeIn(Id);
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

        public async Task<BOL_ApiResponse<int>> TimeOut(int Id)
        {
            var response = new BOL_ApiResponse<int>();
            try
            {
                response.Data = await _IDAl_Attendence.TimeOut(Id);
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


    }
}
