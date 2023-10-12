using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Repository
{
	public class MoviesRepository : IMoviesRepository
	{
		private readonly AppDbContext _context;

		public MoviesRepository(AppDbContext dbContext)
		{
			_context = dbContext;
		}

		public async Task<List<Movie>> GetUpcomingMoviesByKeywordsAndGenres(List<string> keywords, List<string> genres, DateTime fromDate, DateTime toDate)
		{
			return new List<Movie>();
		}
		public async Task<List<Movie>> GetUpcomingMoviesByDateAndAgeRate(DateTime fromDate, DateTime toDate, bool ageRated, List<string> genres)
		{
			return new List<Movie>();
		}
		public async Task<List<Movie>> GetSuccessfulMoviesInCity(DateTime fromDate, DateTime toDate, bool ageRated, List<string> genres, string cityName = null)
		{
			var moviesQuery = _context.Movies
				.Include(m => m.MovieGenres)
				.ThenInclude(mg => mg.Genre)
				.Include(m => m.Sessions)
				.ThenInclude(s => s.Room)
				.ThenInclude(r => r.Cinema)
				.ThenInclude(c => c.City)
				.Where(m => m.Adult == ageRated &&
							m.ReleaseDate >= fromDate &&
							m.ReleaseDate <= toDate);

			if (genres != null && genres.Count > 0)
			{
				moviesQuery = moviesQuery.Where(m => m.MovieGenres.Any(mg => genres.Contains(mg.Genre.Name)));
			}

			if (!string.IsNullOrEmpty(cityName))
			{
				moviesQuery = moviesQuery.Where(m => m.Sessions.Any(s => s.Room.Cinema.City.Name == cityName));
			}

			return await moviesQuery.ToListAsync();
		}


		public async Task<List<Movie>> GetRecommendations(List<string> keywords, List<string> genres)
		{
			return new List<Movie>();
		}

		public async Task<Movie> GetByIdAsync(int id)
		{
			return await _context.Movies.FindAsync(id);
		}

		public async Task<List<Movie>> GetAllAsync()
		{
			return await _context.Movies.ToListAsync();
		}
	}
}
