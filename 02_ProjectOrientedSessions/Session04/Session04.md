# Preparation: 
The unedited conversation with Chat GPT, concerning almost all of the aspects of this session:
(optional): read this to get a better understanding of the topic:
https://chatgpt.com/share/67f18460-1c1c-8010-bc57-9f3b683ec87a

# Branching
- [ ]  Create the feature/transportation-search branch based on develop

# DTO
In order to develop transportation search flow, three DTOs need to be created in the application layer.

- [ ]  Create DTOs related to transportation search flow

📂 Suggested Folder: Application/DTOs/City`


```cs
public class CityDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
}
```

📂 Suggested Folder: Application/DTOs/Transportation

```cs
public class TransportationSearchRequestDto
{
     public short? VehicleTypeId { get; init; }
     public int? FromCityId { get; init; }
     public int? ToCityId { get; init; }
     public DateTime? StartDate { get; init; }
     public DateTime? EndDate { get; init; }
}
```

```cs
public class TransportationSearchResultDto
{
    public long Id { get; init; }
    public required string CompanyTitle { get; init; }   
    public required string FromLocationTitle { get; init; }
    public required string ToLocationTitle { get; init; }
    public required string FromCityTitle { get; init; }
    public required string ToCityTitle { get; init; }
    public DateTime StartDateTime { get; init; }
    public DateTime? EndDateTime { get; init; }
    public decimal Price { get; init; } 
}
```

# Repository
There are a few things to be add to some repositories for transportation search flow.

- [ ] Create DTOs related to transportation search flow

📂 Suggested Folder: Domain/Framework/Interfaces/Repositories/
TransportationRepositories

```cs
public interface ITransportationRepository : IRepository<Transportation, long>
{
    Task<IEnumerable<Transportation>> SearchTransportationsAsync(
	    short? vehicleTypeId,
        int? fromCityId,
        int? toCityId, 
        DateTime? startDate, 
        DateTime? endDate);
}
```


📂 Suggested Folder: Infrastructure/Services/Services/TransportationRepositories

```cs
public class TransportationRepository :
        BaseRepository<ApplicationDBContext, Transportation, long>,
        ITransportationRepository
    {
        public TransportationRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Transportation>> SearchTransportationsAsync(
            short? vehicleTypeId,
            int? fromCityId,
            int? toCityId,
            DateTime? startDate,
            DateTime? endDate)
        {
            var query = DbContext.Transportations
                .Include(x => x.Vehicle)
                .Include(x => x.FromLocation).ThenInclude(x => x.City)
                .Include(x => x.ToLocation).ThenInclude(x => x.City)
                .Include(x => x.Company)
                .AsQueryable();
            query = query.Where(x => vehicleTypeId == null || x.Vehicle.VehicleTypeId == vehicleTypeId.Value);
            query = query.Where(x => fromCityId == null || x.FromLocation.CityId == fromCityId.Value);
            query = query.Where(x => toCityId == null || x.ToLocation.CityId == toCityId.Value);
            query = query.Where(x => startDate == null || x.StartDateTime.Date == startDate.Value.Date);
            query = query.Where(x => endDate == null || (x.EndDateTime.HasValue && x.EndDateTime.Value == endDate.Value.Date));
            
            return await query.ToListAsync();
        }
    }
```


# Auto Mapper
Auto Mapper simplifies mapping between aggregates and DTOs in both directions.

- [ ] Create a `MappingProfile` that inherits `Profile`, and use it to add configurations for mappings

📂 Suggested Folder: Application/Mappers/Profiles

```cs
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transportation, TransportationSearchResultDto>()
            .ForMember(dest => dest.CompanyTitle, 
            opt => opt.MapFrom(src => src.Company.Title))
            
            .ForMember(dest => dest.FromLocationTitle, 
            opt => opt.MapFrom(src => src.FromLocation.Title))
            
            .ForMember(dest => dest.ToLocationTitle, 
            opt => opt.MapFrom(src => src.ToLocation.Title))
            
            .ForMember(dest => dest.FromCityTitle, 
            opt => opt.MapFrom(src => src.FromLocation.City.Title))
            
             .ForMember(dest => dest.ToCityTitle, 
             opt => opt.MapFrom(src => src.ToLocation.City.Title));

        CreateMap<City, CityDto>();
        }
    }
```

- [ ] Register AutoMapper config file in `Program.cs` 

```cs
 .
 .
 .

 builder.Services.AddAutoMapper(typeof(MappingProfile));
 
 var app = builder.Build();

 .
 .
 .
```

# Result & Result Status

- [ ] Create `ResultStatus` enum and `Result` class
📂 Suggested Folder: Application/Result


Result is a template to transfer data between services and controllers (in backend), so will use a generic type

```cs
public class Result<T>
{
	public ResultStatus Status { get; set; }
	public string? ErrorMessage { get; set; }
	public T? Data { get; set; }
	public bool IsSuccess => Status == ResultStatus.Success;

	public static Result<T> Success(T data)
	{
		return new Result<T>
		{
			Status = ResultStatus.Success,
			Data = data
		};
	}

	public static Result<T> Error(T data)
	{
		return new Result<T>
		{
			Status = ResultStatus.Error,
			Data = data
		};
	}

	public static Result<T> NotFound(T data)
	{
		return new Result<T>
		{
			Status = ResultStatus.NotFound,
			Data = data
		};
	}
}
```

As you can see, there's a property of type ResultStatus, which is a enum for status of request

```cs
public enum ResultStatus
{
	Success,
	NotFound,
	ValidationError,
	Conflict,
	Unauthorized,
	Forbidden,
	Error
}
```

You can read more about enums: [W3Schools](https://www.w3schools.com/cs/cs_enums.php)


# IService & Service

Now, use `I[Entity]Repositry` and `IUnitOfWork` in services to implement business logic

- [ ] Create `I[Entity]Service` and `[Entity]Service` which implements it

📂 Suggested Folder for `I[Entity]Service`: Application/Interfaces

📂 Suggested Folder for Services: Application/Services


- existence of an interface for each service class is optional
- services can have multiple repositories in them -> logic-based structure

An example of `I[Entity]Service`:

```c#
public interface ITransportationService
{
    Task<Result<IEnumerable<TransportationSearchResultDto>>> SearchTransportationsAsync(TransportationSearchRequestDto searchRequest);
}
```

An example of `[Entity]Service`:

```cs
public class TransportationService : ITransportationService
{
	private readonly ITransportationRepository _transportationRepository;
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public TransportationService(ITransportationRepository transportationRepository, IMapper mapper, IUnitOfWork unitOfWork)
	{
		_transportationRepository = transportationRepository;
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<IEnumerable<TransportationSearchResultDto>>> SearchTransportationsAsync(TransportationSearchRequestDto requestDto)
	{
		var result = await _transportationRepository.SearchTransportationsAsync(
			vehicleTypeId: requestDto.VehicleTypeId,
			fromCityId: requestDto.FromCityId,
			toCityId: requestDto.ToCityId,
			startDateTime: requestDto.StartDate,
			endDateTime: requestDto.EndDate);

		if (result.Any())
		{
			var dto = _mapper.Map<IEnumerable<TransportationSearchResultDto>>(result);
			return Result<IEnumerable<TransportationSearchResultDto>>.Success(dto);
		}

		return Result<IEnumerable<TransportationSearchResultDto>>.NotFound(null);
	}
}
```

- [ ] Register services in `Program.cs`:
```c#
.
.
.
builder.Services.AddScoped<ITransportationService, TransportationService>();
builder.Services.AddScoped<ICityService, CityService>();
.
.
.
```

# Controller
Now we're getting to endpoints, you should communicate with client side through web-api. So every controller uses Services in Application layer to receive requests and send responses with DTOs.

- [ ] Create an APIController (right click on the folder, and then under Add, select Controller, and then make sure to select the APIController type)
📂 Suggested Folder: WebAPI/Controller

You should use ```[ApiController]``` attribute on top of them, route them and handle different status codes. TransportationController:

```cs
[ApiController]
[Route("api/[controller]")]
public class TransportationController : ControllerBase
{
	private readonly ITransportationService _transportationService;

	public TransportationController(ITransportationService transportationService)
	{
		_transportationService = transportationService;
	}

	[HttpGet("search")]
	public async Task<IActionResult> SearchTransportations([FromQuery] TransportationSearchRequestDto searchRequest)
	{
		if (searchRequest == null)
		{
			return BadRequest("Invalid search request");
		}

		var result = await _transportationService.SearchTransportationsAsync(searchRequest);
		if (result.IsSuccess)
		{
			return Ok(result);
		}

		// any unsuccessful status
		return result.Status switch
		{
			ResultStatus.NotFound => NotFound(result.ErrorMessage),
			ResultStatus.ValidationError => BadRequest(result.ErrorMessage),
			_ => StatusCode(500, result.ErrorMessage),
		};
	}
}
```

- HttpGet: handles a GET request from client -> important for routing
- Ok, BadRequest, NotFound and StatusCode are Json results to send through api
- Use TransportationService to communicate with Application

# Inserting Sample Data 

For testing purposes, add some data into the related tables. 
You are provided with a SQL script, that adds some sample data into the following tables

 **Important Notes:** Note that different database names, and different table names will produce errors while executing the script. Consider adjusting these names before executing the script
 
- Cities 
- Companies 
- LocationTypes
- Locations 
- VehicleTypes 
- Vehicles
- Transportation

- [ ] Open `TransportationRelatedSampleData.sql` with SSMS, and execute the query


# Merge
- [ ] Create a PR and merge the current branch with develop


# Additional Info
### 1. **Should I have an `IEntityService` and then `EntityService` for each of my entities?**

Not necessarily **for _every_** entity — only if it **makes sense**.

- The Application Layer should expose **use cases** — not just CRUD logic for each entity.
    
- If an entity has business logic or interactions that need orchestration (e.g., validations, aggregations, calling repositories, etc.), then **yes**, create a service.
    
- Otherwise, for basic operations, **directly using a repository (via a unit of work or interface)** from the use case handler might be fine.
### 2. **Is it OK to have services not related to a specific entity?**

Absolutely, **yes**. In fact, that’s expected in a Clean Architecture setup.

Examples:

- A `ReportGenerationService` that combines bookings, customers, and payments.
    
- A `TokenService` for authentication tokens.
    
- A `CurrencyConversionService` that hits an external API.
    
- A `NotificationService` that sends emails or SMS.
    

👉 As long as these services **live in the Application Layer** and follow **dependency inversion** (i.e., they depend only on interfaces, not implementations), you’re doing great.

### 3. **Is it necessary to have an interface for each service?**



### 🔹 **What’s the Difference Between Services and Repositories?**

| Aspect             | **Service**                                                            | **Repository**                                                  |
| ------------------ | ---------------------------------------------------------------------- | --------------------------------------------------------------- |
| **Layer**          | Application Layer                                                      | Domain Layer (interface), Infrastructure Layer (implementation) |
| **Responsibility** | **Orchestrates business logic** / use cases                            | **Data access abstraction**                                     |
| **Focus**          | Coordinates multiple domain/repo operations, validation, business flow | Fetching/storing data for a specific entity                     |
| **Example**        | `PlaceOrderService`, `ReportService`                                   | `ICustomerRepository`, `IOrderRepository`                       |

### What does `init` mean?

`init` is an **access modifier for properties** that allows you to **set a property only during object initialization**, **but not after**.


## Should I use `class` or `record` for DTOs in Clean Architecture?

### 🔵 Short answer:

> **Use `record` for DTOs when possible** — it's clean, immutable by default, and semantically perfect for data transfer.

---

### 🔍 Why `record` is a great fit for DTOs

| Feature                 | `record`          | `class`                      |
| ----------------------- | ----------------- | ---------------------------- |
| Immutable by default    | ✅ (with `init`)   | ❌ (need manual setup)        |
| Value-based equality    | ✅                 | ❌ (ref-based by default)     |
| Concise syntax          | ✅                 | ❌ (more boilerplate)         |
| Use for data containers | ✅ (perfect fit)   | ✅ (but more verbose)         |
| Custom behavior/logic   | ❌ (less suitable) | ✅ (better for rich behavior) |
### But when should you prefer `class`?

Use `class` if your DTO or model:

- Needs to be **mutable** after creation
    
- Has to **interact with legacy APIs/libraries**
    
- Needs **inheritance or polymorphism** (not well supported in `record`)
    
- Has **rich behavior** (logic, methods, validation, etc.)
    

> For example, in the Domain Layer (Entities, ValueObjects), you'll usually stick to **`class`** — because that's where behavior lives.

## RESTful APIs:
https://aws.amazon.com/what-is/restful-api/#:~:text=RESTful%20API%20is%20an%20interface,applications%20to%20perform%20various%20tasks.

## What Conditions Make an API RESTful?

### Key Principles of REST:

1. **Statelessness**:
    
    - Each API call must contain all the information the server needs to fulfill the request (no session state). Each request is independent.
        
2. **Resource Identification**:
    
    - Resources (e.g., customers, orders) should be identified using URIs. Use nouns in URIs, not verbs.
        
3. **HTTP Methods**:
    
    - Use standard HTTP methods to represent actions:
        
        - **GET**: Retrieve a resource.
            
        - **POST**: Create a new resource.
            
        - **PUT**: Update a resource entirely.
            
        - **PATCH**: Update a resource partially.
            
        - **DELETE**: Remove a resource.
            
4. **Use of Standard Status Codes**:
    
    - Return appropriate HTTP status codes (e.g., `200 OK`, `201 Created`, `404 Not Found`, `500 Internal Server Error`).
        
5. **HATEOAS**:
    
    - (Hypermedia as the Engine of Application State) - Provide links to related resources within the responses.
        

### Multiple GET Methods in One Controller:

- **Yes, you can have multiple GET methods in one controller**. The key is to differentiate them based on routes and parameters.
    
- For example:
    

```c#
[ApiController] 
[Route("api/[controller]")] 
public class CustomerController : ControllerBase {     
	
	[HttpGet("{id}")]     
	public IActionResult GetCustomerById(int id) { /*...*/ }      
	
	[HttpGet]     
	public IActionResult GetAllCustomers() { /*...*/ }      
	
	[HttpGet("{id}/orders")]     
	public IActionResult GetCustomerOrders(int id) { /*...*/ } 
}
```


- **Routing**: ASP.NET Core uses route templates to differentiate these actions. The combination of route parameters, query strings, and action names can help separate the GET requests.


## List-like stuff in `C#`
Absolutely, let’s go over the main “list-like” data types in C#. They all serve similar purposes—holding multiple items—but differ in functionality, performance, and use cases. Here’s a detailed breakdown:

🔷 1. `IEnumerable`

- Namespace: System.Collections.Generic
    
- Most basic "list-like" abstraction.
    
- Read-only (forward-only iteration).
    
- You can use foreach on it.
    
- Doesn’t support indexing (no .Count, no [i]).
    
- Often used as the return type to expose a stream of data without giving full collection control.
    

Example:

```csharp
IEnumerable<int> numbers = GetNumbers(); // Lazy-loaded maybe
foreach (var num in numbers)
    Console.WriteLine(num);
```

💡 Ideal when:

- You want to return a sequence without exposing modification.
    
- You’re using LINQ chains.
    
- You’re returning data from a database query.
    

---

🔷 2. ICollection

- Extends IEnumerable.
    
- Adds Count and Add/Remove/Clear methods.
    
- Still abstract—List and HashSet implement it.
    

💡 Useful when:

- You want to expose a collection that can be modified (e.g. Add or Remove).
    
- You care about the Count.
    

---

🔷 3. IList

- Extends ICollection and IEnumerable.
    
- Adds index access: list[0] etc.
    
- Think of it like a mutable array with dynamic size.
    

💡 Use when:

- You want ordered collection with indexing.
    
- You need to insert, remove, or replace items at specific positions.
    

---

🔷 4. List

- A concrete class (not interface).
    
- Implements IList, ICollection, IEnumerable.
    
- Backed by an array (auto-resizes).
    
- Fast read and write.
    
- Supports Add, Remove, Insert, IndexOf, etc.
    

Example:

```csharp
var list = new List<string>();
list.Add("One");
list.Add("Two");
var second = list[1]; // "Two"
```

💡 Go-to general purpose collection.

---

🔷 5. IReadOnlyCollection & IReadOnlyList

- IReadOnlyCollection: Just Count and IEnumerable.
    
- IReadOnlyList: Adds indexing without modification.
    
- Used to expose lists safely (read-only).
    

💡 Used when:

- You want to return a list, but prevent any changes.
    

---

🔷 6. Array (T[])

- Fixed-size.
    
- Fastest for indexing.
    
- Cannot change size.
    
- Implements IList (via Array).
    

Example:

```csharp
int[] numbers = new int[5];
numbers[0] = 42;
```

💡 Great for performance-sensitive or fixed-size data.

---

🔷 7. ObservableCollection

- For WPF/Blazor/WinForms data-binding.
    
- Notifies UI when items are added/removed.
    
- Implements IList.
    

💡 Use in UI apps when the view needs to react to collection changes.

---

🔷 8. HashSet

- Unordered, no duplicates.
    
- Implements ICollection, not IList.
    
- No index access.
    

💡 Best for fast membership checking (contains x).

---

🔷 Summary Table

|Type|Indexing|Modifiable|Ordered|Duplicates|Use Case|
|---|---|---|---|---|---|
|IEnumerable|❌|❌|✔️|✔️|Basic read-only stream or LINQ chaining|
|ICollection|❌|✔️|✔️|✔️|Expose modifiable group of items|
|IList|✔️|✔️|✔️|✔️|Full-featured list abstraction|
|List|✔️|✔️|✔️|✔️|Standard go-to collection|
|IReadOnlyList|✔️|❌|✔️|✔️|Return data safely from service or repo|
|Array (T[])|✔️|✔️|✔️|✔️|Fixed-size, performant indexing|
|ObservableCollection|✔️|✔️|✔️|✔️|UI binding for collections|
|HashSet|❌|✔️|❌|❌|Fast lookup, uniqueness enforcement|

—

If you tell me the scenario (e.g. search results, modifying a cart, populating a drop-down), I can recommend the best type for it. Want that?
