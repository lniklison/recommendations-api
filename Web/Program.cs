using Entity.Helpers;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Abstract;
using Repository.Repository;
using Service.Abstract;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);


// Database context
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IMovieGenreRepository, MovieGenreRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IExternalMoviesRepository, ExternalMovieRepository>();
builder.Services.AddScoped<IBillboardRepository, BillboardRepository>();

// Config
builder.Services.Configure<TMDbConfig>(builder.Configuration.GetSection("TMDbConfig"));
builder.Services.AddHttpClient<IExternalMoviesRepository, ExternalMovieRepository>();

// Services
builder.Services.AddScoped<IBillboardService, BillboardService>();

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
