import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-makelaars',
  templateUrl: './makelaars.component.html'
})
export class MakelaarsComponent {
  aanbodObjects: Object[];
  topMakelaars: TopMakelaars[];

  constructor(private httpClient: HttpClient) {
  }

  public getAllMakelaars(chosenCity: string) {
    this.topMakelaars = undefined;
    this.httpClient.get('/api/makelaars/getallmakelaars/' + chosenCity).subscribe((result: AllMakelaarsResponse) => {
      this.aanbodObjects = result.objects;
    });
  }

  public getTopMakelaars(chosenCity: string) {
    this.aanbodObjects = undefined;
    this.httpClient.get('/api/makelaars/gettopmakelaars/' + chosenCity).subscribe((result: TopMakelaarsResponse) => {
      this.topMakelaars = result.topMakelaars;
    });
  }

  public getTopMakelaarsWithTuin(chosenCity: string) {
    this.aanbodObjects = undefined;
    this.httpClient.get('/api/makelaars/gettopmakelaarswithtuin/' + chosenCity).subscribe((result: TopMakelaarsResponse) => {
      this.topMakelaars = result.topMakelaars;
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
