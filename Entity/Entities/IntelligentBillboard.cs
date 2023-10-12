using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	public class IntelligentBillboard
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public List<MovieDTO> BigRoomMovies { get; set; } = new List<MovieDTO>();
		public List<MovieDTO> SmallRoomMovies { get; set; } = new List<MovieDTO>();
	}
}
