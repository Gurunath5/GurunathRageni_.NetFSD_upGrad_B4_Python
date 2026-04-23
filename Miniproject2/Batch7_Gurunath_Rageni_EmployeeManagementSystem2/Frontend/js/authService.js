let _session = null;

const AuthService = (() => {

    async function login(credentials) {
        const res = await fetch(`${CONFIG.API_BASE_URL}/auth/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(credentials)
        });

        const result = await res.json();

        if (result.success) {
            _session = result;
        }

        return result;
    }

    function logout() {
        _session = null;
    }

    function getToken() {
        return _session?.token;
    }

    function getRole() {
        return _session?.role;
    }

    function isAdmin() {
        return _session?.role === "Admin";
    }

    return {
        login,
        logout,
        getToken,
        getRole,
        isAdmin
    };

})();