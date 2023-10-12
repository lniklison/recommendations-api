namespace Tests.Repository
{
	[TestFixture]
	public class BillboardRepositoryTests
	{
		private BillboardRepository _repository;

		[SetUp]
		public void SetUp()
		{
			_repository = new BillboardRepository();
		}

		[Test]
		public async Task Should_Return_New_Billboard_Instance_For_Given_Date_RangeAsync()
		{
			var result = await _repository.GetBillboardByDateRange(DateTime.Now, DateTime.Now.AddDays(1));

			Assert.IsNotNull(result);
		}

		[Test]
		public async Task Should_Return_New_IntelligentBillboard_Instance_For_Given_Date_Range_And_Room_SizeAsync()
		{
			var result = await _repository.GetIntelligentBillboardByDateRangeAndRoomSize(DateTime.Now, DateTime.Now.AddDays(1), 1, 1);

			Assert.IsNotNull(result);
		}

	}
}
