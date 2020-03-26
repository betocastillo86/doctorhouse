using System;
using DoctorHouse.Api.Models.Requests;
using DoctorHouse.Business.Services.Communication;
using DoctorHouse.Data;

namespace DoctorHouse.Api.Validations
{
    public class RequestValidator
    {
        public RequestResponse Validate(SaveRequestModel model)
        {
            if(model.UserOwnerId == model.UserRequesterId)
                return new RequestResponse { Success = false, ErrorMessage = "The requester user and the owner user can not be the same." };
            
            if(model.StartDate > model.EndDate)
                return new RequestResponse { Success = false, ErrorMessage = "Start date must be greater than end date." };

            if(!Enum.IsDefined(typeof(StatusType), model.StatusId))
                return new RequestResponse { Success = false, ErrorMessage = "Invalid status." };

            if(model.UserOwnerId == model.UserRequesterId)
                return new RequestResponse { Success = false, ErrorMessage = "The requester user and the owner user can not be the same." };

            return new RequestResponse { Success = true };
        }

    }
}