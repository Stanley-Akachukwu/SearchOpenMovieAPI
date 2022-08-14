namespace SearchOpenMovies.Models
{
    public class MovieRequest
    {
        public string SearchOption { get; set; }
        public string IMDbID { get; set; }
        public bool FullPlot { get; set; }
        public DateTime ReleaseYear { get; set; }
        public string MovieType { get; set; }
        public string Title { get; set; }
        public int PageSize { get; set; }
    }
}

