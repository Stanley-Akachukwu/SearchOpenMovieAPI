import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Movie } from '../models/movie';
import { MovieService } from '../services/movie.service';
import { Rating } from "../models/Rating";
import { ApiResponse } from '../models/response';

@Component({
  selector: 'app-save-search',
  templateUrl: './save-search.component.html',
  styleUrls: ['./save-search.component.css']
})
export class SaveSearchComponent implements OnInit {
  movies: Movie[] = [];
   movie: Movie = {
      title: "",
      year: "",
      rated: "",
      released: "",
      runtime: "",
      genre: "",
      director: "",
      writer: "",
      actors: "",
      plot: "",
      language: "",
      country: "",
      awards: "",
      poster: "",
      metascore: "",
      imdbRating: "",
      imdbVotes: "",
      imdbID: "",
      type: "",
       dvd: "",
      boxOffice: "",
      production: "",
      website: "",
      response: "",
      ratings: []
  };
  hasMovies: boolean = false;
  singhasMoviesleSearchOption: boolean = false;
  multipleSearchOption: boolean = false;
  response: ApiResponse = { responseCode: 0, responseMessage: '' }
  movieImdbId: string = '';

  constructor(private movieService: MovieService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    //console.log(id);

    const obj = JSON.parse(JSON.stringify(sessionStorage.getItem('movie')))
    this.movie = JSON.parse(obj);

    this.movieImdbId = JSON.parse(JSON.stringify(id))
    if (this.movieImdbId === id) {
      this.hasMovies = true;
    }
    
  }


  

  
  onSubmit(): void {
    this.movieService.saveMovie(this.movie).subscribe(result => {
      this.response =  JSON.parse(JSON.stringify(result))
      alert(this.response.responseMessage);
    }, error => console.error(error));
  }
 
  
}
