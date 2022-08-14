using SearchOpenMovies.Dto;
using SearchOpenMovies.Models;
using SearchOpenMovies.Models.Entities;

namespace SearchOpenMovies.Services.Contracts
{
    public interface IMovieRepository
    {
        Task<Response> SaveMovieAsync(Movie movie);
        Task<List<Movie>> GetSavedMoviesAsync();
        Task<List<Movie>> FetchMovieAsync(MovieRequest model);
    }
}
