using DoctorHouse.Data;

namespace DoctorHouse.Business.Security
{
    public interface IWorkContext
    {
        User CurrentUser { get; }

        int CurrentUserId { get; }

        bool IsAuthenticated { get; }
    }
}