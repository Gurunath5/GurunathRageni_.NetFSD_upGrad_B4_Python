const EmployeeService = (() => {

    async function getEmployees(params) {
        return await StorageService.getAll(params);
    }

    async function getEmployee(id) {
        return await StorageService.getById(id);
    }

    async function createEmployee(emp) {
        return await StorageService.add(emp);
    }

    async function updateEmployee(id, emp) {
        return await StorageService.update(id, emp);
    }

    async function deleteEmployee(id) {
        return await StorageService.remove(id);
    }

    return { getEmployees, getEmployee, createEmployee, updateEmployee, deleteEmployee };

})();