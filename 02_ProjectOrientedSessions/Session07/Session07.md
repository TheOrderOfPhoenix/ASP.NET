

### 🔹 1. **User Sign-Up Flow**

- React modal (SignUpModal component)
    
- Fields: `PhoneNumber`, `Password`, `ConfirmPassword`
    
- Frontend validation (empty fields, password length match, etc.)
    
- Send request to `/api/auth/register` (POST)
    
- Backend controller:
    
    - Check if user with phone exists
        
    - Validate password (min length, maybe complexity rules)
        
    - Hash password
        
    - Store new user in DB
        
    - Return success/failure
        

✅ **Backend helper**: Use `PasswordHasher<TUser>` to hash passwords securely  
✅ **Phone number** can act as username or unique field

---

### 🔹 2. **User Login Flow**

- React modal (LoginModal component)
    
- Fields: `PhoneNumber`, `Password`
    
- Submit to `/api/auth/login` (POST)
    
- Backend checks:
    
    - Find user by phone
        
    - Verify password (using PasswordHasher)
        
    - Generate **JWT**
        
    - Return token (and optionally refresh token)
        

✅ Token should include claims like user ID and role  
✅ Sign token with your secret key

---

### 🔹 3. **Store Token + Access Protected Page**

- Save token to `localStorage` or `cookie`
    
- Attach it to future API requests using Axios interceptor
    
- Use `jwt-decode` to extract roles and validate access in frontend
    
- Restrict `/profile` page using custom React `PrivateRoute`
    

Backend: Secure `/api/profile` with `[Authorize]` (or `[Authorize(Roles = "X")]`)

---

## 🛠 Technologies to Use

|Area|Tool|
|---|---|
|Frontend|React, Axios, React Router, jwt-decode|
|Backend|ASP.NET Core API, EF Core, JWT Bearer Auth|
|Security|PasswordHasher, Authorization attributes, Token signing key|

---


# Mention adding User Role in Database
# Becareful not to put Jwt inside another thing in the appsettings 