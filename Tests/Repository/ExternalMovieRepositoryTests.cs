namespace Tests.Repository
{
	[TestFixture]
	public class ExternalMovieRepositoryTests
	{
		private Mock<HttpClientHandler> _mockHttpMessageHandler;
		private TestableExternalMovieRepository _repository;
		private IOptions<TMDbConfig> _config;

		private Mock<IExternalMoviesRepository> _mockRepository;

		[SetUp]
		public void Setup()
		{
			_mockHttpMessageHandler = new Mock<HttpClientHandler>();

			var client = new HttpClient(_mockHttpMessageHandler.Object);
			var tmdbConfig = new TMDbConfig
			{
				DiscoverMovieUrl = "http://testurl.com",
				ApiToken = "testToken"
			};
			_config = Options.Create(tmdbConfig);

			_mockRepository = new Mock<IExternalMoviesRepository>();
			_mockRepository.Setup(r => r.CastToMovie(It.IsAny<TMDbMovie>()))
				.ReturnsAsync(new Movie { OriginalTitle = "MockMovie" });

			_repository = new TestableExternalMovieRepository(client, _config, _mockRepository.Object);
		}

		[Test]
		public async Task Should_Return_Similar_Movies_For_Given_GenresAsync()
		{
			// Arrange
			var genres = new List<string> { "Action", "Drama" };

			var jsonResponse = @"{
				'results': [
					{ 'id': 1, 'name': 'MockMovie1' },
					{ 'id': 2, 'name': 'MockMovie2' }
				]
			}";

			var mockResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
			};

			_mockHttpMessageHandler
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(mockResponseMessage);

			// Act
			var movies = await _repository.GetSimilarMovies(genres);

			// Assert
			Assert.NotNull(movies);
			Assert.AreEqual(2, movies.Count);
		}
	}

	public class TestableExternalMovieRepository : ExternalMovieRepository
	{
		private readonly IExternalMoviesRepository _mockRepository;

		public TestableExternalMovieRepository(HttpClient httpClient, IOptions<TMDbConfig> config, IExternalMoviesRepository mockRepository)
			: base(httpClient, config)
		{
			_mockRepository = mockRepository;
		}

		public override Task<Movie> CastToMovie(TMDbMovie source)
		{
			return _mockRepository.CastToMovie(source);
		}
	}
}
