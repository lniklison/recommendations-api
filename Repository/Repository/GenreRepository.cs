using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Repository
{
	public class GenreRepository : IGenreRepository
	{
		private readonly AppDbContext _context;

		public GenreRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Genre> GetByIdAsync(int id)
		{
			return await _context.Genres.FindAsync(id);
		}

		public async Task<List<Genre>> GetAllAsync()
		{
			return await _context.Genres.ToListAsync();
		}

		public async Task<List<Genre>> GetBlockbusterGenresAsync()
		{
			return await _context.Genres
				.Where(g => g.MovieGenres
					.SelectMany(mg => mg.Movie.Sessions)
					.Average(s => 100.0 * s.SeatsSold / s.Room.Seats) >= 70)
				.ToListAsync();
		}

		public async Task<List<Genre>> GetMinorityGenresAsync()
		{
			return await _context.Genres
				.Where(g => g.MovieGenres
					.SelectMany(mg => mg.Movie.Sessions)
					.Average(s => 100.0 * s.SeatsSold / s.Room.Seats) < 70)
				.ToListAsync();
		}

	}
}
