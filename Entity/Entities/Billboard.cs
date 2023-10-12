using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	public class Billboard
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public List<Movie> Movies { get; set; } = new List<Movie>();
	}
}
