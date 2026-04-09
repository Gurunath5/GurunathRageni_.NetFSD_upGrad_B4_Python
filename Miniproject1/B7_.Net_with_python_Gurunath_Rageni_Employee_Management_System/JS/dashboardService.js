function updateDashboard() {

    document.getElementById("totalEmployees").innerText = employees.length;

    let active = employees.filter(emp => emp.status === "Active").length;
    document.getElementById("activeEmployees").innerText = active;

    let inactive = employees.filter(emp => emp.status === "Inactive").length;
    document.getElementById("inactiveEmployees").innerText = inactive;

    let departments = [...new Set(employees.map(emp => emp.department))].length;
    document.getElementById("departments").innerText = departments;
}


function getDeptClass(dept) {
    switch(dept) {
        case "Engineering": return "eng";
        case "Marketing": return "mkt";
        case "HR": return "hr";
        case "Finance": return "fin";
        case "Operations": return "ops";
        default: return "eng";
    }
}


function updateDepartmentBreakdown() {

    let deptCount = {};
    let total = employees.length;

    employees.forEach(emp => {
        deptCount[emp.department] = (deptCount[emp.department] || 0) + 1;
    });

    let container = document.getElementById("departmentList");
    container.innerHTML = "";

    for (let dept in deptCount) {

        let count = deptCount[dept];
        let percent = ((count / total) * 100).toFixed(0);
        let cls = getDeptClass(dept);

        container.innerHTML += `
            <div class="department-row">
                <span><span class="dept-badge ${cls}">${dept}</span></span>
                <span>${count}</span>
                <div class="bar-container">
                    <div class="bar ${cls}" style="width:${percent}%"></div>
                </div>
                <span>${percent}%</span>
            </div>
        `;
    }
}


module.exports = {
    updateDashboard,
    updateDepartmentBreakdown
};