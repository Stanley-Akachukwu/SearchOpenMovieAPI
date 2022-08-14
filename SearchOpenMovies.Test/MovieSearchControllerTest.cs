using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SearchOpenMovies.Controllers;
using SearchOpenMovies.Dto;
using SearchOpenMovies.Models;
using SearchOpenMovies.Models.Entities;
using SearchOpenMovies.Services.Contracts;

namespace SearchOpenMovies.Test
{
    public class MovieSearchControllerTest
    {
       
        [Fact]
        public void WhenGettingMovies_From_API_ThenReturn_Movies()
        {
            IMovieRepository repository = new MovieRepositoryMock();
            var movieSearchController = new MovieSearchController(repository);
            var request = new MovieRequest()
            {
                FullPlot = false,
                IMDbID = "tt1234646",
                MovieType = "Movie",
                PageSize = 0,
                ReleaseYear = DateTime.Now,
                SearchOption = "Single",
                Title = "Title",
            };
            var result = movieSearchController.FetchMovieAsync(request).Result;
            Assert.NotNull(result);
        }
        [Fact]
        public void WhenGettingSaved_Movies_From_DB_ThenReturn_Movies()
        {
            IMovieRepository repository = new MovieRepositoryMock();
            var movieSearchController = new MovieSearchController(repository);
            repository.GetSavedMoviesAsync();

            var result = movieSearchController.FetchSavedMoviesAsync().Result;
            Assert.NotNull(result);
        }
        [Fact]
        public void WhenSavingMovies_To_DB_Then_ThenReturn_Ok_Response_Obj()
        {
            IMovieRepository repository = new MovieRepositoryMock();
            var movieSearchController = new MovieSearchController(repository);
            var request = new Movie ()
            {
                Title = "Social Net Work",
                Actors = "Jesse Eisenberg, Andrew Garfield, Justin Timberlake",
                Awards = "Won 3 Oscars. 172 wins & 186 nominations total",
                Id = 1,
                Plot = $"On a fall night in 2003,Harvard undergrad and computer programming genius Mark Zuckerberg sits down at his computer and heatedly begins working on a new idea.In a fury of blogging and programming,what begins in his dorm room soon becomes a global social network and a revolution in communication.A mere six years and 500 million friends later,Mark Zuckerberg is the youngest billionaire in history... but for this entrepreneur, success leads to both personal and legal complications."
            };
            var result = movieSearchController.SaveMovieAsync(request).Result;
            var response = (result.Result as OkObjectResult).Value as Response;
            Assert.Equal(200, response.ResponseCode);
        }
    }
}