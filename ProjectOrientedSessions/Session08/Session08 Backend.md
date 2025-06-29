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

First, create an interface in Application layer:
```
public interface IUserContext
{
long GetUserId();
}
```
Then, implement it in WebAPI in Auth folder:
```
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long GetUserId()
    {
        var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (long.TryParse(userIdStr, out var userId))
        {
            return userId;
        }
        throw new InvalidOperationException("User ID is not available or invalid.");
    }
}
```
Finally, register things in Program.cs
```
// register user context
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();
```

## ✅ Profile Page (Account Info Tab)

- [ ]  Create `ProfileDto` to represent combined data for account, person, bank detail, balance.
```
public class ProfileDto
{
    // from account
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }

    // from person
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdNumber { get; set; }
    public DateTime? BirthDate { get; set; }

    // from bank-account
    public string IBAN { get; set; }
    public string BankAccountNumber { get; set; }
    public int CardNumber { get; set; }
}
```
You can also find another version of this DTO in [Here](https://github.com/MehrdadShirvani/AlibabaClone-Backend/blob/develop/AlibabaClone.Application/DTOs/Account/ProfileDto.cs)

- [ ] Add `GetProfileAsync` in `AccountRepository` and expose via `AccountController`.
    - First, map dto to aggregate
    ```
    CreateMap<Account, ProfileDto>()
	.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
	.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
	.ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
	.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person != null ? src.Person.FirstName : ""))
	.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person != null ? src.Person.LastName : ""))
	.ForMember(dest => dest.IdNumber, opt => opt.MapFrom(src => src.Person != null ? src.Person.IdNumber : ""))
	.ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Person != null ? src.Person.BirthDate : (DateTime?) null))
	.ForMember(dest => dest.IBAN, opt => opt.MapFrom(src => src.BankAccount != null ? src.BankAccount.IBAN : ""))
	.ForMember(dest => dest.BankAccountNumber, opt => opt.MapFrom(src => src.BankAccount != null ? src.BankAccount.BankAccountNumber : ""))
	.ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.BankAccount != null ? src.BankAccount.CardNumber : ""));
    ```
    - Then, add the equivalent methods for AccountRepository, AccountService and AccountController
    ```
    public class AccountRepository : BaseRepository<AlibabaDbContext, Account, long>, IAccountRepository
    {
	    public AccountRepository(AlibabaDbContext context) : base(context)
	    {

	    }

        public async Task<Account> GetByPhoneNumberAsync(string phoneNumber)
        {
		    var user = await DbSet.Include(a => a.AccountRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
		    return user;
        }

        public async Task<Account> GetProfileAsync(long accountId)
        {
            var profile = await DbSet
                .Include(a => a.Person)
                .Include(a => a.BankAccount)
                .FirstOrDefaultAsync(a => a.Id == accountId);

            return profile;
        }
    }

    ```
    
    ```
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public async Task<Result<ProfileDto>> GetProfileAsync(long accountId)
        {
            var result = await _accountRepository.GetProfileAsync(accountId);
            if (result == null)
            {
                return Result<ProfileDto>.NotFound(null);
            }

            return Result<ProfileDto>.Success(_mapper.Map<ProfileDto>(result));
        }
    }
    ```

    ```
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserContext _userContext;
        private readonly IAccountService _accountService;

        public AccountController(IUserContext userContext, IAccountService accountService)
        {
            _userContext = userContext;
            _accountService = accountService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            // get account-id from token
            long userId = _userContext.GetUserId();
            // check for user-id to be valid
            if (userId <= 0)
            {
                return Unauthorized();
            }

            var result = await _accountService.GetProfileAsync(userId);
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
    
    Note: You should add neccessary interfaces and implement them

- [ ] Implement:
    - [ ]  Edit Email (with validation): `EditEmailDto`, service method, and controller endpoint.
    - [ ] Edit Password: `EditPasswordDto`, service and controller.
    - [ ] Edit Person Info: `UpsertAccountPersonDto`, and endpoint to upsert personal data.
    - [ ] Edit BankAccountDetail: `UpsertBankAccountDetailDto` and relevant logic.

- [ ] Add mapping for all Dtos and fix related properties like `CreatorAccountId`.
    

## ✅ List of Travelers

- [ ] Add `GetPeople` endpoint in `AccountController`.
- [ ] Implement separation of `UpsertAccountPerson` and `UpsertPerson`.
- [ ] Adjust Dto: `PersonDto` (with `id`, `creatorAccountId`, `englishFirstName`, etc.).


## ✅ My Travels Tab

- [ ]  Create `TicketOrderSummaryDto` (includes cities, vehicle name, price, etc.).
- [ ] Add `GetTravels` in `AccountService`, and expose `GetMyTravels` in controller.


## ✅ My Transactions Tab

- [ ] Create `TransactionDto` and mapping.
- [ ]  Add method to get transactions by `AccountId`.
- [ ]  Expose `GetMyTransactions` in `AccountController`.
- [ ]  Add modal to simulate balance top-up (manual input).
- [ ]  Format amount text based on transaction type: green (+) for income, red (–) for expense. 
