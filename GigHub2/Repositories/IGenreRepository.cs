using System.Collections.Generic;
using GigHub2.Models;

namespace GigHub2.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}