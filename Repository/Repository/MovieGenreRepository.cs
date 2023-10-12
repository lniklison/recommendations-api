using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Repository
{
	public class MovieGenreRepository : IMovieGenreRepository
	{
		private readonly AppDbContext _context;

		public MovieGenreRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<MovieGenre> GetByIdAsync(int movieId, int genreId)
		{
			return await _context.MovieGenres.FindAsync(movieId, genreId);
		}

		public async Task<List<MovieGenre>> GetAllAsync()
		{
			return await _context.MovieGenres.ToListAsync();
		}
	}
}
