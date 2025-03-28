# Introduction to Repository Pattern 
The **Repository Pattern** in ASP.NET Core is a design pattern used to separate business logic from data access logic by providing an abstraction layer over database operations. This pattern improves maintainability, testability, and flexibility in applications by encapsulating database operations in dedicated repository classes.

---
## **Why Use the Repository Pattern?**

### **Pros:**

1. **Abstraction from ORM (Entity Framework Core)**
    
    - Prevents direct dependency on EF Core, making it easier to swap out the data access layer in the future.
        
2. **Better Code Organization**
    
    - Separates concerns by keeping data logic in repositories and business logic in services/controllers.
        
3. **Improved Testability**
    
    - Makes it easier to mock repositories in unit tests.
        
4. **Encapsulation of Queries**
    
    - Common queries can be abstracted, reducing repetition.
        
5. **Centralized Data Access Logic**
    
    - Ensures a single location for handling CRUD operations.
        

---

## **Comparison: Repository Pattern vs. Direct DbSet Operations**

|Feature|Using Repository Pattern|Using DbSet Directly in Controllers|
|---|---|---|
|**Separation of Concerns**|✅ Maintains separation|❌ Business and data access logic mixed|
|**Testability**|✅ Easy to mock and test|❌ Harder to mock DbContext|
|**Code Reusability**|✅ Common operations are encapsulated|❌ Repetitive DbSet calls|
|**Flexibility**|✅ Can switch database providers easily|❌ Tightly coupled to EF Core|

---

## **Implementation of the Repository Pattern with Unit of Work**

We will create:

1. **IRepository** (Generic repository interface)
    
2. **Repository** (Generic repository implementation)
    
3. **IUnitOfWork** (Handles transaction management)
    
4. **UnitOfWork** (Implementation for database commit operations)
    
5. **Entity-specific repositories** (e.g., `ICustomerRepository` and `CustomerRepository`)
    

---

### **Step 1: Create a Generic Repository Interface**

```csharp
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
```

---

### **Step 2: Implement the Generic Repository**

```csharp
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
```

---

### **Step 3: Create an Entity-Specific Repository Interface**

```csharp
public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync();
}
```

---

### **Step 4: Implement the Entity-Specific Repository**

```csharp
public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync()
    {
        return await _dbSet.Include(c => c.Orders).ToListAsync();
    }
}
```

---

### **Step 5: Create the Unit of Work Interface**

```csharp
public interface IUnitOfWork : IDisposable
{
    ICustomerRepository Customers { get; }
    Task<int> SaveChangesAsync();
}
```

---

### **Step 6: Implement the Unit of Work**

```csharp
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private CustomerRepository _customerRepository;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public ICustomerRepository Customers => 
        _customerRepository ??= new CustomerRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

---

### **Step 7: Register Dependencies in `Program.cs`**

```csharp
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
```

---

### **Step 8: Use in a Service or Controller**

```csharp
public class CustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _unitOfWork.Customers.GetAllAsync();
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();
    }
}
```

---

## **Conclusion**

- The **Repository Pattern** with **Unit of Work** ensures clean architecture, better maintainability, and testability.
    
- It abstracts **DbSet** operations and allows for easier database switching.
    
- The **Unit of Work** manages transactional consistency by coordinating multiple repositories.
    

Would you like modifications, such as adding specifications or pagination? 🚀

# Introduction to Unit of Work Pattern
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

## Create one interface for each entity, and name it `I[Entity]Repository` 
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
