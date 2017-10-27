using GigHub2.Core.Models;
using GigHub2.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GigHub2.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}