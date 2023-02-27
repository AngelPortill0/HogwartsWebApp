import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UpdateStudentService {
  private urlApi = 'http://localhost:5291/api/Student/UpdateInformation';

  constructor(private httpClient: HttpClient) { }

  updateStudent(studedntId: any, name: string, lastname: string, age: any, identityNumber: any) {
    let requestBody = {
      "studentId": studedntId,
      "name": name,
      "lastName": lastname,
      "age": age,
      "identityNumber": identityNumber
    }

    return this.httpClient.put(this.urlApi, requestBody)
  }
}
