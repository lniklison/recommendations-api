using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Repository
{
	public class SessionRepository : ISessionRepository
	{
		private readonly AppDbContext _context;

		public SessionRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Session> GetByIdAsync(int id)
		{
			return await _context.Sessions.FindAsync(id);
		}

		public async Task<List<Session>> GetAllAsync()
		{
			return await _context.Sessions.ToListAsync();
		}
	}
}
