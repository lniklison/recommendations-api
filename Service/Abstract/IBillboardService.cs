using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
	public interface IBillboardService
	{
		Task<Billboard> GetBillboard(DateTime startDate, DateTime endDate, int screens);
		Task<IntelligentBillboard> GetIntelligentBillboard(DateTime startDate, DateTime endDate, int bigRooms, int smallRooms, bool useSimilarityBasedOnCitySuccess, string city);
	}
}
