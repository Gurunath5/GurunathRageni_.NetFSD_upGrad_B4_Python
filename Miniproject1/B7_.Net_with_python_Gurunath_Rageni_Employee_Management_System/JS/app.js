$(document).ready(function () {

    $("#mainnav").hide();

    $("#goToSignup").click(function(e){
        e.preventDefault();
        $("#loginView").hide();
        $("#signupView").show();
    });

    $("#goToLogin").click(function(e){
        e.preventDefault();
        $("#signupView").hide();
        $("#loginView").show();
    });

    $("#loginForm").submit(function(e){

        e.preventDefault();

        const username = $("#loginUsername").val().trim();
        const password = $("#loginPassword").val().trim();

        const error = validateLogin(username, password);
        if(error){
            $("#loginError").text(error);
            return;
        }

        if(login(username, password)){

            $("#loginView").hide();
            $("#dashboardView").show();

            $("#loggedInUser").html(
            `<i class="bi bi-person-circle"></i> ${currentUser.username}`);


            updateDashboard();
            updateDepartmentBreakdown();
            renderRecentEmployees();

            document.body.classList.remove("login-bg");
            document.body.classList.add("dashboard-bg");

            $("#mainnav").show();

        }else{
            $("#loginError").text("Invalid username or password");
        }
    });

    $("#signupForm").submit(function(e){

        e.preventDefault();

        const username = $("#signupUsername").val().trim();
        const password = $("#signupPassword").val().trim();
        const confirm = $("#signupConfirmPassword").val().trim();

        const error = validateSignup(username, password, confirm);
        if(error){
            $("#signupError").text(error);
            return;
        }

            if(signup(username, password)){
                const toastEl = document.getElementById("signupToast");

                //Manually set the text for Signup
                $("#toastMessage").text("Signup successful! Please login.");

                const toast = new bootstrap.Toast(toastEl, {
                    delay: 1500
                });
                toast.show();
                

            setTimeout(() => {

                $("#signupView").hide();
                $("#loginView").show();

                $("#signupUsername").val("");
                $("#signupPassword").val("");
                $("#signupConfirmPassword").val("");

            }, 1600);

        }else{
            $("#signupError").text("Username already exists");
        }
    });
        // Dashboard click
    $("#navDashboard").click(function(e){
        e.preventDefault();

        $("#employeesView").hide();
        $("#dashboardView").show();

        // active highlight
        //$("#navDashboard").addClass("active");
        //$("#navEmployees").removeClass("active");
    });

    // Employees click
    $("#navEmployees").click(function(e){
        e.preventDefault();

        $("#dashboardView").hide();
        $("#employeesView").show();

        renderEmployeesTable(); //  load table

        // active highlight
        //$("#navEmployees").addClass("active");
        //$("#navDashboard").removeClass("active");
    });





    $("#logout").click(function(e){
        e.preventDefault();

        $("#dashboardView").hide();
        $("#employeesView").hide();
        $("#mainnav").hide();
        $("#loginView").show();
        document.body.classList.remove("dashboard-bg");
        document.body.classList.add("login-bg");

    // clear error + form
        $("#loginError").text("");
        $("#loginForm")[0].reset();
    });
    // Open Modal
    $(document).on("click", "#add_emp, .btn-primary:contains('Add Employee')", function(e) {
        e.preventDefault();
        $("#modalOverlay").fadeIn(200); 
    });

    // Close Modal (via X button or Cancel)
    $("#closeAddEmployee, #cancelAddEmployee").click(function() {
        $("#modalOverlay").fadeOut(200);
    });

    // Close Modal when clicking outside the white box
    $("#modalOverlay").click(function(e) {
        if (e.target.id === "modalOverlay") {
            $(this).fadeOut(200);
        }
    });

    // Prevent modal close when clicking inside the form
    $(".custom-modal").click(function(e) {
        e.stopPropagation();
    }); 


    // 1. Open Modal
$(document).on("click", "#add_emp, .btn-primary:contains('Add Employee')", function(e) {
    e.preventDefault();
    $("#modalOverlay").fadeIn(200); 
});

    // --- VIEW EMPLOYEE LOGIC ---
    $(document).on("click", ".view-btn", function() {
        const empId = $(this).data("id");
        // Using existing service to get data
        const emp = employeeService.getEmployeeById(empId); 

        if (emp) {
            // Populate the View Modal
            const initials = (emp.fName[0] + emp.lName[0]).toUpperCase();
            $("#viewAvatar").text(initials);
            $("#viewFullName").text(`${emp.fName} ${emp.lName}`);
            $("#viewDeptBadge").text(emp.dept);
            $("#viewEmail").text(emp.email);
            $("#viewPhone").text(emp.phno);
            $("#viewDesignation").text(emp.desig);
            $("#viewSalary").text(`₹${Number(emp.salary).toLocaleString()}`);
            $("#viewJoinDate").text(emp.date);
            
            const statusClass = emp.status === "Active" ? "bg-success" : "bg-danger";
            $("#viewStatus").html(`<span class="badge ${statusClass}">${emp.status}</span>`);

            $("#viewModalOverlay").fadeIn(200);
        }
        
    // Close buttons for both modals
    //$("#closeViewModal, #closeViewBtn").click(() => $("#viewModalOverlay").fadeOut(200));

    });
    
    // Close buttons for both modals
    $("#closeViewModal, #closeViewBtn").click(() => $("#viewModalOverlay").fadeOut(200));


    // --- ADD EMPLOYEE LOGIC (FIXED) ---
    $("#addEmployeeForm").off("submit").on("submit", function (e) {
    e.preventDefault();

    const formData = {
        fName: $("#firstName").val().trim(),
        lName: $("#lastName").val().trim(),
        email: $("#email").val().trim(),
        phno: $("#phno").val().trim(),
        dept: $("#department").val(),
        desig: $("#designation").val().trim(),
        salary: $("#salary").val(),
        date: $("#joinDate").val(),
        status: $("#status").val()
    };

    //  CLEAR OLD ERRORS
    $(".error-msg").remove();
    $(".is-invalid").removeClass("is-invalid");

    const errors = validateEmployeeForm(formData);

    //  SHOW ERRORS
    if (Object.keys(errors).length > 0) {

        const fieldMap = {
            editFirstName: "#firstName",
            editLastName: "#lastName",
            editEmail: "#email",
            editPhone: "#phno",
            editDepartment: "#department",
            editDesignation: "#designation",
            editSalary: "#salary",
            editStatus: "#status"
        };

        for (let key in errors) {
            const selector = fieldMap[key];
            if (selector) {
                $(selector)
                    .addClass("is-invalid")
                    .after(`<div class="text-danger small error-msg">${errors[key]}</div>`);
            }
        }

        return; //  STOP if errors
    }

    //  SUCCESS
    const success = employeeService.createNewEmployee(formData);

    if (success) {
        renderEmployeesTable();
        updateDashboard();

        $("#modalOverlay").fadeOut(200);
        this.reset();

        $("#toastMessage").text("Employee added successfully!");
        bootstrap.Toast.getOrCreateInstance(document.getElementById('signupToast')).show();
    }
});
});



// --- EDIT EMPLOYEE LOGIC ---
$(document).on("click", ".edit-btn", function() {
    const empId = $(this).data("id");
    const emp = employees.find(e => e.id == empId);

    if (emp) {
        // Split name (e.g., "Pooja Ghosh" -> "Pooja" and "Ghosh")
        const nameParts = emp.name ? emp.name.split(" ") : ["", ""];
        const formattedDate = convertDateToInputFormat(emp.joinDate);
        $("#editJoinDate").val(formattedDate);        
        // Fill the Edit Modal Fields
        $("#editEmpId").val(emp.id);
        $("#editFirstName").val(nameParts[0]);
        $("#editLastName").val(nameParts.slice(1).join(" "));
        $("#editEmail").val(emp.email);
        $("#editPhone").val(emp.phno || "");
        $("#editDepartment").val(emp.department);
        $("#editDesignation").val(emp.designation);
        $("#editSalary").val(emp.salary);
        
        // Show the modal (Using Bootstrap 5 Modal)
        const editModal = new bootstrap.Modal(document.getElementById('editEmployeeModal'));
        editModal.show();
 
    }
});

 




















function convertDateToInputFormat(dateString) {
    if (!dateString) return "";
    
    const date = new Date(dateString);
    // If the date is invalid, return empty
    if (isNaN(date.getTime())) return "";

    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-indexed
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
}









$(document).on("click", "#updateEmployeeBtn", function(e) {
    const currentId = $("#editEmpId").val();
    
    // 1. Collect Data in the same format the validator expects
    const formData = {
        fName: $("#editFirstName").val().trim(),
        lName: $("#editLastName").val().trim(),
        email: $("#editEmail").val().trim(),
        phno: $("#editPhone").val().trim(),
        dept: $("#editDepartment").val(),
        desig: $("#editDesignation").val().trim(),
        salary: $("#editSalary").val(),
        status: $("#editStatus").val()
    };

    // 2. Clear Previous Warnings
    $(".error-msg").remove();
    $(".is-invalid").removeClass("is-invalid");

    // 3. Run Validation
    const errors = validateEmployeeForm(formData, currentId);

    // 4. If there are errors, display them and STOP
    if (Object.keys(errors).length > 0) {
        for (let key in errors) {
            $(`#${key}`).addClass("is-invalid")
                .after(`<div class="text-danger small error-msg">${errors[key]}</div>`);
        }
        return; // This stops the update!
    }

    // 5. If no errors, update the array
    const index = employees.findIndex(emp => emp.id == currentId);
    if (index !== -1) {
        employees[index] = {
            ...employees[index],
            name: `${formData.fName} ${formData.lName}`.trim(),
            email: formData.email,
            phno: formData.phno,
            department: formData.dept,
            designation: formData.desig,
            salary: parseInt(formData.salary),
            status: formData.status
        };

        renderEmployeesTable();
        updateDashboard();
        updateDepartmentBreakdown();
        renderRecentEmployees();
        if (typeof updateDashboard === "function") updateDashboard();

        // Close Modal
        const modalEl = document.getElementById('editEmployeeModal');
        const modalInstance = bootstrap.Modal.getInstance(modalEl) || new bootstrap.Modal(modalEl);
        modalInstance.hide();

        // Success Feedback
        $("#toastMessage").text("Employee updated successfully!");
        bootstrap.Toast.getOrCreateInstance(document.getElementById('signupToast')).show();
    }
});














let employeeIdToDelete = null; // Temporary variable to store the ID

$(document).on("click", ".delete-btn", function() {
    employeeIdToDelete = $(this).data("id"); // Get ID from the trash icon
    const emp = employees.find(e => e.id == employeeIdToDelete);

    if (emp) {
        // Update the name in your Modal text dynamically
        $("#deleteModalName").text(emp.name); 
        
        // Show your custom "Confirm Delete" Modal
        const deleteModal = new bootstrap.Modal(document.getElementById('confirmDeleteModal'));
        deleteModal.show();
    }
});
$(document).on("click", "#confirmDeleteBtn", function() {
    if (employeeIdToDelete !== null) {
        // 1. Remove from the array
        employees = employees.filter(emp => emp.id != employeeIdToDelete);

        // 2. Refresh the UI immediately
        renderEmployeesTable(); 
        updateDashboard();
        updateDepartmentBreakdown();
        renderRecentEmployees();

        // 3. Close the Modal
        const modalEl = document.getElementById('confirmDeleteModal');
        const modalInstance = bootstrap.Modal.getInstance(modalEl);
        if (modalInstance) modalInstance.hide();

        // 4. Show success toast
        $("#toastMessage").text("Employee removed from records.");
        bootstrap.Toast.getOrCreateInstance(document.getElementById('signupToast')).show();

        // Reset the ID tracker
        employeeIdToDelete = null;
    }
});

















