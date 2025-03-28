# Introduction to Repository Pattern (Chat GPT):
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

| Feature                    | Using Repository Pattern               | Using DbSet Directly in Controllers    |
| -------------------------- | -------------------------------------- | -------------------------------------- |
| **Separation of Concerns** | ✅ Maintains separation                 | ❌ Business and data access logic mixed |
| **Testability**            | ✅ Easy to mock and test                | ❌ Harder to mock DbContext             |
| **Code Reusability**       | ✅ Common operations are encapsulated   | ❌ Repetitive DbSet calls               |
| **Flexibility**            | ✅ Can switch database providers easily | ❌ Tightly coupled to EF Core           |

---

## **Implementation of the Repository Pattern**

We will create:

1. **IRepository** (Generic repository interface)
    
2. **Repository** (Generic repository implementation)
    
3. **Entity-specific repositories** (e.g., `ICustomerRepository` and `CustomerRepository`)


# Introduction to Unit of Work Pattern  (Chat GPT):

## **What is the Unit of Work Pattern?**

The **Unit of Work (UoW)** pattern is a **centralized mechanism** to manage **database transactions** and ensure that multiple repository operations are treated as a single unit of execution. It acts as a wrapper around multiple repositories to **coordinate their changes and commit them in one go**.

---

## **Advantages of Unit of Work**

### **1. Single Transaction for Multiple Operations**

- If you're performing **multiple database operations** across different repositories, **Unit of Work ensures atomicity**.
- If one operation fails, everything is **rolled back** (when using explicit transactions).    

### **2. Better Performance**

- **Without UoW:** Every repository would call `SaveChangesAsync()` separately, causing multiple round trips to the database.
- **With UoW:** All changes are saved **at once**, reducing the number of database calls.
### **3. Maintains Consistency**
- When multiple repositories modify related entities, **UoW ensures that all changes are either committed or discarded together**.
### **4. Improves Testability**
- Unit of Work allows you to **mock database changes** and write unit tests efficiently without worrying about inconsistent data states.
### **5. Prevents Partial Updates**
- If multiple repositories handle different entities in the same operation, calling `SaveChangesAsync()` in individual repositories could lead to **partial updates** if one operation succeeds and another fails.

---

## **Why Should `SaveChanges()` NOT Be in the Repository?**

### **1. Each Repository Should Not Control Transactions**

If each repository calls `SaveChangesAsync()`, **you lose control over transactions**.

#### **Example Problem (Without UoW)**

Imagine you have two repositories: `CustomerRepository` and `OrderRepository`.  
If you try to **add a customer** and **add an order** separately, each calling `SaveChangesAsync()`:

```csharp
var customer = new Customer { Name = "John Doe" };
await _customerRepository.AddAsync(customer);
await _customerRepository.SaveChangesAsync(); // ❌ First database call

var order = new Order { CustomerId = customer.Id, TotalAmount = 100 };
await _orderRepository.AddAsync(order);
await _orderRepository.SaveChangesAsync(); // ❌ Second database call
```

**What happens if the second `SaveChangesAsync()` fails?**

- The customer has already been saved, but the order is missing.
- **Your database is left in an inconsistent state!**

### **2. Database Round Trips (Performance Issue)**

If each repository calls `SaveChangesAsync()`, you end up with **multiple database calls** instead of batching them into a single transaction.

```csharp
await _customerRepository.SaveChangesAsync(); // ❌ DB call
await _orderRepository.SaveChangesAsync(); // ❌ Another DB call
```

Using **Unit of Work**, all changes can be saved in one go:

```csharp
await _unitOfWork.SaveChangesAsync(); // ✅ One database call
```

This **reduces network latency** and improves **database performance**.
    
### **3. Promotes Separation of Concerns**

- **Repositories should focus on CRUD operations** (data retrieval and manipulation).
- **Unit of Work should manage transactions**.
- This makes the code **cleaner and easier to maintain**.
---

## **Key Takeaways**

✔ **Unit of Work ensures all database operations are part of a single transaction**.  
✔ **Repositories should NOT call `SaveChangesAsync()` to avoid multiple transactions**.  
✔ **EF Core tracks changes, so calling `SaveChangesAsync()` once is enough**.  
✔ **Using UoW improves performance, consistency, and maintainability**.
---

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
