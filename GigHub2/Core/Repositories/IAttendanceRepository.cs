using System.Collections.Generic;
using GigHub2.Core.Models;

namespace GigHub2.Core.Repositories
{
    public interface IAttendanceRepository
    {
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
        Attendance GetAttendance(string userId, int gigId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}