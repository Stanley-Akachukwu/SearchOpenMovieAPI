using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SearchOpenMovies.Dto;
using SearchOpenMovies.Models;
using SearchOpenMovies.Models.Entities;
using SearchOpenMovies.Services.Contracts;
using System.Net;

namespace SearchOpenMovies.Controllers
{
    [ApiController]
    [Route("/api/MovieSearch")]
    public class MovieSearchController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MovieSearchController(IMovieRepository movieRepository )
        {
            _movieRepository = movieRepository;
        }

        
        [HttpPost("FetchMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> FetchMovieAsync(MovieRequest model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var movies = await _movieRepository.FetchMovieAsync(model);

            return Ok(movies.ToArray());
        }
        [HttpGet("FetchSavedMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> FetchSavedMoviesAsync()
        {
            var movies = await _movieRepository.GetSavedMoviesAsync();

            return Ok(movies.ToArray());
        }
        [HttpPost("SaveMovie")]
        public async Task<ActionResult<Response>> SaveMovieAsync(Movie model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _movieRepository.SaveMovieAsync(model);
            return Ok(response);
        }
        
    }
}