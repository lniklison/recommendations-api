using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace Web.Controllers
{
	[Route("api/movies")]
	public class MoviesController : RecommendationsController<Movie>
	{
		private IMovieService _movieService;
		public MoviesController(IMovieService movieService) : base(movieService)
		{
			_movieService = movieService;
		}

		[HttpGet("upcoming")]
		public async Task<IActionResult> GetUpcomingMovies([FromQuery] List<string> keywords, [FromQuery] List<string> genres, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
		{
			try
			{
				var movies = await _movieService.GetUpcomingMoviesByKeywordsAndGenres(keywords, genres, fromDate, toDate);
				return Ok(movies);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("upcomingMoviesForManagers")]
		public async Task<IActionResult> GetUpcomingManagerMovies([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] string ageRate, [FromQuery] string genre)
		{
			try
			{
				var movies = await _movieService.GetUpcomingMoviesByDateAndAgeRate(fromDate, toDate, ageRate, genre);
				return Ok(movies);
			} 
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}

}
