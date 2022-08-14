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
        private readonly HttpClient _httpClient;
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieSearchController> _logger;
       // private readonly IConfiguration _configuration;

        public MovieSearchController(ILogger<MovieSearchController> logger, HttpClient httpClient, IMovieRepository movieRepository )
        {
            _logger = logger;
            _httpClient = httpClient;
            _movieRepository = movieRepository;
            //_configuration = configuration;

        }

        
        [HttpPost("FetchMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> FetchMovieAsync(MovieRequest model)
        {
           
            var movies = new List<Movie>();
      
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string queryParams = "";
            if (model.SearchOption == "Single")
            {
                queryParams = "i=" + model.IMDbID + "&apikey=8938e916" + "&plot=" + model.FullPlot + "&type=" + model.MovieType + "&y=" + model.ReleaseYear;
            }
            if (model.SearchOption == "Multiple")
            {
                queryParams = "s=" + model.Title + "&apikey=8938e916" + "&page=" +"0-"+ model.PageSize + "&type=" + model.MovieType + "&y=" + model.ReleaseYear;
            }

            //http://www.omdbapi.com/?i=tt1285016&apikey=8938e916&y=08-08-2012&type=movies&plot=full
            HttpResponseMessage response = await _httpClient.GetAsync("http://www.omdbapi.com/?"+ queryParams);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var movie = await response.Content.ReadFromJsonAsync<Movie>();
                movies.Add(movie);  
            }
            
            return Ok(movies.ToArray());
        }
        [HttpGet("FetchSavedMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> FetchSavedMoviesAsync()
        {
            var repoResponse = await _movieRepository.GetSavedMoviesAsync();

            return Ok(repoResponse.Movies.ToArray());
        }
        [HttpPost("SaveMovie")]
        public async Task<ActionResult<IEnumerable<Response>>> SaveMovieAsync(Movie model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _movieRepository.SaveMovieAsync(model);
            return Ok(response);
        }
        
    }
}