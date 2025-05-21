const API_URL = "https://localhost:7016/api";

export async function loginUser(data) {
  const res = await fetch(`${API_URL}/user/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  return await res.json();
}

export async function registerUser(data) {
    console.log(`${API_URL}/user/register`);
  const res = await fetch(`${API_URL}/User/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  console.log(data);
  return await res.json();
}
