function validateLogin(username, password) {
    if (!username || !password) return "All fields required";
    return "";
}
function validateSignup(username, password, confirm) {
    if (!username || !password || !confirm) return "All fields required";

    if (password.length < 6) return "Password must be at least 6 characters";

    if (password !== confirm) return "Passwords do not match";

    return "";
}

//function login(username, password) {
    //return users.some(u => u.username === username && u.password === password);
//}

/*function validateSignup(username, password, confirm) {
    if (!username || !password || !confirm) return "All fields required";
    if (password !== confirm) return "Passwords do not match";
    return "";
}*/

function signup(username, password) {
    let exists = users.some(u => u.username === username);
    if (exists) return false;

    users.push({ username, password });
    return true;
}




let currentUser = null;
function login(username, password) {

    const user = users.find(u => u.username === username && u.password === password);

    if(user){
        currentUser = user; // store logged in user
        return true;
    }

    return false;
}


module.exports = {
    validateLogin,
    validateSignup,
    signup,
    login,
    getCurrentUser  
};


function getCurrentUser() {
    return currentUser;
}