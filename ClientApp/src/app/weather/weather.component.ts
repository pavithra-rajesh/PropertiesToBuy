import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-weather-component',
  templateUrl: './weather.component.html'
})
export class WeatherComponent {
  weather: Weather;

  constructor(private httpClient: HttpClient) {
  }

  public getWeather(chosenCity: string) {
    this.httpClient.get('/api/weather/city/' + chosenCity).subscribe((result: Weather) => {
      this.weather = result;
    });
  }
}

interface Weather {
  temp: string;
  summary: string;
  city: string;
}
