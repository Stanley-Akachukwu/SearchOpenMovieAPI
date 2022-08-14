using SearchOpenMovies.Dto;
using SearchOpenMovies.Models;
using SearchOpenMovies.Models.Entities;
using SearchOpenMovies.Services.Contracts;

namespace SearchOpenMovies.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        public MovieRepository(AppDbContext context)
        {
            _context = context;
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
                        _context.Ratings.AddAsync(item);
                    }
                }
                _context.Movies.Add(movie);
                _context.SaveChangesAsync();
                response = new Response { ResponseCode = 200, ResponseMessage = "Successfully saved!" };
            }
            return await Task.FromResult(response);
        }
        public async Task<RetrievedSavedMoviesResponse> GetSavedMoviesAsync()
        {
            var response = new RetrievedSavedMoviesResponse();
            var moviesWithRatings = new List<Movie>();
            var movies = _context.Movies.ToList();
            if (movies != null)
            {
                response = new RetrievedSavedMoviesResponse
                {
                    ResponseCode = 404,
                    ResponseMessage = "Failed"
                };
            };
            
            foreach(var item in movies)
            {
                var ratings = _context.Ratings.Where(r => r.MovieImDbId ==item.imdbID).ToList();
                item.Ratings=ratings;
                moviesWithRatings.Add(item);
            }

            response = new RetrievedSavedMoviesResponse {
                Movies = moviesWithRatings,
                ResponseCode = 200,
                ResponseMessage = "Successful"
            };
             
            return  await Task.FromResult(response);
        }
    }
}
