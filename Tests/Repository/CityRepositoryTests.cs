namespace Tests.Repository
{
	[TestFixture]
	public class CityRepositoryTests
	{
		private CityRepository _cityRepository;
		private AppDbContext _context;

		[SetUp]
		public void Setup()
		{

			var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "TestDatabase")
			.Options;

			_context = new AppDbContext(options);

			var cities = new List<City>
		{
			new City { Id = 1, Name = "City1", Population = 100000 },
			new City { Id = 2, Name = "City2", Population = 200000 },
			new City { Id = 3, Name = "City3", Population = 300000 }
			};

			_context.Cities.AddRange(cities);
			_context.SaveChanges();

			_cityRepository = new CityRepository(_context);
		}

		[Test]
		public async Task Should_Return_City_For_Valid_IdAsync()
		{
			var city = await _cityRepository.GetByIdAsync(1);
			Assert.IsNotNull(city);
			Assert.AreEqual("City1", city.Name);
		}

		[Test]
		public async Task Should_Return_Null_For_Invalid_IdAsync()
		{
			var city = await _cityRepository.GetByIdAsync(4);
			Assert.IsNull(city);
		}

		[Test]
		public async Task Should_Return_All_Cities_From_RepositoryAsync()
		{
			var cities = await _cityRepository.GetAllAsync();
			Assert.AreEqual(3, cities.Count);
			Assert.AreEqual("City1", cities[0].Name);
			Assert.AreEqual("City2", cities[1].Name);
			Assert.AreEqual("City3", cities[2].Name);
		}

		[TearDown]
		public void TearDown()
		{

			_context.Database.EnsureDeleted();
			_context.Dispose();
		}
	}
}
