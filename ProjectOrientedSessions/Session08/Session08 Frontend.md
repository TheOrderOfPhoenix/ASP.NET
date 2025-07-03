# ✅ Frontend Profile Page Implementation Checklist

## ⚙️ Tooling & Fixes
- [ ]  Install and configure `react-hook-form`
```
npm install react-hook-form
```

## 🔐 Account & Authentication
- [ ] Use Axios request interceptor to attach token to requests and handle logout logic
```
// add a request interceptor to include JWT token if available
agent.interceptors.request.use((config) => {
  const token = useAuthStore.getState().token;
  if (token) {
    config.headers = config.headers || {};
    config.headers["Authorization"] = `Bearer ${token}`;
  }
  return config;
});

// add a response interceptor to handle 401 Unauthorized
agent.interceptors.response.use(
  (res) => res,
  (error) => {
    if (error.response?.status === 401) {
      useAuthStore.getState().logout();
      window.location.href = "/login"; // redirect to login
    }
    return Promise.reject(error);
  }
);
```

## API
- [ ] Provide a method for each new endpoint implemented in backend
```
const Profile = {
  getProfile: () => request.get<ProfileDto>('/account/profile'),
  editEmail: (data: EditEmailDto) => request.put<void>('/account/email', data),
  editPassword: (data: EditPasswordDto) => request.put<void>('/account/password', data),
  upsertAccountPerson: (data: PersonDto) => request.post<number>('/account/account-person', data),
  upsertPerson: (data: PersonDto) => request.post<number>('/account/person', data),
  upsertBankDetail: (data: UpsertBankAccountDetailDto) => request.post<void>('/account/bank-detail', data),
  getMyPeople: () => request.get<PersonDto[]>('/account/my-people'),
  getMyTravels: () => request.get<TicketOrderSummaryDto[]>('/account/my-travels'),
  getMyTransactions: () => request.get<TransactionDto[]>('/account/my-transactions'), 
  topUp: (data : topUpDto) => request.post<number>('/account/top-up', data)
};

```

## 🗂️ Additional Profile Tabs (initial setup)

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

## Profile Summary 
### 📦 DTOs / Models
-  Add models for:
    - [ ]  `EditEmailDto`
    - [ ]  `EditPasswordDto`
    - [ ]  `PersonDto`
    - [ ]  `ProfileDto`
    - [ ] `UpsertBankAccountDetailDto`
 
 - [ ] Implement the process of showing and editing the data

## 🧍 List of Travelers
- [ ]  Implement `ListOfTravelers` page for showing, editing, and adding new people


## 💳 Transactions Module
- [ ] Add `TransactionDto` model
- [ ]  Implement `MyTransactions` page

## 🚆 Travel Module
- [ ] Add `TicketOrderSummaryDto`, `TravelerTicketDto` models
- [ ] Implement `MyTravels` page
- [ ] Implement `TravelOrderDetailsPage`
