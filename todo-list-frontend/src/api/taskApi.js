const API_URL = "https://localhost:7016/api/task";

export const getAllTasks = async () => {
  const res = await fetch(API_URL);
  if (!res.ok) throw new Error("Error get all task");
  return await res.json();
};

export const deleteTask = async (id) => {
  const res = await fetch(`${API_URL}/${id}`, {
    method: "DELETE",
  });
  if (!res.ok) throw new Error("Error deleting task");
  return await res.json();
};

export const updateTask = async (id, taskData) => {
  const res = await fetch(`${API_URL}/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(taskData),
  });
  if (!res.ok) throw new Error("Error updating task");
  return await res.json();
};

export const createTask = async (taskData) => {
  const res = await fetch(`${API_URL}`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(taskData),
  });
  if (!res.ok) throw new Error("Failed to create task");
  return await res.json();
};

