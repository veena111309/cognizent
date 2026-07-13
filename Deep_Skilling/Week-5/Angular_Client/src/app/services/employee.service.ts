import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeRecord } from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = 'http://localhost:5000/api/ClientPersonnel'; // Base API endpoint

  constructor(private http: HttpClient) {}

  /**
   * Fetches all employee records from backend REST service
   */
  getEmployees(): Observable<EmployeeRecord[]> {
    return this.http.get<EmployeeRecord[]>(this.apiUrl);
  }

  /**
   * Updates an existing employee record
   */
  updateEmployee(id: number, employee: EmployeeRecord): Observable<EmployeeRecord> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<EmployeeRecord>(`${this.apiUrl}/${id}`, employee, { headers });
  }
}
