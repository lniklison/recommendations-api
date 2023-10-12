using Entity.Entities;
using Repository.Abstract;

namespace Repository.Repository
{
	public class BillboardRepository : IBillboardRepository
	{
		public async Task<Billboard> GetBillboardByDateRange(DateTime startDate, DateTime endDate)
		{

			return new Billboard();
		}

		public async Task<IntelligentBillboard> GetIntelligentBillboardByDateRangeAndRoomSize(DateTime startDate, DateTime endDate, int bigRooms, int smallRooms)
		{
			return new IntelligentBillboard();
		}
	}
}
