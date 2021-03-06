﻿using GigHub2.Core;
using GigHub2.Core.Repositories;
using GigHub2.Persistence.Repositories;

namespace GigHub2.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IUserNotificationRepository UserNotifications { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Attendances = new AttendanceRepository(context);
            Followings = new FollowingRepository(context);
            Genres = new GenreRepository(context);
            UserNotifications = new UserNotificationRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}