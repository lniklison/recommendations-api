using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	[Table("Cinema")]
	public class Cinema
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime OpenSince { get; set; }
		[Column("CityId")]
		public int CityId { get; set; }
		public virtual City City { get; set; }
		public virtual ICollection<Room> Rooms { get; set; }
	}
}
