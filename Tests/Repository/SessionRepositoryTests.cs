namespace Tests.Repository
{
	[TestFixture]
	public class SessionRepositoryTests
	{
		private AppDbContext _context;
		private SessionRepository _repository;

		[SetUp]
		public void SetUp()
		{

			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: "SessionTestDatabase")
				.Options;


			_context = new AppDbContext(options);
			_context.Database.EnsureDeleted();
			var mockSessions = new List<Session>
			{
				new Session { Id = 1, MovieId = 1 },
				new Session { Id = 2, MovieId = 2 },
				new Session { Id = 3, MovieId = 3 }
			};

			_context.Sessions.AddRange(mockSessions);
			_context.SaveChanges();

			_repository = new SessionRepository(_context);
		}

		[Test]
		public async Task GetByIdAsync_ShouldReturnCorrectSession()
		{
			var session = await _repository.GetByIdAsync(1);
			Assert.AreEqual(1, session.Id);
			Assert.AreEqual(1, session.MovieId);
		}

		[Test]
		public async Task GetAllAsync_ShouldReturnAllSessions()
		{
			var sessions = await _repository.GetAllAsync();
			Assert.AreEqual(3, sessions.Count);
		}
	}
}
