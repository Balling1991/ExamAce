import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/dataService';

import { IStudent } from './IStudent';


@Component({
  selector: 'student-component',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})

export class StudentComponent implements OnInit {

  public student: IStudent;

  constructor(private data: DataService) {
    this.student = data.student;
  }

  ngOnInit(): void {
    this.data.loadStudent()
      .subscribe(() => this.student = this.data.student);
  }
}
