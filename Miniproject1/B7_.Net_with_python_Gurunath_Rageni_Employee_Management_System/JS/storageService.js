const storageService = {
    // Returns the current state of the global employees array
    getAllEmployees: () => {
        return employees; 
    },

    // Adds a new employee to the global array
    saveEmployee: (newEmp) => {
        employees.push(newEmp);
        return true;
    }
};