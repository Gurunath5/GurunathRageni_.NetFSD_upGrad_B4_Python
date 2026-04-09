
const employeeService = {
    generateNextId: () => {
        if (employees.length === 0) return 1;
        // Finds the highest existing ID and adds 1
        return Math.max(...employees.map(emp => emp.id)) + 1;
    },

    createNewEmployee: (formData) => {
        const nextId = employeeService.generateNextId();
        
        // Format the date to "12 Mar 2026"
        const dateObj = new Date(formData.date);
        const formattedDate = dateObj.toLocaleDateString('en-GB', {
            day: '2-digit',
            month: 'short',
            year: 'numeric'
        });

        const newEmployee = {
            id: nextId,
            name: `${formData.fName} ${formData.lName}`.trim(),
            email: formData.email,
            phno: formData.phno,
            department: formData.dept,
            designation: formData.desig,
            salary: parseInt(formData.salary),
            status: formData.status,
            joinDate: formattedDate
        };

        return storageService.saveEmployee(newEmployee);
    }
};





module.exports = employeeService;