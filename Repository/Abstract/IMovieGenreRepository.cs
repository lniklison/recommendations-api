using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
	public interface IMovieGenreRepository
	{
		Task<MovieGenre> GetByIdAsync(int movieId, int genreId);
		Task<List<MovieGenre>> GetAllAsync();
	}
}
