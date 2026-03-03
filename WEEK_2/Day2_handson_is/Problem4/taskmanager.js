// taskManager.js

// Simulated database
let tasks = [];

/* =========================================
   1️⃣ CALLBACK VERSION
========================================= */

// Add Task (Callback)
export const addTaskCallback = (task, callback) => {
    setTimeout(() => {
        tasks.push(task);
        callback(`Task "${task}" added.`);
    }, 1000);
};

// Delete Task (Callback)
export const deleteTaskCallback = (task, callback) => {
    setTimeout(() => {
        tasks = tasks.filter(t => t !== task);
        callback(`Task "${task}" deleted.`);
    }, 1000);
};

// List Tasks (Callback)
export const listTasksCallback = (callback) => {
    setTimeout(() => {
        callback(tasks);
    }, 1000);
};


/* =========================================
   2️⃣ PROMISE VERSION
========================================= */

export const addTaskPromise = (task) => {
    return new Promise((resolve) => {
        setTimeout(() => {
            tasks.push(task);
            resolve(`Task "${task}" added.`);
        }, 1000);
    });
};

export const deleteTaskPromise = (task) => {
    return new Promise((resolve) => {
        setTimeout(() => {
            tasks = tasks.filter(t => t !== task);
            resolve(`Task "${task}" deleted.`);
        }, 1000);
    });
};

export const listTasksPromise = () => {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve(tasks);
        }, 1000);
    });
};


/* =========================================
   3️⃣ ASYNC / AWAIT VERSION
========================================= */

export const addTaskAsync = async (task) => {
    const message = await addTaskPromise(task);
    return message;
};

export const deleteTaskAsync = async (task) => {
    const message = await deleteTaskPromise(task);
    return message;
};

export const listTasksAsync = async () => {
    const allTasks = await listTasksPromise();
    return allTasks;
};