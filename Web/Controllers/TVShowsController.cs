using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace Web.Controllers
{
	[Route("api/tvshows")]
	public class TVShowsController : RecommendationsController<TVShow>
	{
		private readonly ITVShowService _tvShowService;

		public TVShowsController(ITVShowService tvShowService) : base(tvShowService)
		{
			_tvShowService = tvShowService;
		}

		[HttpGet("alltime")]
		public async Task<IActionResult> GetAllTimeTVShows([FromQuery] List<string> keywords, [FromQuery] List<string> genres)
		{
			try
			{
				var tvshows = await _tvShowService.GetRecommendations(keywords, genres);
				return Ok(tvshows);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}

}
