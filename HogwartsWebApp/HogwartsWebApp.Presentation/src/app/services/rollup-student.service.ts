import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RollupStudentService {
  urlEnroll = 'http://localhost:5291/api/Student/Enroll';
  urlAddHistory = 'http://localhost:5291/api/StudentHistory/AddHistory/';

  constructor(private httpClient: HttpClient) { }

  rollUpStudent(studentData: object): any {
    return this.httpClient.post(this.urlEnroll, studentData);
  }

  addStudentToHistory(studentId: any, studentHouse: any): any {
    let requestBody = {
      "student": studentId,
      "house": studentHouse,
      "status": 1,
      "registrationDate": Date.now,
      "lastestUpdate": Date.now
    }
    return this.httpClient.post(`${this.urlAddHistory}${studentId}`, requestBody)
  }
}
