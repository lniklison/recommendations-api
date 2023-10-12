using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	[Table("Movie")]
	public class Movie : Recommendation
	{
		public bool Adult { get; set; }

		public ICollection<Session> Sessions { get; set; }
		[NotMapped]
		public double? Score { get; set; }
		[NotMapped]
		public bool AlreadyRecommended { get; set; } = false;
	}
}
