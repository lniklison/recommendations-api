using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
	public interface IMovieService : IRecommendationService<Movie>
	{
		Task<IEnumerable<Movie>> GetUpcomingMoviesByKeywordsAndGenres(List<string> keywords, List<string> genres, DateTime fromDate, DateTime toDate);
		Task<IEnumerable<Movie>> GetUpcomingMoviesByDateAndAgeRate(DateTime fromDate, DateTime toDate, string ageRate, string genere);
	}
}
