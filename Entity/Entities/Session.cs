using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	[Table("Session")]
	public class Session
	{
		[Key]
		public int Id { get; set; }

		public int RoomId { get; set; }
		public virtual Room Room { get; set; }

		public int MovieId { get; set; }
		public virtual Movie Movie { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public int SeatsSold { get; set; }
	}

}
