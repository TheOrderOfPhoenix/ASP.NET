# Reservation System Development Guide

This guide helps developers understand how to implement, update, and maintain the ticket reservation system. It summarizes changes from commits and provides step-by-step actions, grouped by feature area.
https://github.com/MehrdadShirvani/AlibabaClone-Frontend/commits/develop/
---

# Fixes and Missing Things from Session 08
## Authentication & Protected Routes

- [ ] Store `showLoginModal` state in `authStore` and adjust navbar
- [ ] Add `ProtectedRoute` component
- [ ]  Adjust main `App` to route authenticated pages through protection layer
- [ ]  Add session persistence in `authStore` to store token more persistently 

---
## Profile and User Info Enhancements

- [ ]  Fix and align `birthDate` types in `PersonDto`, `transportationSearchResult`, and `ListOfTravelers`
## Agent
- [ ] Add `topUpDto` and its method in `agent.ts`
- [ ]  Add optional config parameter to `request()` in `agent.ts`

# Branching
- [ ] Create the feature/themes branch based on develop
# 🎨 Theming and UI Styling
- [ ] Install and import `preline`
- [ ] Add theme colors and global styles (index.css)
- [ ] Add `ThemeSwitcher` and integrate it into the navbar
- [ ] If you decide to do this part after implementing pages, make sure to add theme support to the following components:
    - [ ]  `transportationCard`, `transportationSearchForm`, `ReviewAndConfirm`
    - [ ]  All modals: `LoginModal`, `RegisterModal`, `SelectFromPeopleModal`
    - [ ]  Profile section: `ProfilePage`, `ProfileSummary`, `PersonalInformation`, `AccountInfo`, `PersonalAccountInfo`, `BankAccountDetails`, `MyTravels`, `MyTransactions`, `ListOfTravelers`
    - [ ]  Reservation views and components

# Merge
- [ ] Create a PR and merge the current branch with develop
---
# Branching
- [ ] Create the feature/ticket-reservation branch based on develop
# Backend Agent Methods
- [ ] Add `createTicketOrderDto` and `createTravelerTicketDto`
- [ ] Add `transportationSeatDto`
- [ ] Add related methods in `TicketOrder` and add it to agent

# Reservation Process & Step Management

- [ ] Implement `useReservationStore` using `zustand` to manage reservation state
- [ ]  Create step-based routing using `ReservationLayout`
- [ ] Add routing for reservation steps in `App.tsx`
- [ ] Add `StepIndicator` component to show step progress visually
- [ ] Add `stepGuard` logic to prevent accessing future steps prematurely
- [ ] Add logic to skip back only if previous steps are completed
- [ ] Create `TravelerForm` to gather passenger info, with the possibility to load data from the related people of the account.
- [ ] Create `TravelerDetailsForm` to gather passengers info with `TravelerForm` integration
- [ ] Create `ReviewAndConfirm` page to review selections
- [ ] Create `PaymentForm` for transaction process
- [ ] Create `TicketIssued` page for confirmation
- [ ]  Add validation to show error if `seatId` is missing

---
# Seat Selection
- [ ] Add DTO:
```tsx
export interface transportationSeatDto{
    id : number,
    vehicleId : number,
    row : number,
    column : number,
    isVIP : boolean,
    isAvailable : boolean,
    description : string | null,
    isReserved : boolean,
    genderId : number | null
}
```
- [ ] Add `getSeats()` method to `agent.ts`
- [ ] Add `SeatGridSelector` component for graphical seat layout in `TravelerDetailForm` and integrate it with traveler list, only for Buses
- [ ]  Modify `transportationCard` to integrate with seat selection
- [ ]  Add `SeatOnlyGridSeatMap` for simpler seat-only display
---
# Coupon Integration

- [ ] Add `couponValidationRequestDto` and `discountDto`
- [ ] Add `validateCoupon()` method to `agent.ts`
- [ ]  Update `useReservationStore` to include `couponCode`
- [ ]  Ensure `createTicketOrderDto` uses `couponCode` instead of `couponId`
- [ ]  Connect coupon validation flow in `ReviewAndConfirm`

---
# Search, Filter, and Sort Functionality

- [ ] Add filters (company) and sorting (time or price) UI to `SearchResultPage`
- [ ] Add company logo support in result cards and filters
- [ ] Add `previous/next day` buttons for time navigation
- [ ] Add remaining capacity check to transportation cards
- [ ] Add refund policy info display
- [ ] Implement showing seat map in `TransportaionCard` using `ReadOnlySeatMap` 

## 📎 Notes

- Ensure you run a full theme test after UI changes.
- Test step transitions with various invalid scenarios.
- Confirm persistent session and coupon behavior across refreshes.
- Validate all filters, sort, and search navigation works.
- Test seat selection and proper rendering of rotated layouts.

---

# Merge
- [ ] Create a PR and merge the current branch with develop

