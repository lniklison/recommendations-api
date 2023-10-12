namespace Tests.Repository
{
	[TestFixture]
	public class MoviesRepositoryTests
	{
		private MoviesRepository _repository;
		private AppDbContext _context;

		[SetUp]
		public void SetUp()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabaseMovies")
				.Options;

			_context = new AppDbContext(options);


			var mockMovies = new List<Movie>
			{
				new Movie
				{
					Id = 1,
					Adult = false,
					ReleaseDate = DateTime.Now.AddDays(5),
					OriginalTitle = "Sample Movie 1",
					Overview = "This is a sample overview for the first movie.",
					OriginalLanguage = "en",
					Website = "https://samplemovie1.com",
					Keywords = new List<string> { "action", "hero", "climax" },
					MovieGenres = new List<MovieGenre>
					{
						new MovieGenre { Genre = new Genre { Name = "Action" } },
						new MovieGenre { Genre = new Genre { Name = "Drama" } }
					},
					Sessions = new List<Session>{},
					Score = 7.5,
					AlreadyRecommended = false
				},
				new Movie
				{
					Id = 2,
					Adult = true,
					ReleaseDate = DateTime.Now.AddDays(10),
					OriginalTitle = "Sample Movie 2",
					Overview = "This is a sample overview for the second movie.",
					OriginalLanguage = "en",
					Website = "https://samplemovie2.com",
					Keywords = new List<string> { "thriller", "suspense", "mystery" },
					MovieGenres = new List<MovieGenre>
					{
						new MovieGenre { Genre = new Genre { Name = "Action" } },
						new MovieGenre { Genre = new Genre { Name = "Thriller" } }
					},
					Sessions = new List<Session>{},
					Score = 8.2,
					AlreadyRecommended = true
				}
			};

			_context.Movies.AddRange(mockMovies);
			_context.SaveChanges();

			_repository = new MoviesRepository(_context);
		}

		[Test]
		public async Task Should_Return_Movie_By_IdAsync()
		{
			var result = await _repository.GetByIdAsync(1);
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Id);
		}

		[Test]
		public async Task Should_Return_All_MoviesAsync()
		{
			var allMovies = await _repository.GetAllAsync();
			Assert.IsNotNull(allMovies);
			Assert.AreEqual(2, allMovies.Count());
		}

		[Test]
		public async Task GetSuccessfulMoviesInCity_WithAllGenres_Should_Return_All_MoviesAsync()
		{
			var movies = await _repository.GetSuccessfulMoviesInCity(DateTime.Now, DateTime.Now.AddDays(20), false, new List<string> { "Action", "Drama" });
			Assert.IsNotNull(movies);
			Assert.AreEqual(1, movies.Count());
		}

		[Test]
		public async Task GetSuccessfulMoviesInCity_WithoutGenres_Should_Return_All_MoviesAsync()
		{
			var movies = await _repository.GetSuccessfulMoviesInCity(DateTime.Now, DateTime.Now.AddDays(20), false, new List<string>() );
			Assert.IsNotNull(movies);
			Assert.AreEqual(1, movies.Count());
		}

		[TearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}
	}
}
