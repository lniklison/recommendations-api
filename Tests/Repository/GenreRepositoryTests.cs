namespace Tests.Repository
{
	[TestFixture]
	public class GenreRepositoryTests
	{
		private AppDbContext _context;
		private GenreRepository _genreRepository;

		[SetUp]
		public void SetUp()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: "TestGenreDatabase") 
				.Options;

			_context = new AppDbContext(options);

			var genres = new List<Genre>
			{
				new Genre
				{
					Id = 1,
					Name = "Action",
					MovieGenres = new List<MovieGenre>
					{
						new MovieGenre
						{
							Movie = new Movie
							{
								OriginalTitle = "Movie1",
								Sessions = new List<Session>
								{
									new Session
									{
										SeatsSold = 80,
										Room = new Room
										{
											Name = "Room1",
											Seats = 100
										}
									},
									new Session
									{
										SeatsSold = 90,
										Room = new Room
										{
											Name = "Room1",
											Seats = 100
										}
									}
								}
							}
						}
					}
				},
				new Genre
				{
					Id = 2,
					Name = "Romance",
					MovieGenres = new List<MovieGenre>
					{
						new MovieGenre
						{
							Movie = new Movie
							{
								OriginalTitle = "Movie2",
								Sessions = new List<Session>
								{
									new Session
									{
										SeatsSold = 60,
										Room = new Room
										{

											Name = "Room2",
											Seats = 100
										}
									},
									new Session
									{
										SeatsSold = 65,
										Room = new Room
										{
											Name = "Room2",
											Seats = 100
										}
									}
								}
							}
						}
					}
				},
				new Genre
				{
					Id = 3,
					Name = "Comedy",
					MovieGenres = new List<MovieGenre>
					{
						new MovieGenre
						{
							Movie = new Movie
							{
								OriginalTitle = "Movie3",
								Sessions = new List<Session>
								{
									new Session
									{
										SeatsSold = 75,
										Room = new Room
										{
											Name = "Room3",
											Seats = 100
										}
									}
								}
							}
						}
					}
				}
			};

			_context.Genres.AddRange(genres);
			_context.SaveChanges();

			_genreRepository = new GenreRepository(_context);
		}


		[Test]
		public async Task Should_Get_Genre_By_IdAsync()
		{
			// Arrange
			var expectedId = 1;

			// Act
			var result = await _genreRepository.GetByIdAsync(expectedId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedId, result.Id);
		}

		[Test]
		public async Task Should_Get_All_GenresAsync()
		{
			// Act
			var genres = await _genreRepository.GetAllAsync();

			// Assert
			Assert.IsNotNull(genres);
			Assert.AreEqual(3, genres.Count);
		}

		[TearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}
	}
}
