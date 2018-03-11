import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { IStudent } from "../student/IStudent";
import 'rxjs/add/operator/map';



@Injectable()
export class DataService {

    constructor(private http: HttpClient){

    }

    // Get logged in student from api
    public student: IStudent;

    loadStudent(): Observable<IStudent> {
        return this.http.get("/api/student/id")
            .map((result: Response) => this.student = result.json());
    }

}