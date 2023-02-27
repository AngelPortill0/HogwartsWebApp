import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UpdateStudentService } from '../services/update-student.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-student',
  templateUrl: './update-student.component.html',
  styleUrls: ['./update-student.component.css']
})
export class UpdateStudentComponent implements OnInit {

  constructor(private updateStudentServide: UpdateStudentService, private readonly formBuilder: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any) { }

  updateForm!: FormGroup;

  ngOnInit(): void {
    this.updateForm = this.initForm();
  }

  initForm(): FormGroup {
    return this.formBuilder.group({
      name: [this.data.name, [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      lastname: [this.data.lastname, [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      age: [this.data.age, [Validators.required, Validators.maxLength(2), Validators.pattern("^[0-9]{1,2}$")]],
      identityNumber: [this.data.identityNumber, [Validators.required, Validators.maxLength(10), Validators.pattern("^[0-9]{1,10}$")]]
    })
  }

  updateStudent() {
    this.updateStudentServide.updateStudent(
      this.data.id,
      this.updateForm.value.name,
      this.updateForm.value.lastname,
      this.updateForm.value.age,
      this.updateForm.value.identityNumber
    ).subscribe((statusStudent: any) => {
      Swal.fire({
        icon: 'info',
        title: 'Aviso',
        text: statusStudent.responseBody
      })
        .then(result => {
          if (result.value) {
            window.location.href = "/History";
          }
        })
    })
  }
}
