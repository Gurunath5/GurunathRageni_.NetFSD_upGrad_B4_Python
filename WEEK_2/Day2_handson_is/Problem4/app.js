import {
    addTaskCallback,
    deleteTaskCallback,
    listTasksCallback,
    addTaskPromise,
    deleteTaskPromise,
    listTasksPromise,
    addTaskAsync,
    deleteTaskAsync,
    listTasksAsync
} from "./taskmanager.js";


/* =========================================
   1️⃣ CALLBACK USAGE
========================================= */

addTaskCallback("Learn JS", (msg) => {
    console.log(msg);

    listTasksCallback((tasks) => {
        console.log(`Tasks: ${tasks.join(", ")}`);
    });
});

/* =========================================
   2️⃣ PROMISE USAGE
======================================= */
addTaskPromise("Practice DSA")
    .then(msg => {
        console.log(msg);
        return listTasksPromise();
    })
    .then(tasks => {
        console.log(`Tasks: ${tasks.join(", ")}`);
    });

/* =========================================
   3️⃣ ASYNC / AWAIT USAGE
========================================= */
const runAsyncVersion = async () => {
    const msg1 = await addTaskAsync("Build Projects");
    console.log(msg1);

    const msg2 = await deleteTaskAsync("Learn JS");
    console.log(msg2);
    const tasks = await listTasksAsync();
    console.log(`Final Tasks: ${tasks.join(", ")}`);
};
runAsyncVersion();