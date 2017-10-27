using System.Collections.Generic;
using GigHub2.Core.Models;

namespace GigHub2.Core.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}