import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConnectionServiceService {

  private path: string
  private readonly ACTION = "WeatherForecast/"
  private readonly MHETOD: string = 'GetWeatherForecast'

  constructor(
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string
  ) {
    this.path = baseUrl + this.ACTION
    console.log('path', this.path)
  }

  // Запрос с сервера 
  public startHttpRequest(): Observable<object> {
    return this.http.get(this.path + this.MHETOD)
  }
}
