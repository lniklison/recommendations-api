using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Repository
{
	public class CityRepository : ICityRepository
	{
		private readonly AppDbContext _context;

		public CityRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<City> GetByIdAsync(int id)
		{
			return await _context.Cities.FindAsync(id);
		}

		public async Task<List<City>> GetAllAsync()
		{
			return await _context.Cities.ToListAsync();
		}
	}
}
