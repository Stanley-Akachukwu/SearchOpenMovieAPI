using SearchOpenMovies.Dto;
using SearchOpenMovies.Models.Entities;

namespace SearchOpenMovies.Services.Contracts
{
    public interface IMovieRepository
    {
        Task<Response> SaveMovieAsync(Movie movie);
        Task<RetrievedSavedMoviesResponse> GetSavedMoviesAsync();
    }
}
