using SearchOpenMovies.Dto;
using SearchOpenMovies.Models;
using SearchOpenMovies.Models.Entities;
using SearchOpenMovies.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchOpenMovies.Test
{
    public class MovieRepositoryMock: IMovieRepository
    {
        private List<Movie> Movies;
        public MovieRepositoryMock()
        {
            Movies = new List<Movie>();

            InitializeMockRepository();
        }
       
        private void InitializeMockRepository()
        {
            for (int index = 1; index < 10; index++)
            {
                Movies.Add(new()
                {
                    Title = "Social Net Work"+index,
                    Actors = "" + index,
                    Awards = "" + index,
                    Id = index,
                    Plot = $"On a fall night in 2003,Harvard undergrad and computer programming genius Mark Zuckerberg sits down at his computer and heatedly begins working on a new idea.In a fury of blogging and programming,what begins in his dorm room soon becomes a global social network and a revolution in communication.A mere six years and 500 million friends later,Mark Zuckerberg is the youngest billionaire in history... but for this entrepreneur, success leads to both personal and legal complications. {index}"
                });
            }
        }

        public Task<List<Movie>> FetchMovieAsync(MovieRequest model)
        {
             return Task.FromResult(Movies);
        }

        public Task<List<Movie>> GetSavedMoviesAsync()
        {
            return Task.FromResult(Movies);
        }

        public Task<Response> SaveMovieAsync(Movie movie)
        {
            var response = new Response { ResponseCode = 200, ResponseMessage = "Successfully saved!" };
            return Task.FromResult(response);
        }
    }
}
