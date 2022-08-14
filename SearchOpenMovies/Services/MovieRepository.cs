using SearchOpenMovies.Dto;
using SearchOpenMovies.Models;
using SearchOpenMovies.Models.Entities;
using SearchOpenMovies.Services.Contracts;

namespace SearchOpenMovies.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public MovieRepository(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }
        public async Task<List<Movie>> FetchMovieAsync(MovieRequest model)
        {
            var movies = new List<Movie>();

            string queryParams = "";
            if (model.SearchOption == "Single")
            {
                var plot = model.FullPlot == true ? "full" : "short";
                queryParams = "i=" + model.IMDbID + "&apikey=8938e916" + "&plot=" + plot + "&type=" + model.MovieType + "&y=" + model.ReleaseYear;
            }
            if (model.SearchOption == "Multiple")
            {
                queryParams = "s=" + model.Title + "&apikey=8938e916" + "&page=" + "0-" + model.PageSize + "&type=" + model.MovieType + "&y=" + model.ReleaseYear;
            }

            HttpResponseMessage response = await _httpClient.GetAsync("http://www.omdbapi.com/?" + queryParams);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var movie = await response.Content.ReadFromJsonAsync<Movie>();
                movies.Add(movie);
            }

            return await Task.FromResult(movies);
        }
        public async Task<Response> SaveMovieAsync(Movie movie)
        {
            var response = new Response();
            var movieEntity =_context.Movies.FirstOrDefault(x => x.imdbID == movie.imdbID); 
            if(movieEntity != null)
            {
                response = new Response { ResponseCode = 201, ResponseMessage = "Movie already saved!" };
            }
            else
            {
                var ratings = movie.Ratings;
                if (ratings != null)
                {
                    foreach (var item in ratings)
                    {
                        item.MovieImDbId = movie.imdbID;
                      await  _context.Ratings.AddAsync(item);
                    }
                }
              await  _context.Movies.AddAsync(movie);
               await _context.SaveChangesAsync();
                response = new Response { ResponseCode = 200, ResponseMessage = "Successfully saved!" };
            }
            return await Task.FromResult(response);
        }
        public async Task<List<Movie>> GetSavedMoviesAsync()
        {
            var moviesWithRatings = new List<Movie>();
            var movies = _context.Movies.ToList();
            if (movies == null)
            {
                return null;
            };
            
            foreach(var item in movies)
            {
                var ratings = _context.Ratings.Where(r => r.MovieImDbId ==item.imdbID).ToList();
                item.Ratings=ratings;
                moviesWithRatings.Add(item);
            }
            return  await Task.FromResult(moviesWithRatings);
        }

        
    }
}
