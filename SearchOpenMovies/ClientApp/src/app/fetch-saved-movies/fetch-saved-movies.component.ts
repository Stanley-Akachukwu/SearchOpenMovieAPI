import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Movie } from '../models/movie';
import { MovieRequest } from '../models/movieRequest';
import { MovieService } from '../services/movie.service';
@Component({
  selector: 'app-fetch-saved-movies',
  templateUrl: './fetch-saved-movies.component.html',
  styleUrls: ['./fetch-saved-movies.component.css']
})
export class FetchSavedMoviesComponent implements OnInit {
 movies: Movie[] = [];
  hasMovies: boolean = false;
  

  constructor(private movieService: MovieService, private router: Router,private route: ActivatedRoute) { }

  ngOnInit(): void {
    delete sessionStorage.movie;
    this.movieService.getSavedMovies().subscribe(result => {
      this.movies = result;
      this.hasMovies = true;
      console.log(this.movies);
      sessionStorage.setItem('movie', JSON.stringify(this.movies[0]))
    }, error => console.error(error));
  }


   

  
   
  onGetdetails(imDdbID: string) {
    const movie = this.movies.find(m => m.imdbID === imDdbID);
    console.log(movie);

    //alert(movie?.imdbID);
    this.router.navigate([`save-search/${imDdbID}`])
  }
 
  
}
