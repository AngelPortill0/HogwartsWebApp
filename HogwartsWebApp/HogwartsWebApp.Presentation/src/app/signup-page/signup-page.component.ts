import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import Swal from 'sweetalert2';

// Services
import { HousesService } from '../services/houses.service';
import { RollupStudentService } from '../services/rollup-student.service';

@Component({
  selector: 'app-signup-page',
  templateUrl: './signup-page.component.html',
  styleUrls: ['./signup-page.component.css']
})
export class SignupPageComponent implements OnInit {

  signUpStudent!: FormGroup;

  constructor(private housesService: HousesService, private StudentRollupService: RollupStudentService, private readonly formBuilder: FormBuilder) {
  }

  houses: any;
  response: any;
  ngOnInit(): void {
    this.housesService.getHouses()
      .subscribe(response => {
        this.houses = Object.values(response);
      })

    this.signUpStudent = this.initForm();
  }

  initForm(): FormGroup {
    return this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      lastname: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      age: ['0', [Validators.required, Validators.maxLength(2), Validators.pattern("^[0-9]{1,2}$")]],
      identityNumber: ['', [Validators.required, Validators.maxLength(10), Validators.pattern("^[0-9]{1,10}$")]],
      house: ['', [Validators.required]]
    })
  }

  rollupStudent(): any {
    let studentData = this.signUpStudent.value;
    this.response = this.StudentRollupService.rollUpStudent(studentData).subscribe((student: any) => {
      this.StudentRollupService.addStudentToHistory(student.student, studentData.house).subscribe((statusStudent: any) => {
        this.response = statusStudent

        Swal.fire({
          icon: 'info',
          title: 'Aviso',
          text: student.responseBody
        })
          .then(result => {
            if (result.value) {
              window.location.href = "/Home";
            }
          })
      })
    });
  }


}

