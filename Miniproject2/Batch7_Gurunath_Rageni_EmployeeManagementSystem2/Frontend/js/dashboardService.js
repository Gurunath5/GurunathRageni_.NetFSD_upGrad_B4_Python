const DashboardService = (() => {

    async function getSummary() {
        return await StorageService.getDashboard();
    }

    return { getSummary };

})();