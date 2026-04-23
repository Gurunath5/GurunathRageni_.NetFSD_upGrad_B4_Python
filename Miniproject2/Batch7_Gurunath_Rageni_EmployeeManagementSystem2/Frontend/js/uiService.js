const UIService = (() => {

    const DEPT_COLORS = {
        Engineering: { badge: "eng", bar: "eng" },
        Marketing: { badge: "mkt", bar: "mkt" },
        HR: { badge: "hr", bar: "hr" },
        Finance: { badge: "fin", bar: "fin" },
        Operations: { badge: "ops", bar: "ops" }
    };

    // ── Employee table ──────────────────────────────────────────────────────────
    function renderEmployees(result) {
        const tbody = document.getElementById("employeeTableBody");
        if (!result?.data?.length) {
            tbody.innerHTML = `<tr><td colspan="10" class="text-center text-muted py-4">No employees found.</td></tr>`;
            _updateCount(0, 0, 0);
            renderPagination(result);
            return;
        }

        tbody.innerHTML = result.data.map((emp, i) => {
            const initials = (emp.firstName[0] + emp.lastName[0]).toUpperCase();
            const colors = ["#0d6efd", "#198754", "#dc3545", "#fd7e14", "#6f42c1"];
            const bg = colors[emp.id % colors.length];
            const isAdmin = AuthService.isAdmin();

            return `
            <tr>
              <td class="text-muted small">#${emp.id}</td>
              <td>
                <div class="avatar d-inline-flex align-items-center justify-content-center text-white fw-bold"
                     style="width:35px;height:35px;border-radius:50%;background:${bg};font-size:12px">
                  ${initials}
                </div>
              </td>
              <td class="fw-semibold">${emp.name}</td>
              <td class="text-muted small">${emp.email}</td>
              <td><span class="dept-badge ${DEPT_COLORS[emp.department]?.badge || 'ops'}">${emp.department}</span></td>
              <td class="small">${emp.designation}</td>
              <td>₹${Number(emp.salary).toLocaleString("en-IN")}</td>
              <td class="small">${emp.joinDate}</td>
              <td><span class="badge-${emp.status.toLowerCase()}">${emp.status}</span></td>
              <td>
                <button class="btn btn-sm btn-outline-primary me-1" onclick="App.viewEmployee(${emp.id})" title="View">
                  <i class="bi bi-eye"></i>
                </button>
                ${isAdmin ? `
                <button class="btn btn-sm btn-outline-warning me-1" onclick="App.editEmployee(${emp.id})" title="Edit">
                  <i class="bi bi-pencil"></i>
                </button>
                <button class="btn btn-sm btn-outline-danger" onclick="App.confirmDelete(${emp.id}, '${emp.name}')" title="Delete">
                  <i class="bi bi-trash3"></i>
                </button>` : ""}
              </td>
            </tr>`;
        }).join("");

        const start = (result.page - 1) * result.pageSize + 1;
        const end = Math.min(result.page * result.pageSize, result.totalCount);
        _updateCount(start, end, result.totalCount);
        renderPagination(result);
    }

    function _updateCount(start, end, total) {
        const el = document.getElementById("recordCount");
        if (el) el.textContent = total > 0
            ? `Showing ${start}–${end} of ${total} employees`
            : "No employees found";
    }

    // ── Pagination ──────────────────────────────────────────────────────────────
    function renderPagination(result) {
        const container = document.getElementById("paginationContainer");
        if (!container) return;
        if (!result || result.totalPages <= 1) { container.innerHTML = ""; return; }

        let pages = "";
        for (let i = 1; i <= result.totalPages; i++) {
            const active = i === result.page ? "active" : "";
            pages += `<li class="page-item ${active}">
                <button class="page-link" onclick="App.changePage(${i})">${i}</button>
              </li>`;
        }

        container.innerHTML = `
          <nav><ul class="pagination pagination-sm mb-0">
            <li class="page-item ${!result.hasPrevPage ? 'disabled' : ''}">
              <button class="page-link" onclick="App.changePage(${result.page - 1})">
                <i class="bi bi-chevron-left"></i>
              </button>
            </li>
            ${pages}
            <li class="page-item ${!result.hasNextPage ? 'disabled' : ''}">
              <button class="page-link" onclick="App.changePage(${result.page + 1})">
                <i class="bi bi-chevron-right"></i>
              </button>
            </li>
          </ul></nav>`;
    }

    // ── Dashboard ───────────────────────────────────────────────────────────────
    function renderDashboard(data) {
        document.getElementById("totalEmployees").textContent = data.total;
        document.getElementById("activeEmployees").textContent = data.active;
        document.getElementById("inactiveEmployees").textContent = data.inactive;
        document.getElementById("departments").textContent = data.departments;

        // Department breakdown
        const deptList = document.getElementById("departmentList");
        deptList.innerHTML = data.breakdown.map(d => {
            const cls = DEPT_COLORS[d.name]?.badge || "ops";
            return `
            <div class="department-row">
              <span class="dept-badge ${cls}">${d.name}</span>
              <span class="fw-semibold">${d.count}</span>
              <div class="bar-container"><div class="bar ${cls}" style="width:${d.percent}%"></div></div>
              <span class="text-muted small">${d.percent}%</span>
            </div>`;
        }).join("");

        // Recent employees
        const recentList = document.getElementById("recentEmployeesList");
        recentList.innerHTML = data.recent.map(emp => {
            const initials = emp.name.split(" ").map(n => n[0]).join("").toUpperCase().slice(0, 2);
            return `
            <div class="employee-row">
              <div class="emp-left">
                <div class="avatar">${initials}</div>
                <div>
                  <div class="emp-name">${emp.name}</div>
                  <div class="emp-role">${emp.designation} · ${emp.department}</div>
                </div>
              </div>
              <span class="badge-${emp.status.toLowerCase()}">${emp.status}</span>
            </div>`;
        }).join("");
    }

    // ── Role-based UI ───────────────────────────────────────────────────────────
    function applyRoleUI() {
        const isAdmin = AuthService.isAdmin();

        // Show/hide Add Employee buttons
        document.querySelectorAll("#add_emp").forEach(btn => {
            btn.style.display = isAdmin ? "" : "none";
        });

        // Show viewer notice
        const notice = document.getElementById("viewerNotice");
        if (notice) notice.style.display = isAdmin ? "none" : "";

        // Show role badge
        const badge = document.getElementById("roleBadge");
        if (badge) {
            badge.textContent = AuthService.getRole();
            badge.className = `badge ${isAdmin ? "bg-danger" : "bg-secondary"} ms-2`;
        }

        // Show username
        const userEl = document.getElementById("loggedInUser");
        if (userEl) userEl.innerHTML = `<i class="bi bi-person-circle me-1"></i>${AuthService.getUsername()}
            <span id="roleBadge" class="badge ${isAdmin ? 'bg-danger' : 'bg-secondary'} ms-1">
              ${AuthService.getRole()}
            </span>`;
    }

    // ── Toast ───────────────────────────────────────────────────────────────────
    function showToast(message, type = "success") {
        const toastEl = document.getElementById("signupToast");
        const msgEl = document.getElementById("toastMessage");
        if (!toastEl || !msgEl) return;

        toastEl.className = `toast border-0 text-bg-${type}`;
        msgEl.textContent = message;
        new bootstrap.Toast(toastEl, { delay: 3000 }).show();
    }

    // ── Inline field errors ─────────────────────────────────────────────────────
    function showFormErrors(formId, errors) {
        // Clear old errors
        document.querySelectorAll(`#${formId} .field-error`).forEach(el => el.remove());
        document.querySelectorAll(`#${formId} .is-invalid`).forEach(el => el.classList.remove("is-invalid"));

        Object.entries(errors).forEach(([field, msg]) => {
            const input = document.getElementById(
                formId === "editEmployeeForm" ? `edit${field.charAt(0).toUpperCase() + field.slice(1)}` : field
            );
            if (!input) return;
            input.classList.add("is-invalid");
            const err = document.createElement("div");
            err.className = "field-error text-danger small mt-1";
            err.textContent = msg;
            input.parentNode.appendChild(err);
        });
    }

    function clearFormErrors(formId) {
        document.querySelectorAll(`#${formId} .field-error`).forEach(el => el.remove());
        document.querySelectorAll(`#${formId} .is-invalid`).forEach(el => el.classList.remove("is-invalid"));
    }

    return {
        renderEmployees,
        renderPagination,
        renderDashboard,
        applyRoleUI,
        showToast,
        showFormErrors,
        clearFormErrors
    };

})();