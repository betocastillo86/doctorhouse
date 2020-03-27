using System;
using DoctorHouse.Api.Models.Requests;
using DoctorHouse.Data;

namespace DoctorHouse.Api.Validations
{
    public class RequestValidator
    {
        public string Validate(SaveRequestModel model)
        {
            if(model.UserOwnerId == model.UserRequesterId)
                return "The requester user and the owner user can not be the same." ;
            
            if(model.StartDate > model.EndDate)
                return "Start date must be greater than end date.";

            if(!Enum.IsDefined(typeof(StatusType), model.StatusId))
                return "Invalid status.";

            if(model.UserOwnerId == model.UserRequesterId)
                return "The requester user and the owner user can not be the same.";

            return string.Empty;
        }

    }
}