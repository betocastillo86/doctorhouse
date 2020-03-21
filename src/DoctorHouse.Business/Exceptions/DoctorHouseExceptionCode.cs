using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorHouse.Business.Exceptions
{
    public enum DoctorHouseExceptionCode
    {
        BadArgument = 1,

        InvalidForeignKey = 2,

        UserEmailAlreadyExists = 3
    }
}
