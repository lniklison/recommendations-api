namespace Tests.Repository
{
	[TestFixture]
	public class RoomRepositoryTests
	{
		private AppDbContext _context;
		private RoomRepository _repository;

		[SetUp]
		public void SetUp()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase") 
				.Options;

			_context = new AppDbContext(options);

			_context.Rooms.AddRange(
				new Room { Id = 1, Name = "Room1", Size = 100, Seats = 80 },
				new Room { Id = 2, Name = "Room2", Size = 120, Seats = 90 }
			);

			_context.SaveChanges();


			_repository = new RoomRepository(_context);
		}

		[Test]
		public async Task Should_Return_Room_By_IdAsync()
		{
			var room = await _repository.GetByIdAsync(1);
			Assert.IsNotNull(room);
			Assert.AreEqual(1, room.Id);
			Assert.AreEqual("Room1", room.Name);
		}

		[Test]
		public async Task Should_Return_All_RoomsAsync()
		{
			var allRooms = await _repository.GetAllAsync();
			Assert.IsNotNull(allRooms);
			Assert.AreEqual(2, allRooms.Count());
		}

		[Test]
		public async Task Should_Return_Null_If_Room_Not_FoundAsync()
		{
			var room = await _repository.GetByIdAsync(99);
			Assert.IsNull(room);
		}

		[Test]
		public async Task Should_Return_Correct_Room_For_GetByIdAsync()
		{
			var room = await _repository.GetByIdAsync(1);
			Assert.IsNotNull(room);
			Assert.AreEqual(1, room.Id);
		}

		[Test]
		public async Task Should_Return_All_Rooms_For_GetAllAsync()
		{
			var rooms = await _repository.GetAllAsync();
			Assert.IsNotNull(rooms);
			Assert.AreEqual(2, rooms.Count());
		}


		[TearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}
	}
}
