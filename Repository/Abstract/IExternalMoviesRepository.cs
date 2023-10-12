using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
	public interface IExternalMoviesRepository
	{
		public Task<List<Movie>> GetSimilarMovies(List<string> genres);
		public Task<Movie> CastToMovie(TMDbMovie source);
	}
}
