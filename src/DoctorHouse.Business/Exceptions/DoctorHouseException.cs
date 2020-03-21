using Beto.Core.Exceptions;

namespace DoctorHouse.Business.Exceptions
{
    public class DoctorHouseException : CoreException<DoctorHouseExceptionCode>
    {
        public DoctorHouseException(string error) : base(error)
        {
        }

        public DoctorHouseException(DoctorHouseExceptionCode code) : base(DoctorHouseException.GetErrorMessage(code))
        {
            this.Code = code;
        }

        public DoctorHouseException(DoctorHouseExceptionCode code, string error) : base(error)
        {
            this.Code = code;
        }

        public DoctorHouseException(string target, DoctorHouseExceptionCode code) : base(DoctorHouseException.GetErrorMessage(code))
        {
            this.Target = target;
            this.Code = code;
        }

        public static string GetErrorMessage(DoctorHouseExceptionCode code)
        {
            return code.ToString();
        }
    }
}