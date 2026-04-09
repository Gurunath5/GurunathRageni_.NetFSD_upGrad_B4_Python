

// Import functions


const {
    validateLogin,
    validateSignup,
    signup,
    login,
    getCurrentUser
} = require('../js/authService');

// Mock users
global.users = [];
global.currentUser = null;

describe("Auth Service Tests", () => {

    beforeEach(() => {
        users.length = 0;
        currentUser = null;
    });

    //  Signup Validation
    test("should fail if fields are empty", () => {
        expect(validateSignup("", "", "")).toBeTruthy();
    });

    test("should fail if passwords do not match", () => {
        expect(validateSignup("user", "123456", "123")).toBe("Passwords do not match");
    });

    test("should fail for short password", () => {
        expect(validateSignup("user", "123", "123")).toBeTruthy();
    });

    test("should signup successfully", () => {
        const result = signup("john", "123456");
        expect(result).toBe(true);
        expect(users.length).toBe(1);
    });

    test("should not allow duplicate username", () => {
        signup("john", "123456");
        const result = signup("john", "123456");
        expect(result).toBe(false);
    });

    //  Login Tests
    test("should login successfully", () => {
        signup("john", "123456");
        const result = login("john", "123456");

        expect(result).toBe(true);
        //expect(currentUser.username).toBe("john");
        expect(getCurrentUser().username).toBe("john");
    });

    test("should fail login with wrong password", () => {
        signup("john", "123456");
        const result = login("john", "wrong");

        expect(result).toBe(false);
    });

    test("should validate login fields", () => {
        expect(validateLogin("", "")).toBeTruthy();
    });

});