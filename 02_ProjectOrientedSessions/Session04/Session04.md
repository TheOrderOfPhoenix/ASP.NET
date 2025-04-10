# 00 The unedited conversation with Chat GPT, concerning almost all of the aspects of this session:
(optional): read this to get a better understanding of the topic:
https://chatgpt.com/share/67f18460-1c1c-8010-bc57-9f3b683ec87a

# Branching

- [ ]  Create the feature/transportation-search branch based on develop



# DTO
In order to develop transportation search flow, three DTOs need to be created in the application layer.
<br>
- [ ]  Create DTOs related to transportation search flow
<br>

📂 Suggested Folder: Application/DTOs/City`
<br>

```cs
public class CityDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
}
```
<br>
📂 Suggested Folder: Application/DTOs/Transportation

<br>

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
<br>

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
<br><br>

# Repository
There are a few things to be add to some repositories for transportation search flow.
<br>

- [ ] Create DTOs related to transportation search flow

<br>

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
<br>

📂 Suggested Folder: Infrastructure/Services/Services/TransportationRepositories
```cs
public async Task<IEnumerable<Transportation>> SearchTransportationsAsync(
			short? vehicleTypeId,
            int? fromCityId, int? toCityId,
            DateTime? startDate, DateTime? endDate)
{
    var query = DbContext.Transportations
			    .Include(x => x.Vehicle)
                .Include(x => x.FromLocation).ThenInclude(x => x.City)
                .Include(x => x.ToLocation).ThenInclude(x => x.City)
                .Include(x => x.Company)
                .AsQueryable();
    query = query.Where(x => vehicleTypeId == null || x.Vehicle.VehicleTypeId == vehicleTypeId.Value);
    
    query = query.Where
    (x => fromCityId == null || x.FromLocation.CityId == fromCityId.Value);
    
    query = query.Where
    (x => toCityId == null || x.ToLocation.CityId == toCityId.Value);
    
    query = query.Where
    (x => startDate == null || x.StartDateTime.Date == startDate.Value.Date);
    
    query = query.Where
    (x => endDate == null ||
    (x.EndDateTime.HasValue && x.EndDateTime.Value == endDate.Value.Date));
            
    return await query.ToListAsync();
}
```
<br><br>

# Auto Mapper
Auto Mapper simplifies mapping between aggregates and DTOs in both directions.
<br>

- [ ] Config AutoMapper

<br>

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
<br>


📂 Suggested Folder: WebAPI

**Program.cs**
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


📂 Suggested Folder: Application/Result

# IService & Service

- [ ] Use repositories and UnitOfWork in services to implement business logic


- existance of an interface for each service class is optional
- services can have multiple repositories in them -> logic-based structure

here's an example:

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

📂 Suggested Folder: Application/Services

- [ ] To handle dependency injection in Program.cs:
```cs
#region Register Services
builder.Services.AddScoped<ITransportationService, TransportationService>();
builder.Services.AddScoped<ICityService, CityService>();
#endregion
```

# Controller

- [ ] Now we're getting to endpoints, you should communicate with client side through web-api. So every controller uses Services in Application layer to recieve requests and send responses with DTOs.

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

To test your project, run it and use Swagger or Postman

# Inserting Sample Data 

> For testing purposes, add some data into the related tables. 
> You are provided with a SQL script, that adds some sample data into the following tables

 > Important Notes: Note that different database names, and different table names will produce errors while executing the script. Consider adjusting these names before executing the script
 
 - Cities 
- Companies 
- LocationTypes
- Locations 
- VehicleTypes 
- Vehicles
- Transportation

- [ ] Open `TransportationRelatedSampleData.sql` with SSMS, and execute the query

# Test -> Postman 

