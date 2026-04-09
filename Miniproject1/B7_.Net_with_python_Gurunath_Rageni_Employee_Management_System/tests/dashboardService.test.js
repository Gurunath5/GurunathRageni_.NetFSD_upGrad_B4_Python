

const {
    updateDashboard,
    updateDepartmentBreakdown
} = require('../js/dashboardService');

// Mock DOM
document.body.innerHTML = `
    <div id="totalEmployees"></div>
    <div id="activeEmployees"></div>
    <div id="inactiveEmployees"></div>
    <div id="departments"></div>
    <div id="departmentList"></div>
`;

global.employees = [];

describe("Dashboard Service Tests", () => {

    beforeEach(() => {
        employees.length = 0;

        employees.push(
            { id: 1, department: "Engineering", status: "Active" },
            { id: 2, department: "HR", status: "Inactive" },
            { id: 3, department: "Engineering", status: "Active" }
        );
    });

    test("should calculate summary correctly", () => {
        updateDashboard();

        expect(Number(document.getElementById("totalEmployees").innerText)).toBe(3);

        expect(Number(document.getElementById("activeEmployees").innerText)).toBe(2);

        expect(Number(document.getElementById("inactiveEmployees").innerText)).toBe(1);

        expect(Number(document.getElementById("departments").innerText)).toBe(2);
    });

    test("should render department breakdown", () => {
        updateDepartmentBreakdown();

        const html = document.getElementById("departmentList").innerHTML;

        expect(html).toContain("Engineering");
        expect(html).toContain("HR");
    });

});