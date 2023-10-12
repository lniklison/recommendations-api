using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities
{
	[Table("Room")]
	public class Room
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public int Size { get; set; }

		public int Seats { get; set; }
		[Column("CinemaId")]
		public int CinemaId { get; set; }

		public virtual Cinema Cinema { get; set; }
		public virtual ICollection<Session> Sessions { get; set; }
	}

}
