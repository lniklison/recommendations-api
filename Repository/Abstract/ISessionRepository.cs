using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
	public interface ISessionRepository
	{
		Task<Session> GetByIdAsync(int id);
		Task<List<Session>> GetAllAsync();
	}

}
