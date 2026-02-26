// Select Elements
const taskInput = document.getElementById("taskInput");
const addBtn = document.getElementById("addBtn");
const taskList = document.getElementById("taskList");


// Add Task
function addTask() {
    const taskText = taskInput.value.trim();

    if (taskText === "") {
        alert("Please enter a task!");
        return;
    }

    const li = document.createElement("li");

    li.innerHTML = `
        <span class="task-text">${taskText}</span>
        <div class="btn-group">
            <button class="complete-btn">Done</button>
            <button class="delete-btn">Delete</button>
        </div>
    `;

    taskList.appendChild(li);
    taskInput.value = "";
}


// Event Delegation (FIXED)
function handleTaskActions(e) {

    const deleteBtn = e.target.closest(".delete-btn");
    const completeBtn = e.target.closest(".complete-btn");

    // Delete
    if (deleteBtn) {
        deleteBtn.closest("li").remove();
    }

    // Complete
    if (completeBtn) {
        completeBtn.closest("li").classList.toggle("completed");
    }
}


// Event Listeners
addBtn.addEventListener("click", addTask);
taskList.addEventListener("click", handleTaskActions);