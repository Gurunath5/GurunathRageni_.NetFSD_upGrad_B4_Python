
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

function getBarClass(percent) {
    let rounded = Math.round(percent / 10) * 10;
    return "bar-" + rounded;
}

function renderDepartmentBreakdown(data) {

    let container = document.getElementById("departmentList");
    container.innerHTML = "";

    data.forEach(item => {

        let cls = getDeptClass(item.name);
        let widthClass = getBarClass(item.percent);

        container.innerHTML += `
            <div class="department-row">

                <span>
                    <span class="dept-badge ${cls}">${item.name}</span>
                </span>

                <span>${item.count}</span>

                <div class="bar-container">
                    <div class="bar ${cls} ${widthClass}"></div>
                </div>

                <span>${item.percent}%</span>

            </div>
        `;
    });
}

function renderRecentEmployees() {

    let container = document.getElementById("recentEmployeesList");
    container.innerHTML = "";

    let recent = employees.slice(-5).reverse();

    recent.forEach(emp => {

        let initials = emp.name.split(" ").map(n => n[0]).join("");
        let badge = emp.status === "Active" ? "badge-active" : "badge-inactive";

         container.innerHTML += `
<div class="employee-row">

    <div class="emp-left">
        <div class="avatar">${initials}</div>

        <div>
            <div class="emp-name">${emp.name}</div>
            <div class="emp-role">${emp.department}</div>
        </div>
    </div>

    <div class="d-flex gap-2">
        <span class="dept-badge ${getDeptClass(emp.department)}">
            ${emp.department}
        </span>

        <span class="${badge}">
            ${emp.status}
        </span>
    </div>

</div>
`;
    });
}

 

// 1. Modify the function to accept data as a parameter
function renderEmployeesTable(dataToRender = employees) {
    let container = document.getElementById("employeeTableBody");
    container.innerHTML = "";

    // Update the "Showing X of X" count
    const countDisplay = document.querySelector(".col-md-2.text-end.small");
    if(countDisplay) {
        countDisplay.innerText = `Showing ${dataToRender.length} of ${employees.length} employees`;
    }

    dataToRender.forEach(emp => {
        let initials = emp.name
            ? emp.name.split(" ").map(n => n[0]).join("")
            : "?";

        let statusClass = emp.status === "Active"
            ? "badge bg-success"
            : "badge bg-danger";

        container.innerHTML += `
        <tr>
            <td>#${emp.id || "-"}</td>
            <td><div class="avatar">${initials}</div></td>
            <td class="fw-semibold">${emp.name || "-"}</td>
            <td>${emp.email || "-"}</td>
            <td>
                <span class="dept-badge ${getDeptClass(emp.department || "")}">
                    ${emp.department || "-"}
                </span>
            </td>
            <td>${emp.designation || "-"}</td>
            <td>₹${emp.salary ? emp.salary.toLocaleString("en-IN") : "-"}</td>
            <td>${emp.joinDate || "-"}</td>
            <td><span class="${statusClass}">${emp.status || "-"}</span></td>
            <td><button class="btn btn-sm btn-outline-primary me-1 view-btn" data-id="${emp.id}">👁️</button>
                <button class="btn btn-sm btn-outline-warning me-1 edit-btn" data-id="${emp.id}">🖊️</button>
                <button class="btn btn-sm btn-outline-danger delete-btn" data-id="${emp.id}">🗑️</button>
            </td>
        </tr>`;
    });
}


 

// 1. Global States
let currentFilters = {
    search: "",
    dept: "All",
    status: "All"
};

let sortConfig = {
    column: "",    // 'name', 'salary', or 'joinDate'
    direction: "asc" 
};

/**
 * 2. The Master Engine (Filter + Sort)
 */
function applyFilters() {
    if (!employees) return;

    // --- FILTERING ---
    let filteredList = employees.filter(emp => {
        // Search check
        const searchTerm = (currentFilters.search || "").toLowerCase();
        const matchesSearch = 
            (emp.name || "").toLowerCase().includes(searchTerm) || 
            (emp.email || "").toLowerCase().includes(searchTerm);
        
        // Dept check
        const matchesDept = currentFilters.dept === "All" || emp.department === currentFilters.dept;
        
        // Status check - CRITICAL: This must handle the "All" button
        const matchesStatus = currentFilters.status === "All" || emp.status === currentFilters.status;

        return matchesSearch && matchesDept && matchesStatus;
    });

    // --- SORTING ---
    if (sortConfig.column) {
        filteredList.sort((a, b) => {
            let valA = a[sortConfig.column];
            let valB = b[sortConfig.column];

            if (sortConfig.column === 'name') {
                valA = (valA || "").toLowerCase();
                valB = (valB || "").toLowerCase();
                return sortConfig.direction === "asc" ? valA.localeCompare(valB) : valB.localeCompare(valA);
            }
            
            if (sortConfig.column === 'salary') {
                valA = parseFloat(String(valA).replace(/[^0-9.-]+/g, "")) || 0;
                valB = parseFloat(String(valB).replace(/[^0-9.-]+/g, "")) || 0;
            }

            if (sortConfig.column === 'joinDate') {
                valA = new Date(valA || 0).getTime();
                valB = new Date(valB || 0).getTime();
            }

            if (valA < valB) return sortConfig.direction === "asc" ? -1 : 1;
            if (valA > valB) return sortConfig.direction === "asc" ? 1 : -1;
            return 0;
        });
    }

    renderEmployeesTable(filteredList);
    updateSortIcons();
}

// 3. Event Listeners
document.addEventListener("DOMContentLoaded", () => {
    
// --- VIEW BUTTON CLICK HANDLER (Event Delegation) ---
document.getElementById("employeeTableBody")?.addEventListener("click", (e) => {
    // Check if clicked element is the view button or the 👁️ icon inside it
    const viewBtn = e.target.closest(".view-btn");
    
    if (viewBtn) {
        const empId = viewBtn.getAttribute("data-id");
        
        // Find employee data (using your existing global 'employees' array)
        const emp = employees.find(item => String(item.id) === String(empId));

        if (emp) {
            // 1. Populate Avatar & Header
            const initials = emp.name ? emp.name.split(" ").map(n => n[0]).join("") : "?";
            const avatarContainer = document.getElementById("viewAvatar");
            if (avatarContainer) avatarContainer.innerText = initials;
            
            document.getElementById("viewFullName").innerText = emp.name || "N/A";
            document.getElementById("viewDeptBadge").innerText = emp.department || "N/A";
            
            // 2. Populate Grid Data
            document.getElementById("viewEmail").innerText = emp.email || "N/A";
            document.getElementById("viewPhone").innerText = emp.phno || emp.phone || "N/A";
            document.getElementById("viewDesignation").innerText = emp.designation || "N/A";
            
            // Format Currency for Salary
            const salary = emp.salary ? Number(emp.salary).toLocaleString("en-IN") : "0";
            document.getElementById("viewSalary").innerText = `₹${salary}`;
            
            document.getElementById("viewJoinDate").innerText = emp.joinDate || "N/A";
            
            // 3. Status Badge logic
            const statusEl = document.getElementById("viewStatus");
            const statusCls = emp.status === "Active" ? "bg-success" : "bg-danger";
            statusEl.innerHTML = `<span class="badge ${statusCls}">${emp.status}</span>`;

            // 4. Show the Modal
            $("#viewModalOverlay").fadeIn(200);
        }
    }
});






















    // Search
    document.getElementById("employeeSearch")?.addEventListener("input", (e) => {
        currentFilters.search = e.target.value;
        applyFilters();
    });

    // Dropdown
    document.getElementById("deptFilter")?.addEventListener("change", (e) => {
        currentFilters.dept = e.target.value;
        applyFilters();
    });

    // Status Buttons - FIXED LOGIC
    document.querySelectorAll(".status-btn").forEach(btn => {
        btn.addEventListener("click", function() {
            // Update the filter state using the data-status attribute
            currentFilters.status = this.getAttribute("data-status");

            // UI: Update button colors
            document.querySelectorAll(".status-btn").forEach(b => {
                const s = b.getAttribute("data-status");
                b.classList.remove("btn-primary", "btn-success", "btn-danger");
                if(s === "All") b.classList.add("btn-outline-primary");
                if(s === "Active") b.classList.add("btn-outline-success");
                if(s === "Inactive") b.classList.add("btn-outline-danger");
            });

            // Set active color
            this.classList.remove("btn-outline-primary", "btn-outline-success", "btn-outline-danger");
            if(currentFilters.status === "All") this.classList.add("btn-primary");
            if(currentFilters.status === "Active") this.classList.add("btn-success");
            if(currentFilters.status === "Inactive") this.classList.add("btn-danger");

            applyFilters();
        });
    });

    // Sort Headers
    document.querySelectorAll(".sort-header").forEach(header => {
        header.addEventListener("click", () => {
            const column = header.getAttribute("data-column");
            sortConfig.direction = (sortConfig.column === column && sortConfig.direction === "asc") ? "desc" : "asc";
            sortConfig.column = column;
            applyFilters();
        });
    });
});