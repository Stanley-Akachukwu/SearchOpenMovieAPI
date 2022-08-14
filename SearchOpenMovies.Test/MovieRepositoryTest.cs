using Microsoft.AspNetCore.Mvc;
using SearchOpenMovies.Controllers;
using SearchOpenMovies.Dto;
using SearchOpenMovies.Models;
using SearchOpenMovies.Models.Entities;
using SearchOpenMovies.Services;
using SearchOpenMovies.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchOpenMovies.Test
{
    public class MovieRepositoryTest
    {
        [Fact]
        public void Check_That_Method_FetchMovieAsync_Is_Listof_Movies()
        {
            IMovieRepository repository = new MovieRepositoryMock();
            var result = repository.FetchMovieAsync(new MovieRequest());
            Assert.True(result.GetType() == typeof(Task<List<Movie>>));
        }
        [Fact]
        public void Check_That_Method_GetSavedMoviesAsync_Is_Listof_Movies()
        {
            IMovieRepository repository = new MovieRepositoryMock();
            var result = repository.GetSavedMoviesAsync();
            Assert.True(result.GetType() == typeof(Task<List<Movie>>));
        }
        [Fact]
        public void Check_That_Method_SaveMovieAsync_Is_Listof_Movies()
        {
            IMovieRepository repository = new MovieRepositoryMock();
            var result = repository.SaveMovieAsync(new Movie());
            Assert.True(result.GetType() == typeof(Task<Response>));

        }
    }
}


