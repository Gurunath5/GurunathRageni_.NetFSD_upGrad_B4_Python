
const employeeService = require('../js/employeeService');
// Mock global variables
global.employees = [];
global.storageService = {
    saveEmployee: jest.fn()
};

describe("Employee Service Tests", () => {

    beforeEach(() => {
        employees.length = 0;

        storageService.saveEmployee.mockImplementation((emp) => {
            employees.push(emp);
            return true;
        });
    });

    test("should generate ID = 1 when no employees", () => {
        expect(employeeService.generateNextId()).toBe(1);
    });

    test("should generate next ID correctly", () => {
        employees.push({ id: 1 }, { id: 2 }, { id: 5 });
        expect(employeeService.generateNextId()).toBe(6);
    });

    test("should create new employee correctly", () => {
        const formData = {
            fName: "John",
            lName: "Doe",
            email: "john@test.com",
            phno: "9876543210",
            dept: "Engineering",
            desig: "Developer",
            salary: "50000",
            date: "2026-03-12",
            status: "Active"
        };

        const result = employeeService.createNewEmployee(formData);

        expect(result).toBe(true);
        expect(employees.length).toBe(1);

        const emp = employees[0];
        expect(emp.name).toBe("John Doe");
        expect(emp.salary).toBe(50000);
        expect(emp.department).toBe("Engineering");
        expect(emp.status).toBe("Active");
    });

    test("should format join date correctly", () => {
        const formData = {
            fName: "A",
            lName: "B",
            email: "a@test.com",
            phno: "9999999999",
            dept: "HR",
            desig: "Manager",
            salary: "30000",
            date: "2026-03-15",
            status: "Active"
        };

        employeeService.createNewEmployee(formData);

        expect(employees[0].joinDate).toContain("Mar");
    });

});