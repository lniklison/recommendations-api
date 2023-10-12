using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace Web.Controllers
{
	[Route("api/billboards")]
	public class BillboardsController : ControllerBase
	{
		private readonly IBillboardService _billboardService;

		public BillboardsController(IBillboardService billboardService)
		{
			_billboardService = billboardService;
		}

		[HttpGet("suggested")]
		public async Task<IActionResult> GetSuggestedBillboard([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int screens)
		{
			try
			{
				var billboard = await _billboardService.GetBillboard(startDate, endDate, screens);
				return Ok(billboard);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("intelligent")]
		public async Task<IActionResult> GetIntelligentBillboard([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int amountOfBigRooms, [FromQuery] int amountOfSmallRooms, [FromQuery] bool useSimilarityBasedOnCitySuccess = false, [FromQuery] string city = null)
		{
			try
			{
				if(startDate == DateTime.MinValue)
				{
					startDate = DateTime.Now;
				}
				if(endDate == DateTime.MinValue)
				{
					endDate = DateTime.Now.AddDays(7);
				}

				var intelligentBillboard = await _billboardService.GetIntelligentBillboard(startDate, endDate, amountOfBigRooms, amountOfSmallRooms, useSimilarityBasedOnCitySuccess, city);
				return Ok(intelligentBillboard);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}

}
