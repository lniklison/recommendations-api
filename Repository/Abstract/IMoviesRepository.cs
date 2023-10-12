using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
	public interface IMoviesRepository : IRecommendationsRepository<Movie>
	{
		Task<List<Movie>> GetUpcomingMoviesByKeywordsAndGenres(List<string> keywords, List<string> genres, DateTime fromDate, DateTime toDate);
		Task<List<Movie>> GetUpcomingMoviesByDateAndAgeRate(DateTime fromDate, DateTime toDate, bool ageRated, List<string> genres);
		Task<List<Movie>> GetSuccessfulMoviesInCity(DateTime fromDate, DateTime toDate, bool ageRated, List<string> genres, string cityName);
		Task<Movie> GetByIdAsync(int id);
		Task<List<Movie>> GetAllAsync();

	}
}
