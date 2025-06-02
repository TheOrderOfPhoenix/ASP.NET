
# Branching
 - [ ] Create the feature/authentication branch based on develop

# Adding Models
Create three files inside this folder:
📂 Suggested Folder: shared/models/authentication

- [ ] `AuthResponseDto.ts`
```ts
export interface AuthResponseDto {
  token: string;
  phoneNumber: string;
  roles: string[];
}
```

- [ ] `LoginRequestDto.ts`
```ts
export interface LoginRequestDto {
  phoneNumber: string;
  password: string;
}
```

- [ ] `RegisterRequestDto.ts`
```ts
export interface RegisterRequestDto {
  phoneNumber: string;
  password: string;
  confirmPassword: string;
}
```

# Adding Authentication API Calls in `agent.ts`
- [ ] Add the `Auth` object to `agent.ts`:
```ts
const Auth = {
register: (data: RegisterRequestDto) =>
    request.post<{ token: string }>('/auth/register', data),
login: (data: LoginRequestDto) =>
    request.post<{ token: string }>('/auth/login', data),
};
```
- [ ] Ensure `agent.ts` ends like this:
```tsx
const agent = {
    TransportationSearch,
    Cities,
    Auth
}
```

# Creating `authStore.ts`
Suggested Folder
📂 Suggested Folder: shared/store/
- [ ] Create authStore.ts
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

## About `authStore.ts`

This file defines a centralized **authentication state store** using [`Zustand`](https://github.com/pmndrs/zustand), a minimal and scalable state management library for React. It helps manage login state, user data, and authentication token across your application.
    
---

## 🔹 `User` Interface

```ts
interface User {
  phoneNumber: string;
  roles: string[];
}
```

This interface defines the shape of the `user` object stored in the auth state. It currently includes:
- `phoneNumber`: A string representing the user's phone number.
- `roles`: An array representing user roles
    
---

## 🔹 `AuthState` Interface

```ts
interface AuthState {
  isLoggedIn: boolean;
  user: User | null;
  token: string | null;
  login: (response: AuthResponseDto) => void;
  logout: () => void;
  setToken: (token: string) => void;
}
```

This defines the overall structure of the authentication store:

- `isLoggedIn`: Indicates whether a user is logged in.
- `user`: Stores user-specific data if authenticated; otherwise `null`.
- `token`: JWT or access token from the server.
- `login()`: Accepts an `AuthResponseDto` and updates the state.
- `logout()`: Clears all authentication-related data.    
- `setToken()`: Sets the token and toggles login status accordingly.


---

## 🔹 Zustand Store Definition

```ts
export const useAuthStore = create<AuthState>((set) => ({
```

Creates a global auth store using Zustand. `create()` accepts a function that receives `set` (used to update state) and returns the initial store state and methods.

---

## 🔹 Initial State

```ts
isLoggedIn: false,
user: null,
token: null,
```

These lines define the initial, default state for an unauthenticated user.

---

## 🔹 `login()` Method

```ts
login: (response) =>
  set(() => ({
    token: response.token,
    user: {
      phoneNumber: response.phoneNumber,
      roles: response.roles
    },
    isLoggedIn: true,
  })),
```

- Accepts an `AuthResponseDto` object after a successful login.
    
- Extracts the `token`, `phoneNumber`, and `roles`, and sets them in state.
    
- Marks the user as `isLoggedIn: true`.
    

---

## 🔹 `logout()` Method

```ts
logout: () =>
  set(() => ({
    token: null,
    user: null,
    isLoggedIn: false,
  })),
```

- Clears all auth-related data (token and user).
    
- Effectively logs the user out by setting `isLoggedIn` to `false`.
    

---

## 🔹 `setToken()` Method

```ts
setToken: (token) =>
  set((state) => ({
    token,
    isLoggedIn: !!token,
    user: state.user,
  })),
```

- Updates the token in the store.
    
- Sets `isLoggedIn` based on whether a non-empty token exists.
    
- Retains the current `user` object.
    

---
# Add LoginModal
📂 Suggested Folder: shared/features/authentication/modals
## Example
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
      login(response);
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
	//ADD STYLES
};

export default LoginModal;
```

## About LoginModal

This component provides a modal UI that allows users to log in using their **phone number and password**. It integrates with the authentication store and API to perform login logic and handle errors.

---
## 🔹 Props Interface

```tsx
interface LoginModalProps {
  onClose: () => void;
}
```

- `onClose`: A callback to be called when the modal should be closed (e.g., user clicks "Cancel" or logs in successfully).

---

## 🔹 Component Setup

```tsx
const LoginModal: React.FC<LoginModalProps> = ({ onClose }) => {
```

Defines a functional React component with the `onClose` prop destructured.

### 🔸 Accessing Auth Store

```tsx
const login = useAuthStore((state) => state.login);
```

Retrieves the `login` method from Zustand’s `authStore` so that the global auth state can be updated after successful login.

### 🔸 Local Form State

```tsx
const [form, setForm] = useState<LoginRequestDto>({
  phoneNumber: "",
  password: "",
});
```

Initializes `form` state with empty values for the phone number and password.

### 🔸 Error Handling State

```tsx
const [error, setError] = useState<string | null>(null);
```

Stores any error messages resulting from validation or login attempt.

---

## 🔹 Validation Logic

```tsx
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
```

- Validates the phone number format (Iranian phone format in this case).
- Ensures password is at least 8 characters long.
- Returns a string error message or `null` if validation passes.

---
## 🔹 Submit Handler

```tsx
const handleSubmit = async () => {
  const validationError = validate();
  if (validationError) {
    setError(validationError);
    return;
  }

  try {
    const response = await agent.Auth.login(form);
    login(response);
    setError(null);
    onClose();
  } catch (err: any) {
    setError(err.response?.data?.message || "Login failed");
  }
};
```

- Calls `validate()` and prevents submission if there's an error.
- Calls the backend API using `agent.Auth.login()`.
- On success: updates auth state, clears error, closes modal.
- On failure: shows error message.
    

---

## 🔹 UI Layout

```tsx
return (
  <div style={styles.overlay}>
    <div style={styles.modal}>
      <h2>Login</h2>
      <input ... />
      <input ... />
      <button onClick={handleSubmit}>Login</button>
      {error && <p>{error}</p>}
      <button onClick={onClose}>Cancel</button>
    </div>
  </div>
);
```

### Elements:

- **Phone Number Input**
    
- **Password Input**
    
- **Login Button**: Triggers `handleSubmit`.
    
- **Error Message**: Shown only if there's an error.
    
- **Cancel Button**: Triggers `onClose` callback.
    

---

## 🔹 Styles Placeholder

```tsx
const styles: { [key: string]: React.CSSProperties } = {
  // Add modal styles here
};
```

This placeholder defines inline CSS styles for the modal. Each style (e.g., `overlay`, `modal`, `input`, `button`) should be defined here.

---

## ✅ Summary

This modal:

- Provides a simple, reusable login form.
    
- Validates input before calling the API.
    
- Updates global auth state via Zustand.
    
- Handles success/failure states.
    
- Uses modal-friendly inline styles (with room for improvement).
    

---
# Add RegisterModal
- [ ] Create RegisterModal
📂 Suggested Folder: shared/features/authentication/modals
```tsx
import agent from "@/shared/api/agent";
import { RegisterRequestDto } from "@/shared/models/authentication/RegisterRequestDto";
import { useAuthStore } from "@/store/authStore";
import React, { useState } from "react";


interface Props {
  onClose: () => void;
}


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
      login(response);
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
	//ADD STYLES
};

export default RegisterModal;
```

## About RegisterModal

    
## **Component Structure**

### 1. **Props**

```tsx
interface Props {
  onClose: () => void;
}
```

- The modal only expects one prop: `onClose`, a function to close the modal (e.g., hide it from the screen).
    
---

### 2. **State Management**

```tsx
const [form, setForm] = useState<RegisterRequestDto>({
  phoneNumber: "",
  password: "",
  confirmPassword: "",
});
```

- Initializes the form state for inputs, based on the `RegisterRequestDto` shape.

```tsx
const [error, setError] = useState<string | null>(null);
```

- Stores any validation or server error message to display in the UI.

```tsx
const login = useAuthStore((state) => state.login);
```

- Accesses the `login` method from your global auth store, to automatically log in the user after successful registration.
    

---

### 3. **Validation Logic**

```tsx
const validate = () => {
  // Checks for empty fields
  // Validates phone number format (must be 11 digits)
  // Ensures password length is sufficient
  // Confirms password and confirmation match
};
```

- Ensures client-side validation before making a request to the server.

---

### 4. **Input Handling**

```tsx
const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
  setForm({ ...form, [e.target.name]: e.target.value });
};
```

- Updates the correct field in the `form` object dynamically based on the input `name`.

---

### 5. **Form Submission**

```tsx
const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault();
  setError(null);
  const validationError = validate();
  // If validation passes, submit the data to the backend
  // If backend response is successful, log in and close modal
  // If it fails, show error message
};
```

- Prevents default form submission
- Validates inputs
- Sends the data to `agent.Auth.register`
- On success: logs in user and clears form
- On failure: shows error from server

---

### 6. **JSX Render**

```tsx
<div style={styles.overlay}>...</div>
```

- **Modal Overlay**: darkened background behind the modal
- **Modal Box**: contains title, form, and buttons

### Inside `<form>`:

- Inputs for:  
      - `phoneNumber`  
      - `password`  
      - `confirmPassword`
    
- Submit button for Register
    
- Error message display (if any)
    
- Cancel button that calls `onClose`
    
---

# Merge
- [ ] Create a PR and merge the current branch with develop
