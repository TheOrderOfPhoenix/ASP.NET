
# 6. Configurations
## Read the documentation:
https://learn.microsoft.com/en-us/ef/core/modeling/

## Create the feature/entity-configurations branch based on develop

## **1. Where Should You Place Configuration Files?**

✅ **Best Practice:** Place all configuration files in the **Infrastructure** layer.  
📂 Suggested Folder: `Infrastructure/Configurations`

Create configuration classes with this format: `[Entity]Configutaion.cs`
The class should implement the `IEntityTypeConfiguration<[Entity]>`


### **Reason:**

- The **Domain layer** should be **clean** (only entities, no database-related logic).
- The **Infrastructure layer** handles **database interactions**, so configurations belong here.

---

## **2. How to Define Keys (Primary Keys & Identity)**

You **don’t** need to explicitly define the **primary key (PK)** if you follow EF Core conventions (`Id` or `EntityNameId`). However, if you want to be explicit:

```csharp
public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id); // Explicitly defining PK (optional)

        builder.Property(t => t.Id)
               .ValueGeneratedOnAdd(); // Sets Identity (auto-increment)
    }
}
```

> 🛑 **NOTE:** If you’re using a GUID as the ID, you might need `.ValueGeneratedNever()` instead.

---

## **3. How to Define Foreign Keys?**

Use `HasOne()` and `WithMany()` for **one-to-many** relationships.

```csharp
public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        // Foreign Key - Ticket to Transportation
        builder.HasOne(t => t.Transportation) 
               .WithMany(tr => tr.Tickets)   
               .HasForeignKey(t => t.TransportationId)
               .OnDelete(DeleteBehavior.Restrict); // Optional: No cascade delete
    }
}
```

---

## **4. How to Introduce Navigation Properties with Different Names?**

If your navigation property **doesn’t match** the entity name, you should explicitly specify it using `HasOne()` and `WithMany()`.

### **Example: Ticket has a Buyer (which is an Account)**

```csharp
builder.HasOne(t => t.Buyer)  // Navigation property (Ticket → Account)
       .WithMany(a => a.TicketsBought) // Corresponding collection in Account
       .HasForeignKey(t => t.BuyerId);
```

> **Tip:** If your navigation property names don't match table names, always define them explicitly in the Fluent API.

---

## **5. How to Configure Column Types? (nvarchar, date, etc.)**

You can **manually specify column types** using `.HasColumnType()`.

### **All Strings Should Be `nvarchar` with Specific Lengths**

```csharp
builder.Property(t => t.TicketNumber)
       .IsRequired()
       .HasMaxLength(20) // Limits nvarchar length
       .HasColumnType("nvarchar(20)");
```

### **Store Some DateTime Fields as SQL `DATE` Instead of `DATETIME2`**

```csharp
builder.Property(t => t.PurchaseDate)
       .HasColumnType("date");  // Instead of default "datetime2"
```

> 🚀 **Best Practice:** Always set **string lengths** to avoid `nvarchar(MAX)`, which hurts performance.

---

## **6. How to Define Constraints? (Not Null, Length, etc.)**

Use `.IsRequired()` for **NOT NULL** and `.HasMaxLength()` for length constraints.

### **Example: Ticket Number Must Be Unique & Required**

```csharp
builder.Property(t => t.TicketNumber)
       .IsRequired() // NOT NULL
       .HasMaxLength(20)
       .HasColumnType("nvarchar(20)");

builder.HasIndex(t => t.TicketNumber)
       .IsUnique();  // Unique constraint
```

---

## **Final Configuration File Example (TicketConfiguration.cs)**

Here’s a **complete** example of a configuration file:

```csharp
public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id); // Primary Key
        builder.Property(t => t.Id).ValueGeneratedOnAdd(); // Auto-increment

        // Foreign Key Relationships
        builder.HasOne(t => t.Transportation)
               .WithMany(tr => tr.Tickets)
               .HasForeignKey(t => t.TransportationId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Buyer)
               .WithMany(a => a.TicketsBought)
               .HasForeignKey(t => t.BuyerId);

        // Column Types & Constraints
        builder.Property(t => t.TicketNumber)
               .IsRequired()
               .HasMaxLength(20)
               .HasColumnType("nvarchar(20)");

        builder.HasIndex(t => t.TicketNumber)
               .IsUnique();  // Unique constraint

        builder.Property(t => t.PurchaseDate)
               .HasColumnType("date"); // Store as DATE instead of DATETIME2
    }
}
```

---

## **Summary & Best Practices**

✅ **Store Configuration Files in:** `Infrastructure/Configurations`  
✅ **Define Foreign Keys:** Use `HasOne()` and `WithMany()`  
✅ **Explicitly Define Navigation Properties** if the names differ  
✅ **Column Types:** Use `.HasColumnType()` for `nvarchar`, `date`, etc.  
✅ **Constraints:** Use `.IsRequired()`, `.HasMaxLength()`, `.IsUnique()`

---

Now you’re all set to configure your entities properly! 🚀 Let me know if you have more questions! 😊

## Some special cases

### **1. Join Tables with Multiple IDs**

In many-to-many relationships, a join table is created to link two entities. This join table typically contains foreign keys referencing the primary keys of the two entities involved in the relationship.

#### **Example of a Join Table**

Suppose we have two entities, `Student` and `Course`, and we want to create a many-to-many relationship between them. We'll create a join table called `StudentCourses`.

#### **Entities**

```csharp
public class Student : Entity<long>
{
    public required string Name { get; set; }
    public virtual ICollection<StudentCourse> StudentCourses { get; set; }
}

public class Course : Entity<long>
{
    public required string Title { get; set; }
    public virtual ICollection<StudentCourse> StudentCourses { get; set; }
}

public class StudentCourse
{
    public long StudentId { get; set; }
    public virtual Student Student { get; set; }

    public long CourseId { get; set; }
    public virtual Course Course { get; set; }
}
```

#### **Configuration for Join Table**

You would configure the join table using the Fluent API:

```csharp
public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        // Composite Primary Key
        builder.HasKey(sc => new { sc.StudentId, sc.CourseId });

        // Foreign Key Relationships
        builder.HasOne(sc => sc.Student)
            .WithMany(s => s.StudentCourses)
            .HasForeignKey(sc => sc.StudentId);

        builder.HasOne(sc => sc.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.CourseId);
    }
}
```

#### **OnModelCreating Method in DbContext**

In your `DbContext`, you would include:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyConfiguration(new StudentCourseConfiguration());
}
```

#### **Key Points for Join Tables**

- **Composite Primary Key**: The join table uses a composite key made up of both foreign keys.
- **Navigation Properties**: This enables navigation from `Student` to `Course` and vice versa.

---

### **2. Using GUIDs That Should Be Auto-Generated**

GUIDs (Globally Unique Identifiers) can be used as primary keys in your entities. In EF Core, you can configure them to auto-generate when a new entity is created.

#### **Example Entity Using GUID**

```csharp
public class SomeEntity
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate GUID
    public string Name { get; set; }
}
```

#### **Configuration for GUID**

When configuring an entity with a GUID as the primary key, you don’t need a specific setup in the configuration, but you can enforce that the `Id` is generated on addition.

```csharp
builder.Property(e => e.Id)
    .ValueGeneratedOnAdd()
    .HasDefaultValueSql("NEWSEQUENTIALID()"); // Optionally use NEWID() for random GUID
```

#### **How It Works**

- **`Guid.NewGuid()`** generates a new GUID when a new entity instance is created.
- **Database**: If you use `NEWSEQUENTIALID()` in SQL Server, it generates sequential GUIDs, which can improve indexing performance.

#### **Example Configuration in DbContext**

Here's how you might define an entity with GUIDs in your `DbContext`:

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<SomeEntity> SomeEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SomeEntity>(builder =>
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");
        });
    }
}
```

---


## Create a PR and merge the current branch with develop
# Summary
### 📌 **Implementing Database and EF Core in Clean Architecture**

In **Clean Architecture**, everything should be **modular**, **loosely coupled**, and **separated into layers**. The database-related concerns (entities, repositories, unit of work, configurations, migrations, etc.) belong **primarily** to the **Infrastructure** and **Domain layers**.

---

## 🏛 **Layer Breakdown for Database Implementation**

|**Layer**|**Responsibility**|**Examples**|
|---|---|---|
|**Domain**|Business rules & entity models|Entities (POCOs), Value Objects, Interfaces|
|**Application**|Business logic & use cases|Services, DTOs, CQRS Handlers|
|**Infrastructure**|Data access, repositories, database configurations|EF Core, Repository, Unit of Work, Configurations|
|**Presentation**|UI/API layer, controllers, endpoints|ASP.NET Core Controllers, Razor Pages, Blazor|

---

## 🛠 **1️⃣ Defining the Domain Layer (Entities and Interfaces)**

The **Domain Layer** should contain **only business logic and entity definitions**—it should not reference Entity Framework or infrastructure concerns.

### 📌 **Entities (Domain/Entities)**

```csharp
namespace Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }

        // Navigation Property
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; } = null!;
    }
}
```

```csharp
namespace Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
```

### 📌 **Repository Interfaces (Domain/Repositories)**

```csharp
namespace Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Remove(T entity);
    }
}
```

```csharp
namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }
}
```

---

## 🏗 **2️⃣ Implementing Infrastructure Layer (EF Core, Repositories, Configurations, Migrations)**

### 📌 **Database Context (Infrastructure/Persistence/AppDbContext.cs)**

```csharp
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
```

### 📌 **Entity Configurations (Infrastructure/Persistence/Configurations)**

EF Core Fluent API is best kept in separate configuration classes.

```csharp
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CustomerName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)");
            
            builder.HasOne(t => t.Company)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CompanyId);
        }
    }
}
```

---

### 📌 **Repository Pattern (Infrastructure/Persistence/Repositories)**

```csharp
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Remove(T entity) => _dbSet.Remove(entity);
    }
}
```

### 📌 **Unit of Work Implementation**

```csharp
using Domain.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
```

---

## 🌍 **3️⃣ Configuring the Database in ASP.NET Core**

### 📌 **Connection String (appsettings.json)**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YourDb;Trusted_Connection=True;MultipleActiveResultSets=true;"
  }
}
```

### 📌 **Registering EF Core in `Program.cs`**

```csharp
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories and Unit of Work
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();
app.Run();
```

---

## 🚀 **4️⃣ Migrations and Database Setup**

### ✅ **Creating Migrations**

```bash
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Presentation
```

### ✅ **Applying Migrations**

```bash
dotnet ef database update --project Infrastructure --startup-project Presentation
```

---

## 🎯 **Summary**

|**Component**|**Layer**|**Purpose**|
|---|---|---|
|**Entities**|Domain|Business models|
|**Repository Interfaces**|Domain|Data access abstraction|
|**AppDbContext & Configurations**|Infrastructure|EF Core setup & Fluent API|
|**Repositories & UoW**|Infrastructure|Data persistence implementation|
|**Controllers & Services**|Presentation & Application|API logic|
|**Connection String**|AppSettings.json|Database configuration|

---

## 🏆 **Final Thoughts**

✅ **This follows Clean Architecture principles**  
✅ **Keeps domain logic independent of EF Core**  
✅ **Repositories and Unit of Work separate infrastructure concerns**  
✅ **DB Context and migrations handled in the right place**

Would you like to add CQRS or MediatR to this setup? 🚀