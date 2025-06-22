
# 1. Configurations
## Preparation 
- [ ] Read the documentation:
https://learn.microsoft.com/en-us/ef/core/modeling/

## Branching
- [ ] Create the feature/entity-configurations branch based on develop

## Where Should You Place Configuration Files?

✅ **Best Practice:** Place all configuration files in the **Infrastructure** layer.  
📂 Suggested Folder: `Infrastructure/Configurations`
### **Reason:**

- The **Domain layer** should be **clean** (only entities, no database-related logic).
- The **Infrastructure layer** handles **database interactions**, so configurations belong here.

 
## Create configuration classes
- [ ] Create the classes with this format: `[Entity]Configutaion.cs`
- [ ] The class should implement the `IEntityTypeConfiguration<[Entity]>`
## Examples and Details


### **Use this as a reference:**
https://github.com/MehrdadShirvani/AlibabaClone-Backend/tree/develop/AlibabaClone.Infrastructure/Configurations
---

### **How to Define Keys (Primary Keys & Identity)**

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

### **How to Define Foreign Keys?**

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

### **How to Introduce Navigation Properties with Different Names?**

If your navigation property **doesn’t match** the entity name, you should explicitly specify it using `HasOne()` and `WithMany()`.

### **Example: Ticket has a Buyer (which is an Account)**

```csharp
builder.HasOne(t => t.Buyer)  // Navigation property (Ticket → Account)
       .WithMany(a => a.TicketsBought) // Corresponding collection in Account
       .HasForeignKey(t => t.BuyerId);
```

> **Tip:** If your navigation property names don't match table names, always define them explicitly in the Fluent API.

---

### **How to Configure Column Types? (nvarchar, date, etc.)**

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

### **How to Define Constraints? (Not Null, Length, etc.)**

Use `.IsRequired()` for **NOT NULL** and `.HasMaxLength()` for length constraints.

### **Example: Ticket Number Must Be Unique & Required**

```csharp
builder.Property(t => t.TicketNumber)
       .IsRequired() // NOT NULL
       .HasMaxLength(20);

builder.HasIndex(t => t.TicketNumber)
       .IsUnique();  // Unique constraint
```

---

### **Final Configuration File Example (TicketConfiguration.cs)**

Here’s a **complete** example of a configuration file:

```csharp
public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TransportationId)
            .IsRequired();

        builder.Property(t => t.SeatId)
            .IsRequired();

        builder.Property(t => t.BuyerId)
            .IsRequired();

        builder.Property(t => t.TravelerId)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.CompanionId)
            .IsRequired(false);

        builder.Property(t => t.TicketStatusId)
            .IsRequired();

        builder.Property(t => t.SerialNumber)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(t => t.Description)
            .HasMaxLength(200)
            .IsUnicode(false);

        // Relationships
        builder.HasOne(t => t.Transportation)
            .WithMany(t => t.Tickets)
            .HasForeignKey(t => t.TransportationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Seat)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Buyer)
            .WithMany(a => a.BoughtTickets)
            .HasForeignKey(t => t.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Traveler)
            .WithMany(p => p.TraveledTickets)
            .HasForeignKey(t => t.TravelerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Companion)
            .WithMany()
            .HasForeignKey(t => t.CompanionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.TicketStatus)
            .WithMany()
            .HasForeignKey(t => t.TicketStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

---

### **Summary & Best Practices**

✅ **Store Configuration Files in:** `Infrastructure/Configurations`  
✅ **Define Foreign Keys:** Use `HasOne()` and `WithMany()`  
✅ **Explicitly Define Navigation Properties** if the names differ  
✅ **Column Types:** Use `.HasColumnType()` for `nvarchar`, `date`, etc.  
✅ **Constraints:** Use `.IsRequired()`, `.HasMaxLength()`, `.IsUnique()`

---



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


## Merge
- [ ] Create a PR and merge the current branch with develop

# 2. Application DBContext and ConnectionString Configurations

## Preparation 
- [ ] Read the documentation:
https://learn.microsoft.com/en-us/ef/core/modeling/

## Branching
- [ ] Create the feature/setup-dbContext branch based on develop

## Database Context
- [ ] Create ApplicationDBContext 
	- [ ] Location: Infrastructure/ApplicationDbContext.cs
	- [ ] Inherits DbContext
	- [ ] Create the constructor like the code below
	- [ ] Add the Needed DbSets
	- [ ] Override `OnModelCreating` and `OnConfiguring` as below

```csharp
using AlibabaClone.Domain.Aggregates.AccountAggregates;

using Microsoft.EntityFrameworkCore;

namespace AlibabaClone.Infrastructure
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Gender> Genders{ get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Role> Roles { get; set; }

		//... Add other DbSets as well

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

```



---

## Configuring the Database in ASP.NET Core

### 📌 **Connection String **
- [ ] Modify `appsettings.json` and add the following. (They might have a type or something... search the web to make sure)

- [ ] Adjust the ConnectionString to meet your needs
### Option 1:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YourDb;User Id=USERNAME;Password=PASSWORD;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```
### Option 2:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YourDb;Integrated Security=TRUE;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```
- [ ] Put this `appsettings.json` in **`gitignore`** if you think is needed 
### **Registering EF Core in `Program.cs`**
- [ ] Modify `Program.cs`
 
```csharp
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.Run();
```
## Merge
- [ ] Create a PR and merge the current branch with develop
---

# Migrations and Database Setup
## Branching
- [ ] Create the feature/migrations branch based on develop

## Using Package Manager Console 
- [ ] Make sure to set the project to Infrastructure
- [ ] Make sure you have installed Microsoft.EntityFrameworkCore.Tools
- [ ] Run the following command
```
Add-Migration InitialCreate
```
- [ ] In case of succus:
```
Update-Database
```
## Merge
- [ ] Create a PR and merge the current branch with develop
---

