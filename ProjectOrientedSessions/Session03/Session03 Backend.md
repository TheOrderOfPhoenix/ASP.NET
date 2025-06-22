
# 1. Implementing Repository Pattern
## Branching
- [ ] Create the feature/repositories branch based on develop

## Create `IRepository` in Domain 
- [ ] Create the interface and add the following code

📂 Suggested Folder: `Domain/Framework/Interfaces/Respositories`

```c#
public interface IRepository<T_Entity, U_PrimaryKey> where T_Entity : class
{
    Task<T_Entity?> GetByIdAsync(U_PrimaryKey id); 
    Task<IEnumerable<T_Entity>> GetAllAsync();
    Task<IEnumerable<T_Entity>> FindAsync(Expression<Func<T_Entity, bool>> predicate);
    Task AddAsync(T_Entity entity);
    void Update(T_Entity entity);
    void Remove(T_Entity entity);
}
```

## Create a class implementing `IRepository`
- [ ] create a class named `BaseRepository` or `Repository` (choose one) in Infrastructure and implement `IRepository`

📂 Suggested Folder: `Infrastructure/Framework/Base`

- [ ] provide method definitions for the methods 

```c#
public class BaseRepository<K_DbContext, T_Entity, U_PrimaryKey> : IRepository<T_Entity, U_PrimaryKey>
                                                                      where T_Entity : class
                                                                      where K_DbContext : DbContext
{
    public virtual K_DbContext DbContext { get; set; }
    public virtual DbSet<T_Entity> DBSet{ get; set; }
    
    public BaseRepository(K_DbContext dbContext)
    {
        DbContext = dbContext;
        DBSet = dbContext.Set<T_Entity>();
    }
    
    public async Task AddAsync(T_Entity entity)
    {
        await DBSet.AddAsync(entity);
    }
    
    public async Task<T_Entity?> GetByIdAsync(U_PrimaryKey id)
    {
        return await DBSet.FindAsync(id);
    }
    public async Task<IEnumerable<T_Entity>> GetAllAsync()
    {
        var entityList = DBSet.ToListAsync();
        return await entityList;
    }
    public void Update(T_Entity entity)
    {
        DBSet.Update(entity);
    }
    public void Remove(T_Entity entity)
    {
        DBSet.Remove(entity);
    }

    public async Task<IEnumerable<T_Entity>> FindAsync(Expression<Func<T_Entity, bool>> predicate)
    {
        return await DBSet.Where(predicate).ToListAsync();
    }
}
```

## Create one interface for each entity(except the join tables for now), and name it `I[Entity]Repository` 
- [ ] For each entity, create an interface that inherits `IRepository`
- [ ] (Optional): Add method definitions as you deem needed for that entity (not recommended right now. We will come back to this part later)

📂 Suggested Folder: Domain/Framework/Base/Interfaces/{Related Folder}`
for example:
```c#
public interface IAccountRepository : IRepository<Account, long>
{

}
```

### Reference Project:
https://github.com/MehrdadShirvani/AlibabaClone-Backend/tree/develop/AlibabaClone.Domain/Framework/Interfaces/Repositories
## Create one class for each entity, implementing the `I[Entity]Repository`

📂 Suggested Folder: Infrastructure/Services/{Related Folder}

- [ ] For each entity, create an class named `[Entity]Repository` that implements `I[Entity]Repository` and inherits `BaseRepository` 

for example:
```c#
public class AccountRepository :
    BaseRepository<ApplicationDBContext, Account, long>,
    IAccountRepository
{
    public AccountRepository(ApplicationDBContext dbContext) : base(dbContext)
    {

    }
}
```
### Reference Project:
https://github.com/MehrdadShirvani/AlibabaClone-Backend/tree/develop/AlibabaClone.Infrastructure/Services
## Registering Services
- [ ] Modify `Program.cs` in Presentation, and register for each `I[Entity]Repository` the related `[Entity]Repository`

```c#
//some code
//Register Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IGenderRepository, GenderRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationTypeRepository, LocationTypeRepository>();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketStatusRepository, TicketStatusRepository>();
builder.Services.AddScoped<ITransportationRepository, TransportationRepository>();

builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
//some code
```
## Merge
- [ ] Create a PR and merge the current branch with develop


# 2. Implementing Unit of Work Pattern

## Branching
- [ ] Create the feature/UnitOfWork branch based on develop


```c#
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
}
```

## Create `IUnitOfWork` in Domain 
- [ ] Create the interface that inherits `IDisposable` and add the following code

📂 Suggested Folder: `Domain/Framework/Interfaces`

```c#
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
}
```

## Create a class implementing `IUnitOfWork`
- [ ] create a class named `UnitOfWork`  in Infrastructure and implement `IUnitOfWork`

📂 Suggested Folder: `Infrastructure/Framework/Base`

- [ ] provide method definitions for the methods

```c#
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;
    
    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
```

## Registering Services
- [ ] Modify `Program.cs` in Presentation, and register the `UnitOfWork` Service

```c#
//some code
//Register Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//some code
```

## Merge
- [ ] Create a PR and merge the current branch with develop
