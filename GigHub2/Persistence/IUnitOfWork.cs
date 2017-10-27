﻿using GigHub2.Repositories;

namespace GigHub2.Persistence
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        void Complete();
    }
}