using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
	public interface IRecommendationsRepository<T> where T : Recommendation
	{
		Task<List<T>> GetRecommendations(List<string> keywords, List<string> genres);
	}
}
