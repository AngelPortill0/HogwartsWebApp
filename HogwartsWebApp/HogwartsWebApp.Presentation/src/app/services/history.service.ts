import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class HistoryService {
  private urlListApi = "http://localhost:5291/api/Student/List";
  private urlDeleteApi = "http://localhost:5291/api/Student/Delete/"

  constructor(private httpClient: HttpClient) { }

  getHistory() {
    return this.httpClient.get(this.urlListApi);
  }

  deleteHistory(studentId: any) {
    return this.httpClient.delete(`${this.urlDeleteApi}${studentId}`)
  }
}