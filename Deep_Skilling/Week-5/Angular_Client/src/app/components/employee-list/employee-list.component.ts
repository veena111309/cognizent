import { Component, OnInit } from '@angular/core';
import { EmployeeRecord } from '../../models/employee.model';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  employees: EmployeeRecord[] = [];
  errorMessage: string = '';
  selectedEmployee: EmployeeRecord | null = null;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.employeeService.getEmployees().subscribe({
      next: (data) => {
        this.employees = data;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load employee records from server.';
        console.error(err);
      }
    });
  }

  editEmployee(employee: EmployeeRecord): void {
    // Clone record to prevent mutating state directly in view
    this.selectedEmployee = { ...employee };
  }

  saveEmployee(): void {
    if (this.selectedEmployee) {
      this.employeeService.updateEmployee(this.selectedEmployee.id, this.selectedEmployee).subscribe({
        next: () => {
          this.loadEmployees(); // Reload dataset
          this.selectedEmployee = null; // Close form
          alert('Employee updated successfully!');
        },
        error: (err) => {
          this.errorMessage = 'Failed to submit modifications to server.';
          console.error(err);
        }
      });
    }
  }

  cancelEdit(): void {
    this.selectedEmployee = null;
  }
}
