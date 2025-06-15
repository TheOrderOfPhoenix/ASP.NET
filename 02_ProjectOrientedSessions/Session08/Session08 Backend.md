

# Fixes
- [ ] Check for missing `.ValueGeneratedOnAdd()` in all entity configuration.
- [ ] Adjust the database setup according to the second version of ERD
- [ ] Pay attention to the changes in logic and implementation of Person table (Id number is not unique anymore)
- [ ] Add actual data in `TicketStatus`, `Gender`, `TransactionTypes` 
- [ ] Add data in `Seat`, `Person`, `TicketOrder`, `Transaction`, `Ticket` for test. It is recommended to write python code for generating data for Seat table, according to the data already stored in Transportation and related Vehicle data
- [ ] Fix claim extraction (`sub` → standardize JWT claim mapping). (`IUserContext` implmentation)

### ✅ Profile Page (Account Info Tab)

- [ ]  Create `ProfileDto` to represent combined data for account, person, bank detail, balance.    
- [ ] Add `GetProfileAsync` in `AccountRepository` and expose via `AccountController`.
- [ ] Implement:
    - [ ]  Edit Email (with validation): `EditEmailDto`, service method, and controller endpoint.
    - [ ] Edit Password: `EditPasswordDto`, service and controller.
    - [ ] Edit Person Info: `UpsertAccountPersonDto`, and endpoint to upsert personal data.
    - [ ] Edit BankAccountDetail: `UpsertBankAccountDetailDto` and relevant logic.
- [ ] Add mapping for all Dtos and fix related properties like `CreatorAccountId`.
    

---

### ✅ List of Travelers

- [ ] Add `GetPeople` endpoint in `AccountController`.
- [ ] Implement separation of `UpsertAccountPerson` and `UpsertPerson`.
- [ ] Adjust Dto: `PersonDto` (with `id`, `creatorAccountId`, `englishFirstName`, etc.).


---

### ✅ My Travels Tab

- [ ]  Create `TicketOrderSummaryDto` (includes cities, vehicle name, price, etc.).
- [ ] Add `GetTravels` in `AccountService`, and expose `GetMyTravels` in controller.

---

### ✅ My Transactions Tab

- [ ] Create `TransactionDto` and mapping.
- [ ]  Add method to get transactions by `AccountId`.
- [ ]  Expose `GetMyTransactions` in `AccountController`.
- [ ]  Add modal to simulate balance top-up (manual input).
- [ ]  Format amount text based on transaction type: green (+) for income, red (–) for expense. 

---
