import { useState } from "react";
import { loginUser } from "../api/authApi";
import InputField from "../components/InputField";
import { Link } from "react-router-dom";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
const handleSubmit = async e => {
  e.preventDefault();
  const result = await loginUser({ email, password });

  if (result.success) {
    setMessage("Login successful");
    localStorage.setItem("userId", result.data.id); 
    window.location.href = "/tasks"; 
  } else {
    setMessage(result.message || "Login failed");
  }
};
  

  return (
    <form onSubmit={handleSubmit} style={{ maxWidth: "400px", margin: "0 auto" }}>
      <h2>Login</h2>
      <InputField label="Email" value={email} onChange={e => setEmail(e.target.value)} />
      <InputField label="Password" value={password} onChange={e => setPassword(e.target.value)} type="password" />
      <Link to="/tasks" onClick={handleSubmit}>Login</Link>
      <Link to="/register">Register</Link>
      <p>{message}</p>
    </form>
  );
}
