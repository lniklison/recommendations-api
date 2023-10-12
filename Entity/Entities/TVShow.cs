using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	public class TVShow : Recommendation
	{
		public int NumberOfSeasons { get; set; }
		public int NumberOfEpisodes { get; set; }
		public bool IsConcluded { get; set; }
	}
}
