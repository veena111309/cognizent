package Algorithms_DataStructures.Exercise4;

import java.util.Arrays;
import java.util.Optional;

/**
 * ============================================================================
 * Employee Directory Management System
 * Goal: Demonstrate Array operations (insertion, traversal, lookup, deletion)
 *       using a custom resizable array data structure.
 * Scope: Deep Skilling Week 1 - Algorithms & Data Structures (Exercise 4)
 *
 * Design Enhancements:
 * 1. Implements automatic array capacity resizing when the capacity limit is reached,
 *    instead of rejecting additions (standard industry best practice).
 * 2. Employs strong class encapsulation and Optional wrappers.
 *
 * Array Complexity Analysis:
 * -------------------------------------------------------------
 * | Operation        | Time Complexity | Explanation          |
 * |------------------|-----------------|----------------------|
 * | Add (Append)     | O(1) Amortized  | Direct index insert  |
 * | Search (ID)      | O(n)            | Linear scan required |
 * | Traversal        | O(n)            | Accesses all elements|
 * | Delete (by ID)   | O(n)            | Requires shift copy  |
 * -------------------------------------------------------------
 * ============================================================================
 */

class CorporateStaff {
    private final int id;
    private final String name;
    private final String department;
    private final double salary;

    public CorporateStaff(int id, String name, String department, double salary) {
        this.id = id;
        this.name = name;
        this.department = department;
        this.salary = salary;
    }

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public String getDepartment() {
        return department;
    }

    public double getSalary() {
        return salary;
    }

    @Override
    public String toString() {
        return String.format("Staff ID: %-5d | Name: %-12s | Dept: %-12s | Monthly Pay: $%.2f", 
                id, name, department, salary);
    }
}

public class EmployeeDirectory {
    private CorporateStaff[] list;
    private int activeCount;
    private static final int INITIAL_CAPACITY = 2; // Low initial capacity to demonstrate resizing logic

    public EmployeeDirectory() {
        this.list = new CorporateStaff[INITIAL_CAPACITY];
        this.activeCount = 0;
    }

    /**
     * Appends an employee. Resizes the backing array if capacity is reached.
     * Complexity: O(1) average/amortized
     */
    public void addEmployee(CorporateStaff employee) {
        if (activeCount == list.length) {
            resizeBackingArray();
        }
        list[activeCount++] = employee;
        System.out.println("Added: " + employee.getName());
    }

    /**
     * Resizes backing array by doubling its capacity.
     */
    private void resizeBackingArray() {
        int newCapacity = list.length * 2;
        list = Arrays.copyOf(list, newCapacity);
        System.out.println(">> Resized directory buffer to: " + newCapacity);
    }

    /**
     * Search employee by ID.
     * Complexity: O(n)
     */
    public Optional<CorporateStaff> search(int employeeId) {
        for (int i = 0; i < activeCount; i++) {
            if (list[i].getId() == employeeId) {
                return Optional.of(list[i]);
            }
        }
        return Optional.empty();
    }

    /**
     * Delete employee by ID, shifts all trailing elements left to close the gap.
     * Complexity: O(n)
     */
    public boolean delete(int employeeId) {
        int targetIndex = -1;
        for (int i = 0; i < activeCount; i++) {
            if (list[i].getId() == employeeId) {
                targetIndex = i;
                break;
            }
        }

        if (targetIndex == -1) {
            return false;
        }

        // Shift elements to the left
        for (int i = targetIndex; i < activeCount - 1; i++) {
            list[i] = list[i + 1];
        }

        list[activeCount - 1] = null; // Clean reference for garbage collection
        activeCount--;
        return true;
    }

    /**
     * Print all active employees.
     */
    public void traverse() {
        if (activeCount == 0) {
            System.out.println("Directory is empty.");
            return;
        }
        for (int i = 0; i < activeCount; i++) {
            System.out.println(list[i]);
        }
    }

    public static void main(String[] args) {
        EmployeeDirectory dir = new EmployeeDirectory();

        System.out.println("=== Seeding Directory (Triggers Resizing) ===");
        dir.addEmployee(new CorporateStaff(101, "Alice Smith", "HR", 4500.00));
        dir.addEmployee(new CorporateStaff(102, "Bob Jones", "IT", 6200.00));
        dir.addEmployee(new CorporateStaff(103, "Charlie Day", "Finance", 5200.00)); // triggers resize
        dir.addEmployee(new CorporateStaff(104, "David Tennant", "Legal", 7800.00));

        System.out.println("\n=== Directory Roster ===");
        dir.traverse();

        System.out.println("\n=== Searching Staff ID 102 ===");
        dir.search(102).ifPresentOrElse(
            System::println,
            () -> System.out.println("Not found.")
        );

        System.out.println("\n=== Deleting Staff ID 102 ===");
        if (dir.delete(102)) {
            System.out.println("Deletion success.");
        } else {
            System.out.println("Deletion failed. Record not found.");
        }

        System.out.println("\n=== Directory Roster after deletion ===");
        dir.traverse();
    }
}
