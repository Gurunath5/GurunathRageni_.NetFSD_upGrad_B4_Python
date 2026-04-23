const StorageService = (() => {

    function _headers(withAuth = true) {
        const headers = { "Content-Type": "application/json" };
        if (withAuth) {
            const token = AuthService.getToken?.();
            if (token) headers["Authorization"] = `Bearer ${token}`;
        }
        return headers;
    }

    async function getAll(params = {}) {
        // Remove empty/null params before building query string
        const clean = Object.fromEntries(
            Object.entries(params).filter(([, v]) => v !== null && v !== "" && v !== undefined)
        );
        const query = new URLSearchParams(clean).toString();
        const res = await fetch(`${CONFIG.API_BASE_URL}/employees?${query}`, { headers: _headers() });
        if (!res.ok) throw new Error(`getAll failed: ${res.status}`);
        return res.json();
    }

    async function getById(id) {
        const res = await fetch(`${CONFIG.API_BASE_URL}/employees/${id}`, { headers: _headers() });
        if (!res.ok) throw new Error(`getById failed: ${res.status}`);
        return res.json();
    }

    async function add(emp) {
        const res = await fetch(`${CONFIG.API_BASE_URL}/employees`, {
            method: "POST",
            headers: _headers(),
            body: JSON.stringify(emp)
        });
        return { ok: res.ok, status: res.status, data: await res.json() };
    }

    async function update(id, emp) {
        const res = await fetch(`${CONFIG.API_BASE_URL}/employees/${id}`, {
            method: "PUT",
            headers: _headers(),
            body: JSON.stringify(emp)
        });
        return { ok: res.ok, status: res.status, data: await res.json() };
    }

    async function remove(id) {
        const res = await fetch(`${CONFIG.API_BASE_URL}/employees/${id}`, {
            method: "DELETE",
            headers: _headers()
        });
        return res.ok;
    }

    async function login(data) {
        const res = await fetch(`${CONFIG.API_BASE_URL}/auth/login`, {
            method: "POST",
            headers: _headers(false),
            body: JSON.stringify(data)
        });
        return res.json();
    }

    async function register(data) {
        const res = await fetch(`${CONFIG.API_BASE_URL}/auth/register`, {
            method: "POST",
            headers: _headers(false),
            body: JSON.stringify(data)
        });
        return { ok: res.ok, status: res.status, data: await res.json() };
    }

    async function getDashboard() {
        const res = await fetch(`${CONFIG.API_BASE_URL}/employees/dashboard`, { headers: _headers() });
        if (!res.ok) throw new Error(`getDashboard failed: ${res.status}`);
        return res.json();
    }

    return { getAll, getById, add, update, remove, login, register, getDashboard };

})();