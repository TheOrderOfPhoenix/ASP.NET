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

## 🧑‍💼 Path to Service Layer

- [ ] Create DTOs `CreateTravellerTicketDto` and `CreateTicketOrderDto` and the **mappings**:
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
```csharp

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

- [ ] Create `TicketOrderController` with the endpoint `POST /CreateTicketOrder`
```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User")]
public class TicketOrderController : ControllerBase
{
    private readonly IUserContext _userContext;
    private readonly ITicketOrderService _ticketOrderService;

    public TicketOrderController(IUserContext userContext,
        ITicketOrderService ticketOrderService)
    {
        _userContext = userContext;
        _ticketOrderService = ticketOrderService;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateTicketOrder([FromBody] CreateTicketOrderDto dto)
    {
        long accountId = _userContext.GetUserId();
        // check for account-id to be valid
        if (accountId <= 0)
        {
            return Unauthorized();
        }

        var result = await _ticketOrderService.CreateTicketOrderAsync(accountId, dto);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }

        return result.Status switch
        {
            ResultStatus.Unauthorized => Unauthorized(result.ErrorMessage),
            ResultStatus.NotFound => NotFound(result.ErrorMessage),
            ResultStatus.ValidationError => BadRequest(result.ErrorMessage),
            _ => StatusCode(500, result.ErrorMessage)
        };
    }
}
```
        
## 🗃️ Repository Layer

Implement there methods in `TicketOrderRepository` and its interface
- [ ] `FindAndLoadAllDetails`
```csharp
public Task<TicketOrder?> FindAndLoadAllDetailsAsync(long id)
{
    var ticketOrder = DbSet
        .Include(to => to.Transportation).ThenInclude(t => t.FromLocation).ThenInclude(fl => fl.City)
        .Include(to => to.Transportation).ThenInclude(t => t.ToLocation).ThenInclude(tl => tl.City)
        .Include(to => to.Tickets).ThenInclude(t => t.Traveler)
        .Where(to => to.Id == id).FirstOrDefaultAsync();

    return ticketOrder;
}
```

- [ ] `GetAllByBuyerId`
```csharp
public async Task<List<TicketOrder>> GetAllByBuyerId(long buyerId)
{
    var ticketOrders = await DbSet
        .Include(to => to.Transaction)
        .Include(to => to.Transportation).ThenInclude(t => t.FromLocation)
        .Include(to => to.Transportation).ThenInclude(t => t.ToLocation)
        .Include(to => to.Transportation).ThenInclude(t => t.Company)
        .Include(to => to.Transportation).ThenInclude(t => t.Vehicle)
        .Where(to => to.BuyerId == buyerId).ToListAsync();
    return ticketOrders;
}
```

# Transportation and Seat Selection

- [ ] Add `TransportationSeatDto`.
```csharp
public class TransportationSeatDto
{
    public long Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public bool IsVIP { get; set; }
    public bool IsAvailable { get; set; }
    public string? Description { get; set; }
    public bool IsReserved { get; set; }
    public short? GenderId { get; set; }
}
```

- [ ] Add mapping from `Seat` to `TransportationSeatDto`.
```csharp
CreateMap<Seat, TransportationSeatDto>()
    .ForMember(dest => dest.IsReserved, opt => opt.MapFrom(src => src.Tickets.Any(t => t.TicketStatusId == (int)TicketStatusEnum.Reserved)))
    .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.Tickets.Any(t => t.TicketStatusId == (int)TicketStatusEnum.Reserved) ?
    src.Tickets.First(t => t.TicketStatusId == (int)TicketStatusEnum.Reserved).Traveler.GenderId : (short?)null));
```

- [ ] Add method `GetSeatsByVehicleId` in `ISeatRepository` and implement it.
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

- [ ] Add `GetTransportationSeatsAsync` in `ITransportationService` and implement.
```csharp
public async Task<Result<List<TransportationSeatDto>>> GetTransportationSeatsAsync(long transportationId)
{
    var transportation = await _transportationRepository.GetByIdAsync(transportationId);
	if (transportation == null)
	{
		return Result<List<TransportationSeatDto>>.Error(null, "Transportation not found"); 
	}

	var seats = await _seatRepository.GetSeatsByVehicleIdAsync(transportation.VehicleId);
	if (seats == null || seats.Count != 0)
	{
		return Result<List<TransportationSeatDto>>.Success(_mapper.Map<List<TransportationSeatDto>>(seats));
	}

	return Result<List<TransportationSeatDto>>.NotFound(null);
}
```

- [ ] Add `GetTransportationSeats` endpoint in `TransportationController`.
```csharp
[HttpGet("{transportationId}/seats")]
public async Task<IActionResult> GetTransportationSeats(long transportationId)
{
	var result = await _transportationService.GetTransportationSeatsAsync(transportationId);
    if (result.IsSuccess)
    {
		return Ok(result.Data);
    }

    return result.Status switch
    {
        ResultStatus.Unauthorized => Unauthorized(result.ErrorMessage),
        ResultStatus.NotFound => NotFound(result.ErrorMessage),
        ResultStatus.ValidationError => BadRequest(result.ErrorMessage),
        _ => StatusCode(500, result.ErrorMessage)
    };
}
```

- [ ] Ensure `RemainingCapacity` is treated as calculated (ignored in EF, removed from schema).
```csharp
public int RemainingCapacity => Vehicle.Capacity -
	TicketOrders?.SelectMany(to => to.Tickets)
	.Count(t => t.TicketStatusId == 1) ?? 0;
```

### ✅ Ticket Review & Confirmation

- [ ] Make sure you have `TravlerTicketDto`, mapped to `` with the details
```csharp
public class TravellerTicketDto
{
    public long Id { get; set; }
    public required string SerialNumber { get; set; }
    public required string TravellerName { get; set; }
    public DateTime BirthDate { get; set; }
    public required string SeatNumber { get; set; }
    public required string TicketStatus { get; set; }
    public string? CompanionName { get; set; }
    public string? Description { get; set; }
}
```
```csharp
CreateMap<Ticket, TravellerTicketDto>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.TravellerName, opt => opt.MapFrom(src => src.Traveler != null ? $"{src.Traveler.FirstName} {src.Traveler.LastName}" : ""))
    .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
    .ForMember(dest => dest.TicketStatus, opt => opt.MapFrom(src => src.TicketStatus.Ttile))
    .ForMember(dest => dest.CompanionName, opt => opt.MapFrom(src => src.Companion != null ? $"{src.Companion.FirstName} {src.Companion.LastName}" : ""))
    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
```

- [ ] Implement `GetTicketOrderDetails` endpoint to fetch ticket summary, make sure to create required methods as well
```csharp
[HttpGet("my-travels/{ticketOrderId}")]
public async Task<IActionResult> GetTravelDetails(long ticketOrderId)
{
    long accountId = _userContext.GetUserId();
    if (accountId <= 0)
    {
        return Unauthorized();
    }

    var result = await _accountService.GetTicketOrderDetailsAsync(accountId, ticketOrderId);
    if (result.IsSuccess)
    {
        return Ok(result.Data);
    }

    return result.Status switch
    {
        ResultStatus.Unauthorized => Unauthorized(result.ErrorMessage),
        ResultStatus.NotFound => NotFound(result.ErrorMessage),
        ResultStatus.ValidationError => BadRequest(result.ErrorMessage),
        _ => StatusCode(500, result.ErrorMessage)
    };
}
```
```csharp
public async Task<Result<List<TravellerTicketDto>>> GetTicketOrderDetailsAsync(long accoundId, long ticketOrderid)
{
    var result = await _ticketRepository.GetTicketsByTicketOrderId(ticketOrderid);
    if (result != null)
    {
        if (result.Count > 0 && result.First().TicketOrder.BuyerId != accoundId)
        {
            return Result<List<TravellerTicketDto>>.Error(null, "Account unauthorized");
        }

        return Result<List<TravellerTicketDto>>.Success(_mapper.Map<List<TravellerTicketDto>>(result));
    }

    return Result<List<TravellerTicketDto>>.NotFound(null);
}
```
```csharp
public async Task<List<Ticket>> GetTicketsByTicketOrderId(long ticketOrderId)
{
    var tickets = await DbSet
        .Include(t => t.Traveler)
        .Include(t => t.TicketStatus)
        .Include(t => t.Companion)
        .Include(t => t.Seat)
        .Include(t => t.TicketOrder)
        .Where(t => t.TicketOrderId == ticketOrderId).ToListAsync();
    return tickets;
}
```

## Merge
- [ ] Create a PR and merge the current branch with develop
