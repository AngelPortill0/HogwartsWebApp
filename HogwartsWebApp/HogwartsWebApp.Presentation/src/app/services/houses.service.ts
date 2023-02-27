import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HousesService {
  private url = "http://localhost:5291/api/House/List";

  constructor(private httpClient: HttpClient) { }

  getHouses() {
    return this.httpClient.get(this.url);
  }
}
