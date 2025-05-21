import { useState } from "react";
import { registerUser } from "../api/authApi";
import InputField from "../components/InputField";

export default function RegisterPage(){
    const [email, setEmail] = useState("");
    const [name, setName] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState("");

    const handleSubmit = async e => {
        e.preventDefault();
        if (!email.includes("@")) return setMessage("Invalid email format");

        const result = await registerUser({ name, email, password });
        if (result.success) {
        setMessage("User registered successfully");
        } else {
        setMessage(result.message || "Registration failed");
        }
    };

    return (
        <form onSubmit={handleSubmit} style={{ maxWidth: "400px", margin: "0 auto" }}>
        <h2>Register</h2>
        <InputField label="Email" value={email} onChange={e => setEmail(e.target.value)} />
        <InputField label="Name" value={name} onChange={e => setName(e.target.value)} />
        <InputField label="Password" value={password} onChange={e => setPassword(e.target.value)} type="password" />
        <button type="submit">Register</button>
        <p>{message}</p>
        </form>
    );
}