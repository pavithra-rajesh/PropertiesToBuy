import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-makelaars',
  templateUrl: './makelaars.component.html'
})
export class MakelaarsComponent {
  aanbodObjects: Object[];
  topMakelaars: TopMakelaars[];
  errorMessage: any;

  constructor(private httpClient: HttpClient) {
  }

  public getAllMakelaars(chosenCity: string) {
    this.topMakelaars = undefined;
    this.httpClient.get('/api/makelaars/getallmakelaars/' + chosenCity).subscribe(
      (result: AllMakelaarsResponse) => {
        this.errorMessage = null;
        this.aanbodObjects = result.objects;
      },
      (error) => {
        this.topMakelaars = undefined;
        this.errorMessage = error;
      });
  }

  public getTopMakelaars(chosenCity: string, hasTuin: boolean = false) {
    this.aanbodObjects = undefined;
    const requestUri = '/api/makelaars/gettopmakelaars/' + chosenCity + '/' + hasTuin
    this.httpClient.get(requestUri).subscribe(
      (result: TopMakelaarsResponse) => {
        this.errorMessage = null;
        this.topMakelaars = result.topMakelaars;
    },
      (error) => {
        this.topMakelaars = undefined;
        this.errorMessage = error;
      });
  }
}

interface Object {
  makelaarId: number;
  makelaarNaam: string;
  woonPlaats: string;
}

interface AllMakelaarsResponse {
  objects: Object[];
}

interface TopMakelaarsResponse {
  topMakelaars: TopMakelaars[];
}

interface TopMakelaars {
  makelaarId: number;
  makelaarName: string;
  count: number;
}
