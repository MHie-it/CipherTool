import { useState } from "react";
import "./App.css";

const API = "https://localhost:7081/api/cipher";

function App() {
  const [text, setText] = useState("");
  const [result, setResult] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const call = async (action) => {
    setLoading(true);
    setError("");
    setResult("");
    try {
      const res = await fetch(`${API}/${action}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ text }),
      });
      const data = await res.json();
      if (data.success) setResult(data.result);
      else setError(data.error || "Có lỗi xảy ra");
    } catch {
      setError("Không kết nối được Backend.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="app">
      <h1> Mã hóa / Giải mã </h1>

      <div className="card">
        <label>Văn bản</label>
        <textarea
          rows={4}
          placeholder="Nhập nội dung..."
          value={text}
          onChange={(e) => setText(e.target.value)}
        />

        <div className="buttons">
          <button onClick={() => call("encrypt")} disabled={loading}>
             Mã hóa
          </button>
          <button onClick={() => call("decrypt")} disabled={loading} className="secondary">
             Giải mã
          </button>
        </div>

        {loading && <p className="info">Đang xử lý...</p>}
        {error && <p className="error"> {error}</p>}

        {result && (
          <div className="result">
            <label>Kết quả</label>
            <textarea rows={4} readOnly value={result} />
            <button onClick={() => navigator.clipboard.writeText(result)}>
               Sao chép
            </button>
          </div>
        )}
      </div>
    </div>
  );
}

export default App;