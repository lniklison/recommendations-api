using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
	public interface IRoomRepository
	{
		Task<Room> GetByIdAsync(int id);
		Task<List<Room>> GetAllAsync();
	}
}
