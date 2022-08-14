using SearchOpenMovies.Models.Entities;

namespace SearchOpenMovies.Dto
{
    public class RetrievedSavedMoviesResponse: Response
    {
        public List<Movie>Movies { get; set; }
    }
}
