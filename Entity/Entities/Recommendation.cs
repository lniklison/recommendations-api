using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	public abstract class Recommendation 
	{
		public int Id { get; set; }

		public string OriginalTitle { get; set; }
		[NotMapped]
		public string Overview { get; set; }
		public ICollection<MovieGenre> MovieGenres { get; set; }
		[NotMapped]
		public string OriginalLanguage { get; set; }
		public DateTime ReleaseDate { get; set; }
		[NotMapped]
		public string Website { get; set; }
		[NotMapped]
		public List<string> Keywords { get; set; } = new List<string>();
	}
}
