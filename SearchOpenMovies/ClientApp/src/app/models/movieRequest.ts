
export interface MovieRequest {
  searchOption: string;
  iMDbID: string;
  fullPlot: boolean;
  releaseYear: Date;
  movieType: string;
  title: string;
  pageSize: number;
}
