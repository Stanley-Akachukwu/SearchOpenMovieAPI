import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { FetchMoviesComponent } from './fetch-movies/fetch-movies.component';
import { SaveSearchComponent } from './save-search/save-search.component';
import { FetchSavedMoviesComponent } from './fetch-saved-movies/fetch-saved-movies.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    FetchMoviesComponent,
    SaveSearchComponent,
    FetchSavedMoviesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'fetch-movies', component: FetchMoviesComponent },
      { path: 'save-search/:id', component: SaveSearchComponent },
      { path: 'fetch-Saved-movies', component: FetchSavedMoviesComponent }, 

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
