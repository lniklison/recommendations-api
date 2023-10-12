using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	public class MovieResponse
	{
		public int Page { get; set; }
		public List<TMDbMovie> Results { get; set; }
	}
}
