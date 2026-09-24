const apiUrl = "/tasks";

async function loadTasks() {
  const token = localStorage.getItem("token");

  const response = await fetch(apiUrl, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    console.error("Kunne ikke hente tasks");
    return;
  }

  const tasks = await response.json();
  const list = document.getElementById("taskList");

  list.innerHTML = "";

  tasks.forEach((task) => {
    const li = document.createElement("li");
    const span = document.createElement("span");

    span.textContent = task.title;

    if (task.isCompleted) {
      span.classList.add("completed");
    }
    const completeButton = document.createElement("button");
    completeButton.textContent = task.isCompleted ? "Undo" : "Done";
    completeButton.classList.add("complete-button");
    completeButton.addEventListener("click", () => {
      toggleDone(task.id);
    });

    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Delete";
    deleteButton.classList.add("delete-button");
    deleteButton.addEventListener("click", () => {
      deleteTask(task.id);
    });

    li.appendChild(span);
    li.appendChild(completeButton);
    li.appendChild(deleteButton);

    list.appendChild(li);
  });
}

async function addTask() {
  const input = document.getElementById("taskInput");

  if (!input.value.trim()) {
    return;
  }

  const token = localStorage.getItem("token");
  const response = await fetch(apiUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",

      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({
      title: input.value,
    }),
  });

  if (!response.ok) {
    console.error("Kunne ikke opprette en oppgave");
    return;
  }

  input.value = "";
  await loadTasks();
}

async function toggleDone(id) {
  const token = localStorage.getItem("token");

  // /tasks/{id}/complete
  const response = await fetch(`${apiUrl}/${id}/complete`, {
    method: "PATCH",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    console.error("Kunne ikke endre oppgaven");
    return;
  }

  await loadTasks();
}

async function deleteTask(id) {
  const token = localStorage.getItem("token");

  // /tasks/{id}
  const response = await fetch(`${apiUrl}/${id}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    console.error("Kunne ikke slette oppgaven");
    return;
  }

  await loadTasks();
}

const themeButton = document.getElementById("themeButton");

themeButton.addEventListener("click", () => {
  document.body.classList.toggle("dark-mode");

  const darkModeEnabled = document.body.classList.contains("dark-mode");

  if (darkModeEnabled) {
    themeButton.textContent = "Light mode";
  } else {
    themeButton.textContent = "Dark mode";
  }
});

loadTasks();
