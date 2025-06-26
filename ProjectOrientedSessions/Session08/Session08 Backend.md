# Session 8
Profile + some fixes to database

## Fixes
- [ ] Check for missing `.ValueGeneratedOnAdd()` in all entity configurations in ```Infrastructure/Configurations```.

- [ ] Adjust the database setup according to the second version of ERD
    - Some aggregates are added/modified to generate the new database
    - New ERD: [Here](https://github.com/TheOrderOfPhoenix/ASP.NET/blob/main/ProjectOrientedSessions/docs/AlibabaERD-Version02.pdf)
    - Note: Pay attention to the changes in logic and implementation of Person table (Id number is not unique anymore). So remove this code in PersonConfiguration:

      ```
      builder.HasIndex(p => p.IdNumber)
          .IsUnique();
      ```

- [ ] Add actual data in `TicketStatus`, `Gender`, `TransactionTypes`
    - You can either add the data in DbContext or the database itself

- [ ] Add data in `Seat`, `Person`, `TicketOrder`, `Transaction`, `Ticket` for test. It is recommended to write python code for generating data for Seat table, according to the data already stored in Transportation and related Vehicle data. There is also a [SeatGenerator](https://github.com/TheOrderOfPhoenix/ASP.NET/blob/main/ProjectOrientedSessions/Session08/SeatGenerator.py) in this repository, as well.

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
