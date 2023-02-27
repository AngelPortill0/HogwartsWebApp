import { OnInit } from '@angular/core';
import { HistoryService } from '../services/history.service';
import { MatTableDataSource } from '@angular/material/table'
import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import Swal from 'sweetalert2';
import { UpdateStudentComponent } from '../update-student/update-student.component';

@Component({
  selector: 'app-table-history',
  templateUrl: './table-history.component.html',
  styleUrls: ['./table-history.component.css']
})
export class TableHistoryComponent implements OnInit {

  history!: MatTableDataSource<any>;
  displayColumns: string[] = ['studentId', 'name', 'lastName', 'age', 'identityNumber', 'actions'];
  response: any;
  searchKey!: string;

  constructor(private historyService: HistoryService, public dialog: MatDialog) {
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  ngOnInit(): void {
    this.response = this.historyService.getHistory()
      .subscribe(body => {
        this.history = new MatTableDataSource(Object.values(body)[1]);
        this.history.sort = this.sort;
        this.history.paginator = this.paginator;
      })
  }

  triggerSwal(selectedStudent: any) {
    Swal.fire({
      icon: 'warning',
      title: 'Desea eliminar el registro?',
      showDenyButton: true,
      confirmButtonText: 'Si',
      denyButtonText: 'No',
    }).then((result) => {
      if (result.isConfirmed) {
        this.historyService.deleteHistory(selectedStudent).subscribe(body => {
          Swal.fire({
            icon: 'success',
            title: 'Alumno eliminado con éxito',
          }).then(res => {
            location.reload()
          })
        });
      } else if (result.isDenied) {
        Swal.fire('No se ha eliminado el registro');
      }
    })
  }

  openDialog(studentData: any) {
    const dialogRef = this.dialog.open(UpdateStudentComponent, {
      width: '30%', data: {
        id: studentData[0],
        name: studentData[1],
        lastname: studentData[2],
        age: studentData[3],
        identityNumber: studentData[4]
      }
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.history.filter = filterValue.trim().toLowerCase();

    if (this.history.paginator) {
      this.history.paginator.firstPage();
    }
  }

  onSearchClear() {
    this.searchKey = "";
    this.applySearchFilter();
  }

  applySearchFilter() {
    this.history.filter = this.searchKey.trim().toLowerCase();
  }

  getValue(obj: any) {
    return Object.values(obj)[0];
  }
}
