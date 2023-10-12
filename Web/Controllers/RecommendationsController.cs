using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace Web.Controllers
{
	[ApiController] 
	public abstract class RecommendationsController<T> : ControllerBase where T : Recommendation
	{
		protected readonly IRecommendationService<T> _recommendationService;

		protected RecommendationsController(IRecommendationService<T> recommendationService)
		{
			_recommendationService = recommendationService;
		}

		[HttpGet("alltimeRecommendations")]
		public async Task<IActionResult> GetRecommendations([FromQuery] List<string> keywords, [FromQuery] List<string> genres)
		{
			try
			{
				var recommendations = await _recommendationService.GetRecommendations(keywords, genres);
				return Ok(recommendations);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
