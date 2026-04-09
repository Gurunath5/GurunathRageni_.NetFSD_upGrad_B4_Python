function validateLogin(username, password){

    if(username === "" || password === ""){
        return "Please enter all fields";
    }

    return "";
}


function validateSignup(username, password, confirm){

    if(username === "" || password === "" || confirm === ""){
        return "Please enter all fields";
    }

    // Password length check
    if(password.length < 6){
        return "Password must be at least 6 characters";
    }

    // Confirm password match
    if(password !== confirm){
        return "Passwords do not match";
    }

    return "";
}










function validateEmployeeForm(data, empId = null) { // Added empId parameter
    let errors = {};
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!data.fName) errors.editFirstName = "First name is required"; // Use Edit IDs
    if (!data.lName) errors.editLastName = "Last name is required";
    if (!data.dept) errors.editDepartment = "Select a department";
    if (!data.status) errors.editStatus = "Select a status";
    if (!data.desig) errors.editDesignation = "Designation is required";

    // EMAIL VALIDATION
    if (!emailRegex.test(data.email)) {
        errors.editEmail = "Invalid email format";
    } else {
        // IGNORE current user's ID during check
        const emailExists = employees.some(emp => 
            emp.email.toLowerCase() === data.email.toLowerCase() && emp.id != empId
        );
        if (emailExists) errors.editEmail = "This email is already registered";
    }

    // PHONE VALIDATION
    if (!/^\d{10}$/.test(data.phno)) {
        errors.editPhone = "Phone must be exactly 10 digits";
    } else {
        // IGNORE current user's ID during check
        const phnoExists = employees.some(emp => emp.phno === data.phno && emp.id != empId);
        if (phnoExists) errors.editPhone = "This phone number is already in use";
    }

    if (!data.salary || parseInt(data.salary) <= 0) {
        errors.editSalary = "Salary must be a positive number";
    }

    return errors;
}
