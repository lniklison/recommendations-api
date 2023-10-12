using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
	public interface ICityRepository
	{
		Task<City> GetByIdAsync(int id);
		Task<List<City>> GetAllAsync();
	}
}
