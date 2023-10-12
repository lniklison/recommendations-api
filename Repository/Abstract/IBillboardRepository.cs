using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
	public interface IBillboardRepository
	{
		Task<Billboard> GetBillboardByDateRange(DateTime startDate, DateTime endDate);
		Task<IntelligentBillboard> GetIntelligentBillboardByDateRangeAndRoomSize(DateTime startDate, DateTime endDate, int bigRooms, int smallRooms);
	}
}
