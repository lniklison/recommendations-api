using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace Web.Controllers
{
	[Route("api/documentaries")]
	public class DocumentariesController : ControllerBase
	{
		private readonly IDocumentaryService _documentaryService;

		public DocumentariesController(IDocumentaryService documentaryService)
		{
			_documentaryService = documentaryService;
		}

		[HttpGet("alltime")]
		public async Task<IActionResult> GetAllTimeDocumentaries([FromQuery] List<string> topics)
		{
			try
			{
				var documentaries = await _documentaryService.GetRecommendations(topics);
				return Ok(documentaries);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}

}
