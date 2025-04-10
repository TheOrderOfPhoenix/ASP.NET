# 00 The unedited conversation with Chat GPT, concerning almost all of the aspects of this session:
(optional): read this to get a better understanding of the topic:
https://chatgpt.com/share/67f18460-1c1c-8010-bc57-9f3b683ec87a

# Branching

- [ ]  Create the feature/transportation-search branch based on develop

<br>

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
            int? fromCityId, int? toCityId,
            DateTime? startDate, DateTime? endDate)
{
    var query = DbContext.Transportations
                .Include(x => x.FromLocation).ThenInclude(x => x.City)
                .Include(x => x.ToLocation).ThenInclude(x => x.City)
                .Include(x => x.Company)
                .AsQueryable();
                
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

# Result & Result Status (Amin)

# IService & Service (Amin)

# Controller (Amin)

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

