namespace Tests.Repository
{
	[TestFixture]
	public class MovieGenreRepositoryTests
	{
		private AppDbContext _context;
		private MovieGenreRepository _repository;

		[SetUp]
		public void SetUp()
		{

			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: "MovieGenreTestDatabase")
				.Options;


			_context = new AppDbContext(options);
			_context.Database.EnsureDeleted();
			var mockMovieGenres = new List<MovieGenre>
			{
				new MovieGenre { MovieId = 1, GenreId = 1 },
				new MovieGenre { MovieId = 1, GenreId = 2 },
				new MovieGenre { MovieId = 2, GenreId = 1 }
			};

			_context.MovieGenres.AddRange(mockMovieGenres);
			_context.SaveChanges();

			_repository = new MovieGenreRepository(_context);
		}

		[Test]
		public async Task Should_Return_MovieGenre_By_IdAsync()
		{
			var result = await _repository.GetByIdAsync(1, 1);
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.MovieId);
			Assert.AreEqual(1, result.GenreId);
		}

		[Test]
		public async Task Should_Return_All_MovieGenresAsync()
		{
			var allMovieGenres = await _repository.GetAllAsync();
			Assert.IsNotNull(allMovieGenres);
			Assert.AreEqual(3, allMovieGenres.Count());
		}
	}
}
