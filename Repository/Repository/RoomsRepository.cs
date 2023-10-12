using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Repository
{
	public class RoomRepository : IRoomRepository
	{
		private readonly AppDbContext _context;

		public RoomRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Room> GetByIdAsync(int id)
		{
			return await _context.Rooms.FindAsync(id);
		}

		public async Task<List<Room>> GetAllAsync()
		{
			return await _context.Rooms.ToListAsync();
		}
	}
}
