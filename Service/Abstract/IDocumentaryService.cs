using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
	public interface IDocumentaryService
	{
		Task<IEnumerable<Documentary>> GetRecommendations(List<string> topics);
	}
}
