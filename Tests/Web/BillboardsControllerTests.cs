namespace Tests.Web
{
	[TestFixture]
	public class BillboardsControllerTests
	{
		private Mock<IBillboardService> _mockBillboardService;
		private BillboardsController _controller;

		[SetUp]
		public void SetUp()
		{
			_mockBillboardService = new Mock<IBillboardService>();
			_controller = new BillboardsController(_mockBillboardService.Object);
		}

		[Test]
		public async Task GetSuggestedBillboard_ShouldReturnOkResult_WhenSuccessful()
		{
			// Arrange
			_mockBillboardService.Setup(x => x.GetBillboard(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
								 .ReturnsAsync(new Billboard());

			// Act
			var result = await _controller.GetSuggestedBillboard(DateTime.Now, DateTime.Now.AddDays(7), 5);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
		}

		[Test]
		public async Task GetSuggestedBillboard_ShouldReturnBadRequest_WhenExceptionOccurs()
		{
			// Arrange
			_mockBillboardService.Setup(x => x.GetBillboard(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
								 .ThrowsAsync(new Exception("Sample exception"));

			// Act
			var result = await _controller.GetSuggestedBillboard(DateTime.Now, DateTime.Now.AddDays(7), 5);

			// Assert
			Assert.IsInstanceOf<BadRequestObjectResult>(result);
		}

		[Test]
		public async Task GetIntelligentBillboard_ShouldReturnOkResult_WhenSuccessful()
		{
			// Arrange
			_mockBillboardService.Setup(x => x.GetBillboard(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
								 .Returns(Task.FromResult(new Billboard()));

			// Act
			var result = await _controller.GetIntelligentBillboard(DateTime.Now, DateTime.Now.AddDays(7), 2, 3, true, "SampleCity");

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
		}

		[Test]
		public async Task GetIntelligentBillboard_ShouldReturnBadRequest_WhenExceptionOccurs()
		{
			// Arrange
			_mockBillboardService.Setup(x => x.GetIntelligentBillboard(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
								 .ThrowsAsync(new Exception());

			// Act
			var result = await _controller.GetIntelligentBillboard(DateTime.Now, DateTime.Now.AddDays(7), 2, 3, true, "SampleCity");

			// Assert
			Assert.IsInstanceOf<BadRequestObjectResult>(result);
		}
	}
}
