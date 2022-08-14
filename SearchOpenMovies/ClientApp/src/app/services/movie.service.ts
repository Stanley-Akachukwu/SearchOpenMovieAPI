import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Movie } from '../models/movie';


@Injectable({
  providedIn: 'root'
})
export class MovieService {
  _baseUrl: string;
  _http: HttpClient;
  public movies: Movie[] = [];
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;
  }


  getMovies(movieRequestModel: any): Observable<Movie[]> {
    const movieRequestJson = JSON.parse(JSON.stringify(movieRequestModel));
    return this.http.post<Movie[]>(this._baseUrl + 'api/moviesearch/fetchmovies', movieRequestJson);
  }
  saveMovie(movie: any): Observable<Response> {
    const movieRequestJson = JSON.parse(JSON.stringify(movie));
    return this.http.post<Response>(this._baseUrl + 'api/moviesearch/SaveMovie', movieRequestJson);
  }
  getSavedMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(this._baseUrl + 'api/moviesearch/fetchSavedmovies');
  }
}


 



