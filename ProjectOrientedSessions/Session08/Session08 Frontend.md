
# 🛠️ Task Checklist

## ⚙️ Tooling & Fixes
- [ ]  Install and configure `react-hook-form`
```
npm install react-hook-form
```

## 🔐 Account & Authentication
- [ ] Use Axios request interceptor to attach token to requests and handle logout logic
```ts
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
```ts
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
    - [ ] `MyTransactions`
    - [ ] `MyPeople`

- [ ] Implement `ProfilePage` using the components created. This is an example of how it can be done:
```ts
import InfoAccount from "./InfoAccount";
import InfoPeople from "./InfoPeople";
import InfoTransactions from "./InfoTransactions";
import InfoTravels from "./InfoTravels";
import { useState } from "react";

const tabs = [
  { label: "Account", component: <InfoAccount /> },
  { label: "Transactions", component: <InfoTransactions /> },
  { label: "Travels", component: <InfoTravels /> },
  { label: "People", component: <InfoPeople /> }
];

const Profile = () => {
  const [selected, setSelected] = useState(0);
  return (
    <div className="max-w-4xl mx-auto py-8 px-4">
      <div className="flex flex-col md:flex-row gap-8">
        <aside className="md:w-64 w-full bg-white rounded-lg shadow p-4 flex md:flex-col flex-row gap-2 md:gap-0 mb-4 md:mb-0">
          {tabs.map((tab, idx) => (
            <button
              key={tab.label}
              className={`text-right px-4 py-2 rounded transition font-medium text-base md:text-lg w-full ${
                selected === idx
                  ? "bg-blue-100 text-blue-700"
                  : "hover:bg-gray-100 text-gray-700"
              }`}
              onClick={() => setSelected(idx)}
              type="button"
            >
              {tab.label}
            </button>
          ))}
        </aside>
        <main className="flex-1 min-w-0">{tabs[selected].component}</main>
      </div>
    </div>
  );
};

export default Profile;

```

- [ ]  Create prototype of `ProfilePage` and integrate with navbar. (Create a button or link to access `ProfilePage`, only when user is logged in)

- [ ] Define and adjust routes for profile and its tabs
```ts
const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/profile" element={<Profile />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};

export default AppRoutes;

```

- [ ] Implement tab handling inside `ProfilePage`. You can also do it **route-based**.

## Profile Summary 
### 📦 DTOs / Models
-  Add models for:
    - [ ]  `EditEmailDto`
	```ts
 	export interface EditEmailDto {
	    newEmail: string;
	}
 	```
    - [ ]  `EditPasswordDto`
	```ts
 	export interface EditPasswordDto {
	    oldPassword: string;
	    newPassword: string;
	    confirmNewPassword: string;
	}
 	```
    - [ ]  `PersonDto`
	```ts
	export interface PersonDto {
 	    id?: number;
 	    creatorAccountId?: number;
 	    firstName: string;
	    lastName: string;
 	    idNumber: string;
	    genderId: number;
	    phoneNumber: string;
	    birthDate: string;
 	}
 	```
    - [ ]  `ProfileDto`
	```ts
 	export interface ProfileDto {
	    accountPhoneNumber: string;
	    email: string;
	    balance: number;

	    firstName: string;
	    lastName: string;
	    idNumber: string;
	    personPhoneNumber: string;
	    birthDate: Date | string | null;

	    iban: string;
	    bankAccountNumber: string;
	    cardNumber: string;
 	}
 	```
    - [ ] `UpsertBankAccountDetailDto`
	```ts
	export interface UpsertBankAccountDto {
	    iban?: string;
	    bankAccountNumber?: string;
 	    cardNumber?: string;
 	}
 	```
 
 - [ ] Implement the process of showing and editing the data. To do so, you can use modal components and set their functionality in the pages.

## 🧍 List of Travelers
- [ ]  Implement `ListOfTravelers` page for showing, editing, and adding new people. You can use the same modal used for `UpsertAccountPerson`.

## 💳 Transactions Module
- [ ] Add `TransactionDto` model
```ts
export interface TransactionDto {
    id: number;
    transactionTypeId: number;
    accountId: number;
    ticketOrderId?: number;
    baseAmount: number;
    finalAmount: number;
    serialNumber: string;
    createdAt: Date | string;
    description?: string;
    transactionType: string;
}
```
- [ ]  Implement `MyTransactions` page
Note: It is recommended to add a component to be displayed as a card, including the table of information, then use it in the page of MyTransactions

## 🚆 Travel Module
- [ ] Add `TicketOrderSummaryDto` model
```ts
export interface TicketOrderSummaryDto {
    id: number;
    serialNumber: string;
    boughtAt: Date | string;

    price: number;

    travelStartDate: Date | string;
    travelEndDate: Date | string;

    fromCity: string;
    toCity: string;

    companyName: string;

    vehicleTypeId: number;
    vehicleName: string;
}
```

- [ ] Implement `MyTravels` page. Note that this page can be similarly implemented by a card.


# 🧠 Hints & Notes
# 🙌 Acknowledgements

- ChatGPT for snippet refinement and explanations
# 🔍 References






