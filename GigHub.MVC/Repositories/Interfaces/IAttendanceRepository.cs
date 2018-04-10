using GigHub.MVC.Models;
using System.Collections.Generic;

namespace GigHub.MVC.Repositories.Interfaces
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int gigId, string userId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}
