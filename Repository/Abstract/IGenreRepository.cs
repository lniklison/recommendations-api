using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
	public interface IGenreRepository
	{
		Task<Genre> GetByIdAsync(int id);
		Task<List<Genre>> GetAllAsync();
		Task<List<Genre>> GetBlockbusterGenresAsync();
		Task<List<Genre>> GetMinorityGenresAsync();
	}
}
