using Entity.Dto;
using Entity.Entities;
using Microsoft.Extensions.Caching.Memory;
using Repository.Abstract;
using Service.Abstract;

namespace Service.Services
{
	public class BillboardService : IBillboardService
	{
		private IBillboardRepository _billboardRepository;
		private IMoviesRepository _movieRepository;
		private IGenreRepository _genreRepository;
		private IExternalMoviesRepository _externalMovieApiClient;
		private readonly IMemoryCache _cache;
		private List<Movie> _alreadyRecommendedMovies;
		private const string AlreadyRecommendedMoviesCacheKey = "AlreadyRecommendedMovies";

		public BillboardService(IBillboardRepository billboardRepository, 
			IMoviesRepository movieRepository, 
			IGenreRepository genreRepository, 
			IExternalMoviesRepository externalMovieApiClient, 
			IMemoryCache cache)
		{
			_billboardRepository = billboardRepository;
			_movieRepository = movieRepository;
			_genreRepository = genreRepository;
			_externalMovieApiClient = externalMovieApiClient;
			_cache = cache;
			_alreadyRecommendedMovies = _cache.GetOrCreate(AlreadyRecommendedMoviesCacheKey, entry => new List<Movie>());
		}

		public async Task<IntelligentBillboard> GetIntelligentBillboard(DateTime startDate, DateTime endDate, int bigRooms, int smallRooms, bool useSimilarityBasedOnCitySuccess = false, string city = null)
		{
			var moviesForSmallRooms = await GetMoviesForRoom(startDate, endDate, _genreRepository.GetMinorityGenresAsync(), smallRooms, useSimilarityBasedOnCitySuccess, city);
			var moviesForBigRooms = await GetMoviesForRoom(startDate, endDate, _genreRepository.GetBlockbusterGenresAsync(), bigRooms, useSimilarityBasedOnCitySuccess, city);

			MarkMoviesAsRecommended(moviesForSmallRooms);
			MarkMoviesAsRecommended(moviesForBigRooms);

			var moviesForSmallRoomsDTOs = moviesForSmallRooms.Select(MovieDTO.FromMovie).ToList();
			var moviesForBigRoomsDTOs = moviesForBigRooms.Select(MovieDTO.FromMovie).ToList();

			return new IntelligentBillboard
			{
				StartDate = startDate,
				EndDate = endDate,
				BigRoomMovies = moviesForBigRoomsDTOs,
				SmallRoomMovies = moviesForSmallRoomsDTOs
			};
		}

		private async Task<List<Movie>> GetMoviesForRoom(DateTime startDate, DateTime endDate, Task<List<Genre>> genresTask, int roomSize, bool useSimilarity, string city)
		{
			var genres = await genresTask;
			var successfulMovies = await GetRecommendedMovies(genres.Select(g => g.Name).ToList(), false, startDate, endDate, city);

			var moviesForRoom = useSimilarity
				? await AddSimilarMoviesToRoom(successfulMovies)
				: successfulMovies;

			return FilterAndOrderMovies(moviesForRoom, roomSize);
		}

		private List<Movie> FilterAndOrderMovies(IEnumerable<Movie> movies, int roomSize)
		{
			return movies
				.Where(m => !_alreadyRecommendedMovies.Any(arm => arm.Id == m.Id))
				.OrderByDescending(m => m.Score)
				.Take(roomSize)
				.ToList();
		}

		private async Task<List<Movie>> AddSimilarMoviesToRoom(IEnumerable<Movie> successfulMovies)
		{
			var similarMoviesList = new List<Movie>();

			foreach (var movie in successfulMovies)
			{
				similarMoviesList.AddRange(await GetSimilarMoviesFor(movie));
			}

			return similarMoviesList;
		}

		private async Task<List<Movie>> GetSimilarMoviesFor(Movie movie)
		{
			return await _externalMovieApiClient.GetSimilarMovies(movie.MovieGenres.Select(x => x.Genre.Name).ToList());
		}

		private void MarkMoviesAsRecommended(List<Movie> movies)
		{
			foreach (var movie in movies)
			{
				movie.AlreadyRecommended = true;
				if (!_alreadyRecommendedMovies.Contains(movie))
				{
					_alreadyRecommendedMovies.Add(movie);
				}
			}
			_cache.Set(AlreadyRecommendedMoviesCacheKey, _alreadyRecommendedMovies);
		}


		public async Task<Billboard> GetBillboard(DateTime startDate, DateTime endDate, int screens)
		{
			return new Billboard();
		}

		private async Task<List<Movie>> GetRecommendedMovies(List<string> genres, bool adultRated, DateTime dateStart, DateTime dateEnd, string city)
		{
			var recommendedMovies = await _movieRepository.GetSuccessfulMoviesInCity(dateStart, dateEnd, adultRated, genres, city);

			foreach (var movie in recommendedMovies)
			{
				CalculateAndSetAverageViewershipScore(movie);
			}

			return recommendedMovies;
		}

		private void CalculateAndSetAverageViewershipScore(Movie movie)
		{
			if (movie == null)
			{
				throw new ArgumentNullException(nameof(movie));
			}

			if (movie.Sessions == null || !movie.Sessions.Any())
			{
				movie.Score = 0;
				return;
			}

			double totalPercentage = movie.Sessions.Sum(session => (double)session.SeatsSold / session.Room.Seats * 10);
			int totalSessions = movie.Sessions.Count;

			movie.Score = totalPercentage / totalSessions;
		}
	}
}
