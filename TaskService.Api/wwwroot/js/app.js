const apiUrl = "/tasks";

async function loadTasks() {
  const response = await fetch(apiUrl);
  const tasks = await response.json();

  const list = document.getElementById("taskList");
  list.innerHTML = "";

  for (const task of tasks) {
    const li = document.createElement("li");

    li.innerHTML = `
      <span style="text-decoration:${task.isCompleted ? "line-through" : "none"}">
        ${task.title}
      </span>

      <button onclick="toggleDone(${task.id})">
        ${task.isCompleted ? "Undo" : "Done"}
      </button>
    `;

    list.appendChild(li);
  }
}

async function addTask() {
  const input = document.getElementById("taskInput");

  await fetch(apiUrl, {
    method: "POST",
    headers: {
      "Content-type": "application/json",
    },
    body: JSON.stringify({
      title: input.value,
    }),
  });

  input.value = "";
  loadTasks();
}

async function toggleDone(id) {
  await fetch(`/tasks/${id}/complete`, {
    method: "PATCH",
  });

  loadTasks();
}
loadTasks();
