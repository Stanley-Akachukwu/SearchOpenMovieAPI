import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Movie } from '../models/movie';
import { MovieRequest } from '../models/movieRequest';
import { MovieService } from '../services/movie.service';
@Component({
  selector: 'app-fetch-movies',
  templateUrl: './fetch-movies.component.html',
  styleUrls: ['./fetch-movies.component.css']
})
export class FetchMoviesComponent implements OnInit {
 movies: Movie[] = [];
  hasMovies: boolean = false;
  singleSearchOption: boolean = false;
  multipleSearchOption: boolean = false;
  movieRequest: MovieRequest = {
    searchOption: "",
    iMDbID: "",
    fullPlot: false,
    releaseYear: new Date(), 
    movieType: "",
    title: "",
    pageSize: 0
  };

  constructor(private movieService: MovieService, private router: Router,private route: ActivatedRoute) { }

  ngOnInit(): void {
    delete sessionStorage.movie;
  }


  onSearchOptionChange() {
    this.singleSearchOption = this.movieRequest.searchOption == "Single" ? true : false;
    this.multipleSearchOption = this.movieRequest.searchOption == "Multiple" ? true : false;
  }

  
  onSubmit(): void {
    if (this.movieRequest.searchOption == "Single") {
      if (this.movieRequest.iMDbID === this.movieRequest.title === null || this.movieRequest.iMDbID === this.movieRequest.title === undefined) {
        alert("Please enter IMBID or Title to proceed")
      }
    }

    if (this.movieRequest.searchOption == "Multiple") {
      if (this.movieRequest.title === null || this.movieRequest.title === undefined) {
        alert("Please enter Title to proceed")
      }
    }
    this.movieService.getMovies(this.movieRequest).subscribe(result => {
      this.movies = result;
      this.hasMovies = true;
      sessionStorage.setItem('movie', JSON.stringify(this.movies[0]))
     /* console.log(this.movies);*/
    }, error => console.error(error));
  }
  onGetdetails(imDdbID: string) {
    const movie = this.movies.find(m => m.imdbID === imDdbID);
    console.log(movie);

    //alert(movie?.imdbID);
    this.router.navigate([`save-search/${imDdbID}`])
  }
 
  
}
