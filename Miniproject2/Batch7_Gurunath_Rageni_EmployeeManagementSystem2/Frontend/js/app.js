const App = (() => {

    let _state = {
        page: 1,
        pageSize: CONFIG.PAGE_SIZE,
        search: "",
        department: "",
        status: "",
        sortBy: "name",
        sortDir: "asc"
    };

    let _deleteTargetId = null;
    let _searchDebounce = null;

    async function init() {
        UIService.applyRoleUI();
        showView("dashboardView");
        await Promise.all([loadDashboard(), loadEmployees()]);
        _bindNavEvents();
    }

    function showView(viewId) {
        ["loginView", "signupView", "dashboardView", "employeesView"]
            .forEach(id => {
                const el = document.getElementById(id);
                if (el) el.style.display = "none";
            });
        const target = document.getElementById(viewId);
        if (target) target.style.display = "";
        const nav = document.getElementById("mainnav");
        if (nav) nav.style.display = (viewId === "loginView" || viewId === "signupView") ? "none" : "";
        document.getElementById("navDashboard")?.classList.toggle("active", viewId === "dashboardView");
        document.getElementById("navEmployees")?.classList.toggle("active", viewId === "employeesView");
    }

    function _bindNavEvents() {
        document.getElementById("navDashboard")?.addEventListener("click", async e => {
            e.preventDefault();
            showView("dashboardView");
            await loadDashboard();
        });
        document.getElementById("navEmployees")?.addEventListener("click", async e => {
            e.preventDefault();
            showView("employeesView");
            await loadEmployees();
        });
        document.getElementById("logout")?.addEventListener("click", e => {
            e.preventDefault();
            AuthService.logout();
            showView("loginView");
        });
        document.querySelectorAll("#add_emp").forEach(btn =>
            btn.addEventListener("click", e => { e.preventDefault(); openAddModal(); })
        );
    }

    async function login() {
        const username = document.getElementById("loginUsername").value.trim();
        const password = document.getElementById("loginPassword").value;
        const errorEl = document.getElementById("loginError");
        errorEl.textContent = "";

        if (!username || !password) {
            errorEl.textContent = "Please enter username and password.";
            return;
        }

        const result = await AuthService.login({ username, password });
        if (result.success) {
            await init();
        } else {
            errorEl.textContent = result.message || "Invalid credentials.";
        }
    }

    async function signup() {
        const username = document.getElementById("signupUsername").value.trim();
        const password = document.getElementById("signupPassword").value;
        const confirm = document.getElementById("signupConfirmPassword").value;
        const errorEl = document.getElementById("signupError");
        errorEl.textContent = "";

        if (!username || !password) { errorEl.textContent = "All fields are required."; return; }
        if (password !== confirm) { errorEl.textContent = "Passwords do not match."; return; }
        if (password.length < 6) { errorEl.textContent = "Password must be at least 6 characters."; return; }

        const res = await AuthService.register({ username, password, role: "Viewer" });
        if (res.ok || res.data?.success) {
            UIService.showToast("Account created! Please log in.");
            showView("loginView");
        } else {
            errorEl.textContent = res.data?.message || "Registration failed.";
        }
    }

    async function loadEmployees() {
        try {
            const result = await EmployeeService.getEmployees(_state);
            UIService.renderEmployees(result);
        } catch (err) {
            console.error("loadEmployees error:", err);
        }
    }

    async function loadDashboard() {
        try {
            const data = await DashboardService.getSummary();
            UIService.renderDashboard(data);
        } catch (err) {
            console.error("loadDashboard error:", err);
        }
    }

    function onSearch(value) {
        clearTimeout(_searchDebounce);
        _searchDebounce = setTimeout(() => {
            _state.search = value;
            _state.page = 1;
            loadEmployees();
        }, 350);
    }

    function onDeptFilter(value) {
        _state.department = value === "All" ? "" : value;
        _state.page = 1;
        loadEmployees();
    }

    function onStatusFilter(value) {
        _state.status = value === "All" ? "" : value;
        _state.page = 1;
        document.querySelectorAll(".status-btn").forEach(btn => {
            const isActive = btn.dataset.status === value;
            btn.className = isActive
                ? "btn btn-primary btn-sm status-btn"
                : (btn.dataset.status === "Active"
                    ? "btn btn-outline-success btn-sm status-btn"
                    : (btn.dataset.status === "Inactive"
                        ? "btn btn-outline-danger btn-sm status-btn"
                        : "btn btn-outline-secondary btn-sm status-btn"));
        });
        loadEmployees();
    }

    function onSort(column) {
        if (_state.sortBy === column) {
            _state.sortDir = _state.sortDir === "asc" ? "desc" : "asc";
        } else {
            _state.sortBy = column;
            _state.sortDir = "asc";
        }
        document.querySelectorAll(".sort-header").forEach(th => {
            const icon = th.querySelector(".sort-icon");
            if (!icon) return;
            icon.textContent = th.dataset.column === _state.sortBy
                ? (_state.sortDir === "asc" ? "↑" : "↓") : "↕️";
        });
        loadEmployees();
    }

    function changePage(p) {
        _state.page = p;
        loadEmployees();
    }

    function openAddModal() {
        document.getElementById("addEmployeeForm").reset();
        UIService.clearFormErrors("addEmployeeForm");
        document.getElementById("modalOverlay").style.display = "flex";
    }

    function closeAddModal() {
        document.getElementById("modalOverlay").style.display = "none";
    }

    async function addEmployee() {
        const emp = {
            firstName: document.getElementById("firstName").value.trim(),
            lastName: document.getElementById("lastName").value.trim(),
            email: document.getElementById("email").value.trim(),
            phone: document.getElementById("phno").value.trim(),
            department: document.getElementById("department").value,
            designation: document.getElementById("designation").value.trim(),
            salary: parseFloat(document.getElementById("salary").value),
            joinDate: document.getElementById("joinDate").value,
            status: document.getElementById("status").value
        };

        const errors = ValidationService.validateEmployee(emp);
        if (Object.keys(errors).length) {
            UIService.showFormErrors("addEmployeeForm", errors);
            return;
        }

        const res = await EmployeeService.createEmployee(emp);
        if (res.ok) {
            closeAddModal();
            UIService.showToast("Employee added successfully!");
            _state.page = 1;
            await Promise.all([loadEmployees(), loadDashboard()]);
        } else {
            const serverErrors = ValidationService.mapServerErrors(res.status, res.data);
            UIService.showFormErrors("addEmployeeForm", serverErrors);
        }
    }

    async function viewEmployee(id) {
        try {
            const emp = await EmployeeService.getEmployee(id);
            const initials = emp.name.split(" ").map(n => n[0]).join("").toUpperCase().slice(0, 2);
            document.getElementById("viewAvatar").textContent = initials;
            document.getElementById("viewFullName").textContent = emp.name;
            document.getElementById("viewDeptBadge").textContent = emp.department;
            document.getElementById("viewEmail").textContent = emp.email;
            document.getElementById("viewPhone").textContent = emp.phone;
            document.getElementById("viewDesignation").textContent = emp.designation;
            document.getElementById("viewSalary").textContent = `₹${Number(emp.salary).toLocaleString("en-IN")}`;
            document.getElementById("viewJoinDate").textContent = emp.joinDate;
            document.getElementById("viewStatus").innerHTML = `<span class="badge-${emp.status.toLowerCase()}">${emp.status}</span>`;
            document.getElementById("viewModalOverlay").style.display = "flex";
        } catch (err) {
            UIService.showToast("Failed to load employee details.", "danger");
        }
    }

    async function editEmployee(id) {
        try {
            const emp = await EmployeeService.getEmployee(id);
            document.getElementById("editEmpId").value = emp.id;
            document.getElementById("editFirstName").value = emp.firstName;
            document.getElementById("editLastName").value = emp.lastName;
            document.getElementById("editEmail").value = emp.email;
            document.getElementById("editPhone").value = emp.phone;
            document.getElementById("editDepartment").value = emp.department;
            document.getElementById("editDesignation").value = emp.designation;
            document.getElementById("editSalary").value = emp.salary;
            document.getElementById("editStatus").value = emp.status;
            const d = new Date(emp.joinDate);
            document.getElementById("editJoinDate").value = d.toISOString().split("T")[0];
            UIService.clearFormErrors("editEmployeeForm");
            new bootstrap.Modal(document.getElementById("editEmployeeModal")).show();
        } catch (err) {
            UIService.showToast("Failed to load employee for editing.", "danger");
        }
    }

    async function updateEmployee() {
        const id = parseInt(document.getElementById("editEmpId").value);
        const emp = {
            firstName: document.getElementById("editFirstName").value.trim(),
            lastName: document.getElementById("editLastName").value.trim(),
            email: document.getElementById("editEmail").value.trim(),
            phone: document.getElementById("editPhone").value.trim(),
            department: document.getElementById("editDepartment").value,
            designation: document.getElementById("editDesignation").value.trim(),
            salary: parseFloat(document.getElementById("editSalary").value),
            joinDate: document.getElementById("editJoinDate").value,
            status: document.getElementById("editStatus").value
        };

        const errors = ValidationService.validateEmployee(emp);
        if (Object.keys(errors).length) {
            UIService.showFormErrors("editEmployeeForm", errors);
            return;
        }

        const res = await EmployeeService.updateEmployee(id, emp);
        if (res.ok) {
            bootstrap.Modal.getInstance(document.getElementById("editEmployeeModal"))?.hide();
            UIService.showToast("Employee updated successfully!");
            await Promise.all([loadEmployees(), loadDashboard()]);
        } else {
            const serverErrors = ValidationService.mapServerErrors(res.status, res.data);
            UIService.showFormErrors("editEmployeeForm", serverErrors);
        }
    }

    function confirmDelete(id, name) {
        _deleteTargetId = id;
        document.getElementById("deleteModalName").textContent = name;
        new bootstrap.Modal(document.getElementById("confirmDeleteModal")).show();
    }

    async function deleteEmployee() {
        if (!_deleteTargetId) return;
        const success = await EmployeeService.deleteEmployee(_deleteTargetId);
        bootstrap.Modal.getInstance(document.getElementById("confirmDeleteModal"))?.hide();
        _deleteTargetId = null;
        if (success) {
            UIService.showToast("Employee deleted.");
            _state.page = 1;
            await Promise.all([loadEmployees(), loadDashboard()]);
        } else {
            UIService.showToast("Failed to delete employee.", "danger");
        }
    }

    document.addEventListener("DOMContentLoaded", () => {

        document.getElementById("loginForm")?.addEventListener("submit", e => {
            e.preventDefault(); login();
        });

        document.getElementById("signupForm")?.addEventListener("submit", e => {
            e.preventDefault(); signup();
        });

        document.getElementById("goToSignup")?.addEventListener("click", e => {
            e.preventDefault(); showView("signupView");
        });

        document.getElementById("goToLogin")?.addEventListener("click", e => {
            e.preventDefault(); showView("loginView");
        });

        document.getElementById("closeAddEmployee")?.addEventListener("click", closeAddModal);
        document.getElementById("cancelAddEmployee")?.addEventListener("click", closeAddModal);

        document.getElementById("addEmployeeForm")?.addEventListener("submit", e => {
            e.preventDefault(); addEmployee();
        });

        document.getElementById("closeViewModal")?.addEventListener("click", () => {
            document.getElementById("viewModalOverlay").style.display = "none";
        });
        document.getElementById("closeViewBtn")?.addEventListener("click", () => {
            document.getElementById("viewModalOverlay").style.display = "none";
        });

        document.getElementById("updateEmployeeBtn")?.addEventListener("click", updateEmployee);
        document.getElementById("confirmDeleteBtn")?.addEventListener("click", deleteEmployee);

        document.getElementById("employeeSearch")?.addEventListener("input", e => {
            onSearch(e.target.value);
        });

        document.getElementById("deptFilter")?.addEventListener("change", e => {
            onDeptFilter(e.target.value);
        });

        document.querySelectorAll(".status-btn").forEach(btn => {
            btn.addEventListener("click", () => onStatusFilter(btn.dataset.status));
        });

        document.querySelectorAll(".sort-header").forEach(th => {
            th.addEventListener("click", () => onSort(th.dataset.column));
        });
    });

    return {
        init,
        changePage,
        viewEmployee,
        editEmployee,
        confirmDelete
    };

})();