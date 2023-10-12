namespace Tests.Service
{
	[TestFixture]
	public class BillboardServiceTests
	{
		private BillboardService _billboardService;
		private Mock<IBillboardRepository> _mockBillboardRepository;
		private Mock<IMoviesRepository> _mockMovieRepository;
		private Mock<IGenreRepository> _mockGenreRepository;
		private Mock<IExternalMoviesRepository> _mockExternalMovieApiClient;
		private IMemoryCache _cache;

		[SetUp]
		public void SetUp()
		{
			_mockBillboardRepository = new Mock<IBillboardRepository>();
			_mockMovieRepository = new Mock<IMoviesRepository>();
			_mockGenreRepository = new Mock<IGenreRepository>();
			_mockExternalMovieApiClient = new Mock<IExternalMoviesRepository>();

			var cacheOptions = new MemoryCacheOptions();
			_cache = new MemoryCache(cacheOptions);

			_billboardService = new BillboardService(_mockBillboardRepository.Object, _mockMovieRepository.Object, _mockGenreRepository.Object, _mockExternalMovieApiClient.Object, _cache);
		}

		[Test]
		public async Task Should_Return_IntelligentBillboard_For_Given_CriteriaAsync()
		{
			// Arrange
			var startDate = DateTime.Now;
			var endDate = DateTime.Now.AddDays(7);
			var genres = new List<string> { "Action", "Comedy" };

			_mockGenreRepository.Setup(x => x.GetBlockbusterGenresAsync()).ReturnsAsync(new List<Genre>
			{
				new Genre { Id = 1, Name = "Action" },
				new Genre { Id = 2, Name = "Comedy" }
			});

			_mockGenreRepository.Setup(x => x.GetMinorityGenresAsync()).ReturnsAsync(new List<Genre>
			{
				new Genre { Id = 3, Name = "Drama" },
				new Genre { Id = 4, Name = "Biographic" }
			});
			

			_mockMovieRepository.Setup(x => x.GetSuccessfulMoviesInCity(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<bool>(), It.IsAny<List<string>>(), null))
				.ReturnsAsync(new List<Movie>
				{
					new Movie { Id = 1, OriginalTitle = "Action Movie", MovieGenres = new List<MovieGenre> { new MovieGenre { GenreId = 1, Genre = new Genre { Id = 1, Name = "Action" } } } },
					new Movie { Id = 2, OriginalTitle = "Comedy Movie", MovieGenres = new List<MovieGenre> { new MovieGenre { GenreId = 2, Genre = new Genre { Id = 2, Name = "Comedy" } } } }
				});

			// Act
			var result = await _billboardService.GetIntelligentBillboard(startDate, endDate, 1, 1);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(startDate, result.StartDate);
			Assert.AreEqual(endDate, result.EndDate);
			Assert.AreEqual(2, result.BigRoomMovies.Count + result.SmallRoomMovies.Count);
		}
	}
}
