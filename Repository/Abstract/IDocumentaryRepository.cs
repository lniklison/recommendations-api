using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
	public interface IDocumentaryRepository
	{
		Task<IEnumerable<Documentary>> GetDocumentariesByTopics(List<string> topics);
	}
}
