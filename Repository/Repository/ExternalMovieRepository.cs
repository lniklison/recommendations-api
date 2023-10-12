using Entity.Dto;
using Entity.Entities;
using Entity.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Repository.Abstract;

namespace Repository.Repository
{
	public class ExternalMovieRepository : IExternalMoviesRepository
	{
		private readonly HttpClient _httpClient;
		private readonly string _discoverMovieUrl;
		private readonly string _apiToken;

		public ExternalMovieRepository(HttpClient httpClient, IOptions<TMDbConfig> tmdbConfig)
		{
			_httpClient = httpClient;
			_discoverMovieUrl = tmdbConfig.Value.DiscoverMovieUrl;
			_apiToken = tmdbConfig.Value.ApiToken;
		}

		public async Task<List<Movie>> GetSimilarMovies(List<string> genres)
		{
			var genresString = FormatGenres(genres);
			var url = $"{_discoverMovieUrl}?api_key={_apiToken}&with_genres={genresString}";


			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(url),
				Headers =
				{
					{ "accept", "application/json" },
					{ "Authorization", $"Bearer {_apiToken}" },
				},
			};
			
			using (var response = await _httpClient.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				MovieResponse responseObj = JsonConvert.DeserializeObject<MovieResponse>(body);
				
				var movies = new List<Movie>();
				foreach (var movie in responseObj.Results)
				{
					movies.Add(await CastToMovie(movie));
				}
				return movies;
			}
		}

		private string FormatGenres(List<string> genres)
		{
			return string.Join("%2C", genres);
		}

		public virtual async Task<Movie> CastToMovie(TMDbMovie source)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			var genreMap = await GetMovieDbGenres();

			Movie movie = new Movie
			{
				Id = source.id,
				Adult = source.adult,
				OriginalTitle = source.original_title,
				Overview = source.overview,
				OriginalLanguage = source.original_language,
				ReleaseDate = DateTime.TryParse(source.release_date, out var releaseDate)
							  ? releaseDate
							  : DateTime.MinValue,
				MovieGenres = source.genre_ids
								  .Where(id => genreMap.ContainsKey(id))
								  .Select(id => new MovieGenre { GenreId = id, Genre = new Genre { Id = id, Name = genreMap[id] } })
								  .ToList(),
				Score = source.vote_average
			};

			return movie;
		}


		private async Task<Dictionary<int, string>> GetMovieDbGenres()
		{
			var url = "https://api.themoviedb.org/3/genre/movie/list?language=en";

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(url),
				Headers =
				{
					{ "accept", "application/json" },
					{ "Authorization", $"Bearer {_apiToken}" },
				},
			};

			using (var response = await _httpClient.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				var responseObj = JsonConvert.DeserializeObject<GenreResponse>(body);

				return responseObj.Genres.ToDictionary(g => g.Id, g => g.Name);
			}
		}


	}

}
