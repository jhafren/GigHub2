using System.Collections.Generic;
using GigHub2.Core.Models;

namespace GigHub2.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(string userId, int gigId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}