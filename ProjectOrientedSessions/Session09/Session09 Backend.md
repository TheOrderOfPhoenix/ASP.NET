# Session 9 - Ticket Reservation
As this order is comming to an end, we are completing the functionality of application with the vital feature of reserving tickets

## Miscellaneous / Fixes

- [ ] Fix GUID generation and async handling in ticket creation. (Use `Guid.NewGuid()` instead of `new Guid()`)
```csharp
SerialNumber = Guid.NewGuid().ToString("N")
```

- [ ] Make `SerialNumber`, `TicketOrderId`, `BaseAmount` publicly settable in DTOs if they're private.
- [ ] Check `Transaction` and make sure `SerialNumber` is not a required property.

## Branching
- [ ] Create the feature/ticket-reservation branch based on develop

# Ticket Ordering System

## 🧱 Domain and Infrastructure Setup

- [ ] Check out new ERD: [Here](https://github.com/TheOrderOfPhoenix/ASP.NET/blob/main/ProjectOrientedSessions/docs/AlibabaERD-Version02.pdf)
- [ ] Add migrations for the above database changes.

## 🧑‍💼 Service Layer

- [ ] Create DTOs `CreateTravellerTicketDto` and `CreateTicketOrderDto`:
```csharp
public class CreateTravellerTicketDto
{
    public long Id { get; set; }
    public long CreatorId { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Id number is required")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "National ID number must be exactly 10 digits")]
    public required string IdNumber { get; set; }

    [Required(ErrorMessage = "Gender should be identified")]
    public required short GenderId { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Birth date is required")]
    public DateTime BirthDate { get; set; }

    public long? SeatId { get; set; }
    public bool IsVIP { get; set; }
    public string? Description { get; set; }
}
```
```csharp
public class CreateTicketOrderDto
{
    public long TransportationId { get; set; }
    public List<CreateTravellerTicketDto> MyProperty { get; set; }
}
```
Note: If you have the **Coupon** feature in your project, then add a `CouponCode` property in `CreateTicketOrderDto`.

- [ ] Modify SeatRepository to add method `GetSeatsByVehicleIdAsync`
```csharp
public Task<List<Seat>> GetSeatsByVehicleIdAsync(long vehicleId)
{
    var seats = DbSet
        .Include(s => s.Vehicle)
        .Include(s => s.Tickets).ThenInclude(t => t.Traveler)
        .Where(s => s.VehicleId == vehicleId).ToListAsync();
    return seats;
}
```
- [ ] Add Enum for **TicketStatus**, **VehicleType** and **TransactionType**
```csharp
public enum TicketStatusEnum
{
    Reserved = 1,
    Paid = 2,
    CancelledByUser = 3,
    CancelledBySystem = 4,
    Used = 5,
    Expired = 6
}
```
```csharp
public enum VehicleTypeEnum
{
    Airplane = 1,
    Train = 2,
    Bus = 3
}
```
```csharp
public enum TransactionTypeEnum
{
    Deposit = 1,
    Withdraw = 2
}
```

- [ ] Add the lock-service to lock the transportation through reservation, then register it:
```csharp
public class TransportationLockService : ITransportationLockService
{
    private readonly ConcurrentDictionary<long, SemaphoreSlim> _locks = new();

    public async Task<IDisposable> AcquireLockAsync(long transportationId)
    {
        var semaphore = _locks.GetOrAdd(transportationId, new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync();
        return new Releaser(() => semaphore.Release());
    }

    private class Releaser : IDisposable
    {
        private readonly Action _release;

        public Releaser(Action release)
        {
            _release = release;
        }

        public void Dispose()
        {
            _release();
        }
    }
}
```

- [ ] Add method `CreateAsync` in `TransportationService`
```csharp
public async Task<Result<long>> CreateAsync(long accountId, TransactionDto dto)
{
    Transaction transaction = new();
    _mapper.Map(dto, transaction);
    transaction.AccountId = accountId;

    await _transactionRepository.InsertAsync(transaction);
    await _unitOfWork.CompleteAsync();
    return Result<long>.Success(transaction.Id);
}
```

- [ ] Add method `PayForTicketOrderAsync` in `AccountService`
```csharp
public async Task<Result<long>> PayForTicketOrderAsync(long accountId, long ticketOrderId, decimal baseAmount, decimal finalAmount)
{
    var account = await _accountRepository.GetByIdAsync(accountId);
    if (account == null)
    {
        return Result<long>.Error(0, "Account not found");
    }

    if (account.Balance < finalAmount)
    {
        return Result<long>.Error(0, "Not enough money");
    }

    account.Withdraw(finalAmount);
    _accountRepository.Update(account);
    await _unitOfWork.CompleteAsync();

    TransactionDto dto = new()
    {
        CreatedAt = DateTime.UtcNow,
        Description = "Payment for ticket order #" + ticketOrderId + " at " + DateTime.UtcNow,
        BaseAmount = baseAmount,
        FinalAmount = finalAmount,
        SerialNumber = Guid.NewGuid().ToString("N"),
        TicketOrderId = ticketOrderId,
        TransactionTypeId = (int)TransactionTypeEnum.Withdraw,
        TransactionType = TransactionTypeEnum.Withdraw.ToString()
    };

    return await _transactionService.CreateAsync(accountId, dto);
}
```

- [ ] Create `ITicketOrderService`, `TicketOrderService` and implement `CreateTicketOrderAsync`
```csharp
public async Task<Result<long>> CreateTicketOrderAsync(long accountId, CreateTicketOrderDto dto)
{
    // get the account
    var account = await _accountRepository.GetByIdAsync(accountId);
    if (account == null)
    {
        return Result<long>.Error(0, "Account not found");
    }

    // get the transportation
    var transportation = await _transportationRepository.GetByIdAsync(dto.TransportationId);
    if (transportation == null)
    {
        return Result<long>.Error(0, "Transportation not found");
    }

    // lock the transportation through reservation
    using (await _transportationLockService.AcquireLockAsync(dto.TransportationId))
    {
        var baseAmount = transportation.BasePrice * dto.Travellers.Count;
        if (account.Balance < baseAmount)
        {
            return Result<long>.Error(0, "Not enough money");
        }
        // check validity of transportation
        var checkSeats = ValidateTransportationAndSeats(transportation, dto.Travellers);
        if (!string.IsNullOrEmpty(checkSeats))
        {
            return Result<long>.Error(0, checkSeats);
        }

        var finalAmount = baseAmount;
        await AssignSeatsIfDynamic(transportation.VehicleId, dto.Travellers);
        await UpsertTravellers(accountId, dto.Travellers);

        // add the ticket order by the info we have
        TicketOrder ticketOrder = new()
        {
            BuyerId = accountId,
            CreatedAt = DateTime.UtcNow,
            Description = "",
            SerialNumber = Guid.NewGuid().ToString("N"),
            TransportationId = dto.TransportationId
        };
        await _ticketOrderRepository.InsertAsync(ticketOrder);
        
        foreach (var traveller in dto.Travellers)
        {
            if (!traveller.SeatId.HasValue)
            {
                return Result<long>.Error(0, "Seat ID is required for each traveller");
            }

            Ticket ticket = new()
            {
                CreatedAt = DateTime.UtcNow,
                Description = traveller.Description,
                SeatId = traveller.SeatId.Value,
                SerialNumber = Guid.NewGuid().ToString("N"),
                TicketOrder = ticketOrder,
                TicketStatusId = 1,
                TravelerId = traveller.Id,
            };
            await _ticketRepository.InsertAsync(ticket);
        }

        await _unitOfWork.CompleteAsync();
        await _accountService.PayForTicketOrderAsync(account.Id, ticketOrder.Id,
            baseAmount, finalAmount);
        return Result<long>.Success(ticketOrder.Id);
    }
}
```
- [ ] Register `TicketOrderService` in DI container.

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

