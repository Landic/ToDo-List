import React, { useEffect, useState } from "react";
import { getAllTasks, deleteTask, updateTask, createTask } from "../api/taskApi";

function TasksPage() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editTaskId, setEditTaskId] = useState(null);
  const [editFormData, setEditFormData] = useState({ name: "", description: "", deadline: "" });
  const [newTaskData, setNewTaskData] = useState({ name: "", description: "", deadline: "" });
  const [searchTerm, setSearchTerm] = useState("");
  const [error, setError] = useState(null);
  const userId = parseInt(localStorage.getItem("userId"));

  const fetchTasks = async () => {
    const userId = parseInt(localStorage.getItem("userId"));
    if (!userId) {
      setError("User ID not found. Please log in again.");
      return;
    }

    try {
      setLoading(true);
      const data = await getAllTasks();
      if (data.success) {
        const userTasks = data.data.filter(task => task.userId === userId);
        setTasks(userTasks);
      } else {
        setError(data.message || "Failed to load tasks");
      }
    } catch (e) {
      setError(e.message || "An error occurred while loading tasks");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  const handleNewTaskChange = (e) => {
    const { name, value } = e.target;
    setNewTaskData((prev) => ({ ...prev, [name]: value }));
  };

  const handleCreateTask = async () => {
    if (!newTaskData.name.trim()) {
      alert("Task name is required");
      return;
    }

    const userId = parseInt(localStorage.getItem("userId")); 

    if (!userId) {
      alert("User ID not found. Please log in again.");
      return;
    }

    const taskToSend = {
      ...newTaskData,
      deadline: newTaskData.deadline ? new Date(newTaskData.deadline).toISOString() : null,
      userId,
    };

    try {
      const data = await createTask(taskToSend);
      if (data.success) {
        setTasks(prev => [...prev, data.data]);
        setNewTaskData({ name: "", description: "", deadline: "" });
      } else {
        alert(data.message || "Failed to create task");
      }
    } catch (e) {
      alert(e.message || "An error occurred while creating the task");
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm("Are you sure you want to delete this task?")) return;
    try {
      const data = await deleteTask(id);
      if (data.success) {
        setTasks((prev) => prev.filter((task) => task.id !== id));
      } else {
        alert(data.message);
      }
    } catch (e) {
      alert(e.message);
    }
  };

  const startEdit = (task) => {
    setEditTaskId(task.id);
    setEditFormData({
      name: task.name || "",
      description: task.description || "",
      deadline: task.deadline ? task.deadline.slice(0, 10) : "",
    });
  };

  const cancelEdit = () => {
    setEditTaskId(null);
    setEditFormData({ name: "", description: "", deadline: "" });
  };

  const handleEditChange = (e) => {
    const { name, value } = e.target;
    setEditFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleEditSave = async () => {
    try {
      const payload = {
        ...editFormData,
        deadline: editFormData.deadline
          ? new Date(editFormData.deadline).toISOString()
          : null,
      };

      if (!payload.name || payload.name.trim() === "") {
        alert("Task name is required");
        return;
      }

      const data = await updateTask(editTaskId, payload);
      if (data.success) {
        setTasks((prev) =>
          prev.map((task) => (task.id === editTaskId ? data.data : task))
        );
        cancelEdit();
      } else {
        alert(data.message || "Failed to update task");
      }
    } catch (e) {
      alert(e.message || "Unexpected error occurred");
    }
  };

  const filteredTasks = tasks.filter(task =>
    task.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  if (loading) return <p>Loading tasks...</p>;
  if (error) return <p style={{ color: "red" }}>Error: {error}</p>;

  return (
    <div>
      <h1>Task List</h1>

      <div style={{ marginBottom: 20 }}>
        <input
          type="text"
          placeholder="Search tasks..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          style={{ padding: "8px", width: "100%", maxWidth: "400px" }}
        />
      </div>

      {/* Добавление новой задачи */}
      <div style={{ marginBottom: 20, padding: 10, border: "1px solid gray" }}>
        <h3>Add New Task</h3>
        <input
          name="name"
          value={newTaskData.name}
          onChange={handleNewTaskChange}
          placeholder="Task name"
        />
        <br />
        <textarea
          name="description"
          value={newTaskData.description}
          onChange={handleNewTaskChange}
          placeholder="Task description"
        />
        <br />
        <input
          type="date"
          name="deadline"
          value={newTaskData.deadline}
          onChange={handleNewTaskChange}
        />
        <br />
        <button onClick={handleCreateTask}>Add Task</button>
      </div>

      {filteredTasks.length === 0 && <p>No tasks found.</p>}
      <ul style={{ listStyle: "none", padding: 0 }}>
        {filteredTasks.map((task) => (
          <li key={task.id} style={{ marginBottom: 15, border: "1px solid #ccc", padding: 10 }}>
            {editTaskId === task.id ? (
              <>
                <input
                  name="name"
                  value={editFormData.name}
                  onChange={handleEditChange}
                  placeholder="Task name"
                />
                <br />
                <textarea
                  name="description"
                  value={editFormData.description}
                  onChange={handleEditChange}
                  placeholder="Task description"
                />
                <br />
                <input
                  type="date"
                  name="deadline"
                  value={editFormData.deadline}
                  onChange={handleEditChange}
                />
                <br />
                <button onClick={handleEditSave}>Save</button>
                <button onClick={cancelEdit} style={{ marginLeft: 8 }}>
                  Cancel
                </button>
              </>
            ) : (
              <>
                <strong>{task.name}</strong> <br />
                <em>{task.description}</em> <br />
                <small>Deadline: {task.deadline ? new Date(task.deadline).toLocaleDateString() : "-"}</small>
                <br />
                <button onClick={() => startEdit(task)}>Edit</button>
                <button
                  onClick={() => handleDelete(task.id)}
                  style={{ marginLeft: 8, color: "red" }}
                >
                  Delete
                </button>
              </>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default TasksPage;
