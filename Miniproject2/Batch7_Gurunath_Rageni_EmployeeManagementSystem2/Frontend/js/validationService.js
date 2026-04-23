const ValidationService = (() => {

    function validateEmployee(emp) {
        const errors = {};
        if (!emp.firstName?.trim()) errors.firstName = "First name is required.";
        if (!emp.lastName?.trim()) errors.lastName = "Last name is required.";
        if (!emp.email?.trim()) errors.email = "Email is required.";
        else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emp.email))
            errors.email = "Enter a valid email address.";
        if (!emp.phone?.trim()) errors.phone = "Phone is required.";
        else if (!/^\d{10}$/.test(emp.phone.trim()))
            errors.phone = "Phone must be 10 digits.";
        if (!emp.department) errors.department = "Department is required.";
        if (!emp.designation?.trim()) errors.designation = "Designation is required.";
        if (!emp.salary || emp.salary <= 0) errors.salary = "Salary must be a positive number.";
        if (!emp.joinDate) errors.joinDate = "Join date is required.";
        if (!emp.status) errors.status = "Status is required.";
        return errors;
    }

    // Translates API error responses into field-level messages
    function mapServerErrors(status, responseData) {
        const errors = {};
        if (status === 409) {
            errors.email = "An employee with this email already exists.";
        } else if (status === 400 && responseData?.errors) {
            Object.entries(responseData.errors).forEach(([field, msgs]) => {
                errors[field.toLowerCase()] = Array.isArray(msgs) ? msgs[0] : msgs;
            });
        }
        return errors;
    }

    return { validateEmployee, mapServerErrors };

})();