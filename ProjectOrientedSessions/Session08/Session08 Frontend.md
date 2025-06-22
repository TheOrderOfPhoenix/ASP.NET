## ✅ Frontend Profile Page Implementation Checklist

### ⚙️ Tooling & Fixes
- [ ]  Install and configure `react-hook-form`
---
### 🔐 Account & Authentication
- [ ] Use Axios request interceptor to:
    - [ ]  Attach token to requests
    - [ ] Handle logout logic
---
### API
- [ ] Provide a method for each new endpoint implemented in backend  
---
### 🗂️ Additional Profile Tabs (initial setup)

- [ ]  Add empty pages/tabs for:
    - [ ] `ProfileSummary`
	- [ ] `MyTravels`
	- [ ] `ListOfTravelers`
	- [ ] Favorites
    - [ ] Support
    - [ ] `MyTransactions`
- [ ] Implement `ProfilePage` component
- [ ]  Create prototype of `ProfilePage` and integrate with navbar. (Create a button or link to access `ProfilePage`, only when user is logged in)
- [ ] Define and adjust routes for profile and its tabs
- [ ] Implement route-based tab handling inside `ProfilePage`

---
### Profile Summary 
#### 📦 DTOs / Models
-  Add models for:
    - [ ]  `EditEmailDto`
    - [ ]  `EditPasswordDto`
    - [ ]  `PersonDto`
    - [ ]  `ProfileDto`
    - [ ] `UpsertBankAccountDetailDto`
 - [ ] Implement the process of showing and editing the data
---
### 🧍 List of Travelers
- [ ]  Implement `ListOfTravelers` page for showing, editing, and adding new people

---
### 💳 Transactions Module
- [ ] Add `TransactionDto` model
- [ ]  Implement `MyTransactions` page
---
### 🚆 Travel Module
- [ ] Add `TicketOrderSummaryDto`, `TravelerTicketDto` models
- [ ] Implement `MyTravels` page
- [ ] Implement `TravelOrderDetailsPage`

---
