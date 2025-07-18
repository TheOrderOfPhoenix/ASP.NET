# Reservation System Development Guide - Session 10

This guide helps developers understand how to implement, update, and maintain the ticket reservation system. It summarizes changes from commits and provides step-by-step actions, grouped by feature area.
https://github.com/MehrdadShirvani/AlibabaClone-Frontend/commits/develop/

## Fixes and Missing Things from Session 08
### Authentication & Protected Routes

- [ ] Add session persistence in `authStore` to store token more persistently
```ts
import { create } from "zustand";
import { createJSONStorage, persist } from "zustand/middleware";
import { AuthResponseDto } from "@/shared/models/authentication/AuthResponseDto";

interface AuthState {
  user: AuthResponseDto | null;
  login: (user: AuthResponseDto) => void;
  logout: () => void;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      user: null,
      login: (user: AuthResponseDto) => set({ user }),
      logout: () => set({ user: null }),
    }),
    {
      name: "auth-storage", // storage key in localStorage
      storage: createJSONStorage(() => localStorage),
    }
  )
);

``` 

## Profile and User Info Enhancements

- [ ] Fix and align `birthDate` types in `PersonDto`, `TransportationSearchResult`, and `ListOfTravelers`. Make sure to use `Date | string` data type for them

## Branching

- [ ] Create the feature/themes branch based on develop

### 🎨 Theming and UI Styling

- [ ] Install and import `preline`
```bash
npm install preline
```
- [ ] Add theme colors and global styles (index.css). Take a look at their [Documentation](https://preline.co/docs/index.html)
- [ ] Add `ThemeSwitcher` and integrate it into the navbar
- [ ] If you decide to do this part after implementing pages, make sure to add theme support to the following components:
    - [ ]  `transportationCard`, `transportationSearchForm`, `ReviewAndConfirm`
    - [ ]  All modals: `LoginModal`, `RegisterModal`, `SelectFromPeopleModal`
    - [ ]  Profile section: `ProfilePage`, `ProfileSummary`, `PersonalInformation`, `AccountInfo`, `PersonalAccountInfo`, `BankAccountDetails`, `MyTravels`, `MyTransactions`, `ListOfTravelers`
    - [ ]  Reservation views and components

### Merge
- [ ] Create a PR and merge the current branch with develop

## Branching for reservation

- [ ] Create the feature/ticket-reservation branch based on develop

## Backend Agent Methods

- [ ] Add `CreateTicketOrderDto` and `CreateTravelerTicketDto`. Check backend endpoints for the dtos you need for this session
- [ ] Add `TransportationSeatDto`
- [ ] Add related methods in `TicketOrder` and add it to agent
```ts
const TicketOrder = {
  create: (data : CreateTicketOrderDto) => request.post<number>('/ticketOrder/create', data),
  downloadPdf: (ticketOrderId : number) => request.get<Blob>(`/ticketOrder/${ticketOrderId}/pdf`, {
      responseType: 'blob',
    }),
}
```

## Reservation Process & Step Management

- [ ] Implement `useReservationStore` using `zustand` to manage reservation state
```ts
import { PersonDto } from '@/shared/models/account/PersonDto';
import { CreateTravelerTicketDto } from '@/shared/models/ticketOrder/createTravelerTicketDto';
import { TransportationSearchResult } from '@/shared/models/transportation/transportationSearchResult';
import { create } from 'zustand';

const defaultTraveler: CreateTravelerTicketDto = {
  id: 0,
  creatorAccountId: 0,
  firstName: '',
  lastName: '',
  genderId: 0,
  birthDate: '',
  idNumber: '',
  phoneNumber: '',
  englishFirstName: '',
  englishLastName: '',
  isVIP: false,
  seatId: null,
  description: '',
};

type ReservationStep = "transportation" | "travelers" | "review" | "payment" | "success";

interface ReservationState {
  transportation: TransportationSearchResult | null;
  travelers: CreateTravelerTicketDto[];
  ticketOrderId: number;
  couponCode: string | null;
  paymentInfo: any;
  people: PersonDto[],
  currentStep: ReservationStep,
  isConfirmed: boolean,
  isPayed: boolean,
  isTravelerPartDone: boolean, 
  addTraveler: (traveler: CreateTravelerTicketDto) => void;
  setTravelers: (travelers: CreateTravelerTicketDto[]) => void;
  updateTraveler: (index: number, traveler: CreateTravelerTicketDto) => void;
  removeTraveler: (index: number) => void;
  setTicketOrderId: (newId: number) => void;
  setTransportation: (transportation: TransportationSearchResult) => void;
  setCouponCode: (code: string | null) => void;
  setPaymentInfo: (info: any) => void;
  setPeople: (people: PersonDto[]) => void;

  setCurrentStep: (step: ReservationStep) => void;
  setIsConfirmed: (value: boolean) => void;
  setIsPayed: (value: boolean) => void;
  setIsTravelerPartDone: (value: boolean) => void;

  resetReservation: () => void;
}

export const useReservationStore = create<ReservationState>((set) => ({
  transportation: null,
  people: [],
  travelers: [defaultTraveler],
  couponCode: null,
  paymentInfo: null,
  ticketOrderId : 0,
  currentStep: "transportation",
  isConfirmed: false,
  isPayed : false,
  isTravelerPartDone : false,
  setTicketOrderId: (newId) => set(() =>({
      ticketOrderId : newId
  })),
  addTraveler: (traveler) => set((state) => ({ travelers: [...state.travelers, traveler] })),
  setTravelers: (newTravelers) => set(() => ({
    travelers : newTravelers
  })),
  setPeople: (newPeople) => set(() => ({
    people : newPeople
  })),
  updateTraveler: (index, traveler) =>
    set((state) => {
      const updated = [...state.travelers];
      updated[index] = traveler;
      return { travelers: updated };
    }),

  removeTraveler: (index) =>
    set((state) => {
      if (state.travelers.length <= 1) return state;
      const updated = [...state.travelers];
      updated.splice(index, 1);
      return { travelers: updated };
    }),

  setTransportation: (theTransportation) => set({ transportation: theTransportation }),
  setCouponCode: (code) => set({ couponCode: code }),
  setPaymentInfo: (info) => set({ paymentInfo: info }),
  setCurrentStep: (step) => set({ currentStep: step }),

  resetReservation: () =>
    set({
      transportation: null,
      travelers: [],
      ticketOrderId: 0,
      couponCode: null,
      paymentInfo: null,
      people: [],
      currentStep: "transportation",
      isConfirmed: false,
      isPayed: false,
      isTravelerPartDone: false
    }),
    
  setIsConfirmed: (value) => set(() => ({
    isConfirmed : value
  })),
  setIsPayed: (value) => set(() => ({
    isPayed : value
  })),
  setIsTravelerPartDone: (value) => set(() => ({
    isTravelerPartDone : value
  })),
}));
```

- [ ] Create step-based routing using `ReservationLayout`. Step-based routing, in the context of contact centers or workflow management, refers to a method of directing work items (like customer service calls or support tickets) to the most appropriate agent or resource based on a series of predefined steps or conditions.

- [ ] Add routing for reservation steps in `App.tsx`

- [ ] Add `StepIndicator` component to show step progress visually (optional)
    - [ ] Add `stepGuard` logic to prevent accessing future steps prematurely
    - [ ] Add logic to skip back only if previous steps are completed

- [ ] Create `TravelerForm` to gather passenger info, with the possibility to load data from the related people of the account.

- [ ] Create `TravelerDetailsForm` to gather passengers info with `TravelerForm` integration

- [ ] Create `ReviewAndConfirm` page to review selections

- [ ] Create `PaymentForm` for transaction process

- [ ] Create `TicketIssued` page for confirmation

- [ ]  Add validation to show error if `seatId` is missing

## Seat Selection
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
- [ ] Modify `TransportationCard` to integrate with seat selection
- [ ] Add `SeatOnlyGridSeatMap` for simpler seat-only display

### Coupon Integration

- [ ] Add `CouponValidationRequestDto` and `DiscountDto`
- [ ] Add `validateCoupon()` method to `agent.ts`
- [ ] Update `useReservationStore` to include `CouponCode`
- [ ] Ensure `CreateTicketOrderDto` uses `CouponCode` instead of `couponId`
- [ ] Connect coupon validation flow in `ReviewAndConfirm`

## Search, Filter, and Sort Functionality

- [ ] Add filters (company) and sorting (time or price) UI to `SearchResultPage`
- [ ] Add company logo support in result cards and filters
- [ ] Add `previous/next day` buttons for time navigation
- [ ] Add remaining capacity check to transportation cards
- [ ] Add refund policy info display
- [ ] Implement showing seat map in `TransportaionCard` using `ReadOnlySeatMap` 

### 📎 Notes

- Ensure you run a full theme test after UI changes.
- Test step transitions with various invalid scenarios.
- Confirm persistent session and coupon behavior across refreshes.
- Validate all filters, sort, and search navigation works.
- Test seat selection and proper rendering of rotated layouts.

## Merge
- [ ] Create a PR and merge the current branch with develop
