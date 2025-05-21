export default function InputField({ label, value, onChange, type = "text" }) {
  return (
    <div>
      <label>{label}</label><br />
      <input
        type={type}
        value={value}
        onChange={onChange}
        required
        style={{ padding: "8px", marginBottom: "12px", width: "100%" }}
      />
    </div>
  );
}
