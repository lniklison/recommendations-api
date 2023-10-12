using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
	public class MovieDTO
	{
		public int Id { get; set; }
		public string OriginalTitle { get; set; }
		public string Overview { get; set; }
		public List<string> Genres { get; set; } = new List<string>();
		public string Language { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string Website { get; set; }
		public List<string> Keywords { get; set; } = new List<string>();
		public bool Adult { get; set; }
		public double? Score { get; set; }
		public bool AlreadyRecommended { get; set; }

		public static MovieDTO FromMovie(Movie movie)
		{
			return new MovieDTO
			{
				Id = movie.Id,
				OriginalTitle = movie.OriginalTitle,
				Overview = movie.Overview,
				Genres = movie.MovieGenres?.Select(mg => mg.Genre?.Name).ToList() ?? new List<string>(),
				Language = movie.OriginalLanguage,
				ReleaseDate = movie.ReleaseDate,
				Website = movie.Website,
				Keywords = movie.Keywords,
				Adult = movie.Adult,
				Score = movie.Score,
				AlreadyRecommended = movie.AlreadyRecommended
			};
		}

	}
}