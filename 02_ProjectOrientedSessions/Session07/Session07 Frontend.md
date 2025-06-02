

# Branching
# Adding Models
📂 Suggested Folder: shared/models/authentication
- [ ] `AuthResponseDto.tsx`
```tsx
export interface AuthResponseDto {
  token: string;
  phoneNumber: string;
  roles: string[];
}
```

- [ ] `LoginRequestDto`
```tsx
export interface LoginRequestDto {
  phoneNumber: string;
  password: string;
}
```

- [ ] `RegisterRequestDto`
```tsx
export interface RegisterRequestDto {
  phoneNumber: string;
  password: string;
  confirmPassword: string;
}
```

# Adding Authentication API Calls in Agent
- [ ] Add the following code
```tsx
const Auth = {
register: (data: RegisterRequestDto) =>
    request.post<{ token: string }>('/auth/register', data),
login: (data: LoginRequestDto) =>
    request.post<{ token: string }>('/auth/login', data),
};
```
- [ ] Update agent
```tsx
const agent = {
    TransportationSearch,
    Cities,
    Auth
}
```

# Creating AuthStore
Suggested Folder

```tsx
import { AuthResponseDto } from '@/shared/models/authentication/AuthResponseDto';
import {create} from 'zustand';

interface User {
  phoneNumber: string;
  roles: any;
}

  

interface AuthState {
  isLoggedIn: boolean;
  user: User | null;
  token: string | null;
  login: (response: AuthResponseDto) => void;
  logout: () => void;
  setToken: (token: string) => void;
}

  

export const useAuthStore = create<AuthState>((set) => ({
  isLoggedIn: false,
  user: null,
  token: null,


  login: (response) =>
  set(() => ({
    token: response.token,
    user: {
      phoneNumber: response.phoneNumber,
      roles: response.roles
    },

    isLoggedIn: true,
  })),

  logout: () =>
    set(() => ({
      token: null,
      user: null,
      isLoggedIn: false,
    })),

  

  setToken: (token) =>
    set((state) => ({
      token,
      isLoggedIn: !!token,
      user: state.user,
    })),
}));
```

# Add LoginModal
```tsx
import React, { useState } from "react";
import { useAuthStore } from "@/store/authStore";
import agent from "@/shared/api/agent";
import { LoginRequestDto } from "@/shared/models/authentication/LoginRequestDto";

interface LoginModalProps {
  onClose: () => void;
}

  

const LoginModal: React.FC<LoginModalProps> = ({ onClose }) => {
  
  const login = useAuthStore((state) => state.login);

  const [form, setForm] = useState<LoginRequestDto>({
    phoneNumber: "",
    password: "",
  });

  const [error, setError] = useState<string | null>(null);

  const validate = () => {
    const phoneRegex = /^(?:\+98|0)?9\d{9}$/;
    if (!phoneRegex.test(form.phoneNumber)) {
      return "Invalid phone number format";
    }

    if (!form.password || form.password.length < 8) {
      return "Password must be at least 8 characters";
    }
    return null;
  };

  const handleSubmit = async () => {
    const validationError = validate();
    if (validationError) {
      setError(validationError);
      return;
    }

    try {
      const response = await agent.Auth.login(form);
      login(response.token); // update store with full auth info
      setError(null);
      onClose();
    } catch (err: any) {
      setError(err.response?.data?.message || "Login failed");
    }
  };

  

  return (
    <div style={styles.overlay}>
      <div style={styles.modal}>
        <h2 style={{ marginBottom: "1rem" }}>Login</h2>
        <input
          type="text"
          placeholder="Phone Number"
          value={form.phoneNumber}
          onChange={(e) => setForm({ ...form, phoneNumber: e.target.value })}
          style={styles.input}
        />

        <input
          type="password"
          placeholder="Password"
          value={form.password}
          onChange={(e) => setForm({ ...form, password: e.target.value })}
          style={styles.input}
        />
        <button onClick={handleSubmit} style={styles.button}>
          Login
        </button>
        {error && (
          <p style={{ color: "red", marginTop: "0.5rem", fontWeight: "bold" }}>
            {error}
          </p>
        )}

        <button
          onClick={onClose}
          style={{
            ...styles.button,
            marginTop: "0.5rem",
            backgroundColor: "#ccc",
            color: "#333",
          }}
        >
          Cancel
        </button>
      </div>
    </div>
  );
};

const styles: { [key: string]: React.CSSProperties } = {
  overlay: {
    position: "fixed",
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: "rgba(0,0,0,0.5)",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    zIndex: 1000,
  },

  modal: {
    backgroundColor: "white",
    padding: "2rem",
    borderRadius: "8px",
    boxShadow: "0 2px 10px rgba(0,0,0,0.3)",
    width: "320px",
    display: "flex",
    flexDirection: "column",
  },

  input: {
    marginBottom: "1rem",
    padding: "0.5rem",
    fontSize: "1rem",
    borderRadius: "4px",
    border: "1px solid #ccc",
  },

  button: {
    padding: "0.6rem 1.2rem",
    fontSize: "1rem",
    borderRadius: "4px",
    border: "none",
    backgroundColor: "#007bff",
    color: "white",
    cursor: "pointer",
  },
};

export default LoginModal;
```


# Add RegisterModal

```tsx
import agent from "@/shared/api/agent";

import { RegisterRequestDto } from "@/shared/models/authentication/RegisterRequestDto";

import { useAuthStore } from "@/store/authStore";

import React, { useState } from "react";

  

interface Props {
  onClose: () => void;
}

  

// Define RegisterRequestDto interface explicitly for form and request typing

  

const RegisterModal: React.FC<Props> = ({ onClose }) => {

  const [form, setForm] = useState<RegisterRequestDto>({
    phoneNumber: "",
    password: "",
    confirmPassword: "",
  });

  

  const [error, setError] = useState<string | null>(null);

  

  const login = useAuthStore((state) => state.login);

  

  const validate = () => {

    const { phoneNumber, password, confirmPassword } = form;

    if (!phoneNumber || !password || !confirmPassword) {

      return "All fields are required.";

    }

    if (!/^\d{11}$/.test(phoneNumber)) {

      return "Phone number must be 11 digits.";

    }

    if (password.length < 6) {

      return "Password must be at least 6 characters.";

    }

    if (password !== confirmPassword) {

      return "Passwords do not match.";

    }

    return null;

  };

  

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {

    setForm({ ...form, [e.target.name]: e.target.value });

  };

  

  const handleSubmit = async (e: React.FormEvent) => {

    e.preventDefault();

    setError(null);

  

    const validationError = validate();

    if (validationError) {
      setError(validationError);
      return;
    }

  

    try {

      // Use the form as RegisterRequestDto explicitly

      const requestData: RegisterRequestDto = {
        phoneNumber: form.phoneNumber,
        password: form.password,
        confirmPassword: form.confirmPassword,
      };

  

      const response = await agent.Auth.register(requestData);
      login(response.token);
      setForm({ phoneNumber: "", password: "", confirmPassword: "" });
      onClose();
    } catch (err: any) {
      setError(err.response?.data?.message || "Registration failed.");
    }
  };

  

  return (
    <div style={styles.overlay}>
      <div style={styles.modal}>
        <h2 style={{ marginBottom: "1rem" }}>Register</h2>
        <form
          onSubmit={handleSubmit}
          style={{ display: "flex", flexDirection: "column" }}
        >

          <input
            type="text"
            name="phoneNumber"
            value={form.phoneNumber}
            onChange={handleChange}
            placeholder="Phone Number"
            style={styles.input}
          />

          <input
            type="password"
            name="password"
            value={form.password}
            onChange={handleChange}
            placeholder="Password"
            style={styles.input}
          />

          <input
            type="password"
            name="confirmPassword"
            value={form.confirmPassword}
            onChange={handleChange}
            placeholder="Confirm Password"
            style={styles.input}
          />

          {error && (

            <p
              style={{ color: "red", marginTop: "0.5rem", fontWeight: "bold" }}
            >
              {error}
            </p>
          )}

          <button type="submit" style={styles.button}>
            Register
          </button>

        </form>

        <button

          onClick={onClose}

          style={{
            ...styles.button,
            marginTop: "0.5rem",
            backgroundColor: "#ccc",
            color: "#333",
          }}
        >
          Cancel
        </button>
      </div>
    </div>
  );
};

  

const styles: { [key: string]: React.CSSProperties } = {

  overlay: {
    position: "fixed",
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: "rgba(0,0,0,0.5)",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    zIndex: 1000,
  },

  modal: {
    backgroundColor: "white",
    padding: "2rem",
    borderRadius: "8px",
    boxShadow: "0 2px 10px rgba(0,0,0,0.3)",
    width: "320px",
    display: "flex",
    flexDirection: "column",
  },

  input: {
    marginBottom: "1rem",
    padding: "0.5rem",
    fontSize: "1rem",
    borderRadius: "4px",
    border: "1px solid #ccc",
  },

  button: {
    padding: "0.6rem 1.2rem",
    fontSize: "1rem",
    borderRadius: "4px",
    border: "none",
    backgroundColor: "#007bff",
    color: "white",
    cursor: "pointer",
  },
};

export default RegisterModal;
```
