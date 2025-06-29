Here's a **structured checklist document**, broken into **sections by feature area**. Each section contains tasks in implementation order (oldest to newest), rephrased as clear action items with contextual notes for implementation.
These are created based on this commit messages: 
https://github.com/MehrdadShirvani/AlibabaClone-Backend/commits/develop/

---
# Miscellaneous / Fixes

- [ ] Fix GUID generation and async handling in ticket creation. (Use `Guid.NewGuid()` instead of `new Guid()`)
- [ ]  Make `SerialNumber`, `TicketOrderId`, `BaseAmount` publicly settable in DTOs.
- [ ] Fix issues in seat mappings and transportation queries.
# Fixes and Missing Parts from Session 08 

## 👤 Person & Account Info

- [ ] Add `Person`, `BankAccountDetail`, and navigation properties.
- [ ]  Add `UpsertPersonAsync` and `UpsertBankAccountDetailAsync` in `IAccountService`.
- [ ] Add `UpsertPerson`, `UpsertBankDetail` endpoints in `AccountController`.
- [ ] Add `GetPeople`, `GetProfileAsync`, and DTOs (`ProfileDto`, `PersonDto`, `BankAccountDetailDto`).
- [ ] Handle person editing logic via `UpsertPerson` and adjust classes that used it the old way.
## 🔐 Auth & Settings

- [ ]  Complete the error handling of `EditEmailDto`, `EditPasswordDto`.
- [ ] Add `GetByEmailAsync` in `AccountRepository`.



# Branching
- [ ] Create the feature/ticket-reservation branch based on develop

# Ticket Ordering System

## 🧱 Domain and Infrastructure Setup

- [ ] Check out new ERD: [Here](https://github.com/TheOrderOfPhoenix/ASP.NET/blob/main/ProjectOrientedSessions/docs/AlibabaERD-Version02.pdf)
- [ ] Create `TicketOrder`, `Ticket`, and `Transaction` entities.
- [ ]  Add configurations for `TicketOrder`, `Ticket`, and `Transaction` (relationships, constraints). 
- [ ] Adjust `Transportation` entity configuration to support ticketing.
- [ ] Add migrations for the above database changes.

## 🧑‍💼 Service Layer

- [ ]  Create `ITicketOrderService`, `TicketOrderService` and implement:
    - [ ] `CreateTicketOrderAsync`
    - [ ] `GenerateTicketsPdfAsync`
- [ ]  Register `TicketOrderService` in DI container.

## 🎯 Controller Layer

- [ ]  Create `TicketOrderController` with the following endpoints:
    - [ ] `POST /CreateTicketOrder`
    - [ ] `GET /DownloadPdf`
        
## 🔁 DTOs & Mappings

- [ ]  Add `CreateTicketOrderDto`, `CreateTravelerTicketDto`.
- [ ]  Add `TicketOrderSummaryDto`, `TravelerTicketDto`.
- [ ]  Add mappings in `MappingProfile`.

## 🗃️ Repository Layer

- [ ]  Create `ITicketOrderRepository`, `TicketOrderRepository`.
- [ ]  Implement:
    - [ ] `FindAndLoadAllDetails`
    - [ ] `GetAllByBuyerId`

---

# Transportation and Seat Selection

- [ ]  Add `TransportationSeatDto`.
- [ ]  Add method `GetSeatsByVehicleId` in `ISeatRepository` and implement it.
- [ ]  Add mapping from `Seat` to `TransportationSeatDto`.
- [ ]  Add `GetTransportationSeatsAsync` in `ITransportationService` and implement.
- [ ]  Add `GetTransportationSeats` endpoint in `TransportationController`.
- [ ] Fix vehicle and seat-related mapping issues (e.g., missing `VehicleTypeId`, logic errors).
- [ ]  Ensure `RemainingCapacity` is treated as calculated (ignored in EF, removed from schema).

---

# Coupon System

## 🏗️ Domain & Infrastructure
- [ ]  Add `Coupon` entity with `IsExpired`, `CouponCode` (unique).
- [ ]  Add `ICouponRepository`, `CouponRepository`.
- [ ] Add `DiscountDto`, `CouponValidationRequestDto`.
- [ ]  Add migrations for new `Coupon` table.

### 🧑‍💼 Service & Logic
- [ ]  Create `ICouponService`, implement validation logic.
- [ ]  Add coupon validation in `TicketOrderService`.
### 🎯 Controller
- [ ]  Add `ValidateCoupon` endpoint in `CouponController`.

---

# Payment & Transactions

- [ ]  Add `CouponId` to `PayForTicketOrderAsync` in `IAccountService` & implementation.
- [ ] Add `CouponId` to `TransactionDto` (adjusted to `CouponCode` later).
- [ ]  Add `TopUpAccount` logic (DTO, Controller, Service).
- [ ]  Add `Transaction` relationship to `TicketOrder` as one-to-one.
- [ ]  Add logic for creating transactions with tickets.
- [ ]  Add endpoint `GetMyTransactions` and DTOs (`TransactionDto`).

---

## ✅ Ticket Review & Confirmation

- [ ] Add `TicketOrderSummaryDto`, map details: from/to city, company, vehicle, time.
- [ ]  Implement `GetTravelOrderDetails` endpoint to fetch ticket summary.
- [ ]  Display number of travelers, per-seat price, and total cost.
- [ ]  Include coupon entry and balance payment option.
- [ ]  Use ticket + person data (via `GetTicketOrderTravelersDetails`).

---

## ✅ PDF Generation

- [ ] Install `QuestPDF` in infrastructure.
- [ ]  Create `IPdfGenerator`, implement with QuestPDF.
- [ ] Register `IPdfGenerator` service.
- [ ]  Create `PdfGenerator` logic to render ticket PDFs.
- [ ]  Add PDF download endpoint (`DownloadPdf`) in `TicketOrderController`.
---
# Merge
- [ ] Create a PR and merge the current branch with develop


---


