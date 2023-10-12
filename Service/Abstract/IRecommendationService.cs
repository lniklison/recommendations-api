using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
	public interface IRecommendationService<T> where T : Recommendation
	{
		Task<IEnumerable<T>> GetRecommendations(List<string> keywords, List<string> genres);
	}
}