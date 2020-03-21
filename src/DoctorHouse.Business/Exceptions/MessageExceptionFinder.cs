using System;
using Beto.Core.Exceptions;

namespace DoctorHouse.Business.Exceptions
{
    public class MessageExceptionFinder : IMessageExceptionFinder
    {
        public string GetErrorMessage<T>(T exceptionCode)
        {
            if (exceptionCode is DoctorHouseExceptionCode)
            {
                return DoctorHouseException.GetErrorMessage((DoctorHouseExceptionCode)Enum.Parse(typeof(DoctorHouseExceptionCode), exceptionCode.ToString()));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}