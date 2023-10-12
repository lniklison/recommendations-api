using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	[Table("City")]
	public class City
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public int Population { get; set; }
		public virtual ICollection<Cinema> Cinemas { get; set; }
	}

}
