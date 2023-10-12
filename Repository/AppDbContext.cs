using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class AppDbContext : DbContext
	{
		public virtual DbSet<Movie> Movies { get; set; }
		public virtual DbSet<Room> Rooms { get; set; }
		public virtual DbSet<City> Cities { get; set; }
		public virtual DbSet<Genre> Genres { get; set; }
		public virtual DbSet<MovieGenre> MovieGenres { get; set; }
		public virtual DbSet<Session> Sessions { get; set; }
		public virtual DbSet<Cinema> Cinemas { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Movie>()
				.ToTable("Movie")
				.HasKey(h => h.Id);

			modelBuilder.Entity<Room>()
				.ToTable("Room")
				.HasKey(r => r.Id);

			modelBuilder.Entity<City>()
				.ToTable("City")
				.HasKey(c => c.Id);

			modelBuilder.Entity<Genre>()
				.ToTable("Genre")
				.HasKey(g => g.Id);

			modelBuilder.Entity<MovieGenre>()
				.ToTable("MovieGenre")
				.HasKey(mg => new { mg.MovieId, mg.GenreId });

			modelBuilder.Entity<Session>()
				.ToTable("Session")
				.HasKey(s => s.Id);

			modelBuilder.Entity<Cinema>()
				.ToTable("Cinema")
				.HasKey(g => g.Id);

			modelBuilder.Entity<MovieGenre>()
				.HasKey(mg => new { mg.MovieId, mg.GenreId });

			modelBuilder.Entity<MovieGenre>()
				.HasOne(mg => mg.Movie)
				.WithMany(m => m.MovieGenres)
				.HasForeignKey(mg => mg.MovieId);

			modelBuilder.Entity<MovieGenre>()
				.HasOne(mg => mg.Genre)
				.WithMany(g => g.MovieGenres)
				.HasForeignKey(mg => mg.GenreId);

			modelBuilder.Entity<Session>()
				.HasOne(s => s.Room)
				.WithMany(r => r.Sessions)
				.HasForeignKey(s => s.RoomId);

			modelBuilder.Entity<Room>()
				.HasOne(r => r.Cinema)
				.WithMany(c => c.Rooms)
				.HasForeignKey(r => r.CinemaId);

			modelBuilder.Entity<Cinema>()
				.HasOne(c => c.City)
				.WithMany(c => c.Cinemas)
				.HasForeignKey(c => c.CityId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
