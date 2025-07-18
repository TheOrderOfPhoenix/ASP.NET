

# 🛠️ Task Checklist
## 🚧 Branching (Configurations)
- [ ] Create the feature/entity-configurations branch based on develop
## Preparation 
- [ ] Read the [documentation](https://learn.microsoft.com/en-us/ef/core/modeling/):
## Create configuration classes
📂 Suggested Folder: `Infrastructure/Configurations`
- [ ] Create the classes with this format: `[Entity]Configutaion.cs`
- [ ] The class should implement the `IEntityTypeConfiguration<[Entity]>`

### Where Should You Place Configuration Files?
✅ **Best Practice:** Place all configuration files in the **Infrastructure** layer.  
#### **Reason:**
- The **Domain layer** should be **clean** (only entities, no database-related logic).
- The **Infrastructure layer** handles **database interactions**, so configurations belong here.
### Use this as a reference:
[reference](https://github.com/MehrdadShirvani/AlibabaClone-Backend/tree/develop/AlibabaClone.Infrastructure/Configurations)
### Explanations and Details

#### **How to Define Keys (Primary Keys & Identity)**

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
#### **How to Define Foreign Keys?**

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

#### **How to Introduce Navigation Properties with Different Names?**

If your navigation property **doesn’t match** the entity name, you should explicitly specify it using `HasOne()` and `WithMany()`.

#### **Example: Ticket has a Buyer (which is an Account)**

```csharp
builder.HasOne(t => t.Buyer)  // Navigation property (Ticket → Account)
       .WithMany(a => a.TicketsBought) // Corresponding collection in Account
       .HasForeignKey(t => t.BuyerId);
```

> **Tip:** If your navigation property names don't match table names, always define them explicitly in the Fluent API.

---

#### **How to Configure Column Types? (nvarchar, date, etc.)**

You can **manually specify column types** using `.HasColumnType()`.

#### **All Strings Should Be `nvarchar` with Specific Lengths**

```csharp
builder.Property(t => t.TicketNumber)
       .IsRequired()
       .HasMaxLength(20) // Limits nvarchar length
       .HasColumnType("nvarchar(20)");
```

#### **Store Some DateTime Fields as SQL `DATE` Instead of `DATETIME2`**

```csharp
builder.Property(t => t.PurchaseDate)
       .HasColumnType("date");  // Instead of default "datetime2"
```

---
#### **How to Define Constraints? (Not Null, Length, etc.)**

Use `.IsRequired()` for **NOT NULL** and `.HasMaxLength()` for length constraints.

##### **Example: Ticket Number Must Be Unique & Required**

```csharp
builder.Property(t => t.TicketNumber)
       .IsRequired() // NOT NULL
       .HasMaxLength(20);

builder.HasIndex(t => t.TicketNumber)
       .IsUnique();  // Unique constraint
```

---


#### **Join Tables with Multiple IDs**
In many-to-many relationships, a join table is created to link two entities. This join table typically contains foreign keys referencing the primary keys of the two entities involved in the relationship.


Suppose we have two entities, `Student` and `Course`, and we want to create a many-to-many relationship between them. We'll create a join table called `StudentCourses`.

##### **Entities**

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

##### **Configuration for Join Table**

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

### Final Configuration File Example (`TicketConfiguration.cs`)

Here’s an example of a configuration file:

```csharp
public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.TicketOrderId)
            .IsRequired();

        builder.Property(t => t.SeatId)
            .IsRequired();

        builder.Property(t => t.TravelerId)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.CanceledAt)
            .IsRequired(false);

        builder.Property(t => t.CompanionId)
            .IsRequired(false);

        builder.Property(t => t.TicketStatusId)
            .IsRequired();

        builder.Property(t => t.SerialNumber)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.HasIndex(x => x.SerialNumber).IsUnique();


        builder.Property(t => t.Description)
            .HasMaxLength(200)
            .IsUnicode(false);

        // Relationships
        builder.HasOne(t => t.TicketOrder)
            .WithMany(t => t.Tickets)
            .HasForeignKey(t => t.TicketOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Seat)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.SeatId)
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
## 🚧  Merge
- [ ] Create a PR and merge the current branch with develop

## 🚧 Branching (Application `DBContext` and Connection String Configurations)

## Preparation 
- [ ] Read the [documentation](https://learn.microsoft.com/en-us/ef/core/modeling/):

## Branching
- [ ] Create the feature/setup-dbContext branch based on develop

## Database Context
📂 Suggested Folder: `Infrastructure/ApplicationDbContext.cs
- [ ] Create ApplicationDBContext 
	- [ ] Inherits DbContext
	- [ ] Create the constructor like the code below
	- [ ] Add the necessary DbSets
	- [ ] Override `OnModelCreating` and `OnConfiguring` as below
```csharp
public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Gender> Genders{ get; set; }
    public DbSet<BankAccountDetail> BankAccountDetails{ get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Role> Roles { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<City> Cities { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<LocationType> LocationTypes{ get; set; }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Coupon> Coupons{ get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketOrder> TicketOrders { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }
    public DbSet<Transportation> Transportations { get; set; }

    public DbSet<Seat> Seats { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Persian_100_CI_AI");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}
```

---
## Configuring the Database in ASP.NET Core

### 📌 **Connection String**
- [ ] Modify `appsettings.json` and add the following. 
- [ ] Adjust the Connection String to meet your needs
### Option 1:
```json
{
  "ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YourDb;User Id=USERNAME;Password=PASSWORD;TrustServerCertificate=True;"
  }
}
```
### Option 2:
```json
{
  "ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YourDb;Integrated Security=True;TrustServerCertificate=True;"
	}
}
```
- [ ] Put this `appsettings.json` in **`gitignore`**
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

## 🚧  Merge
- [ ] Create a PR and merge the current branch with develop
---
## 🚧Branching (Migrations and Database Setup)
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
## 🚧Merge
- [ ] Create a PR and merge the current branch with develop

# 🧠 Hints & Notes
# 🙌 Acknowledgements

- ChatGPT for snippet refinement and explanations
# 🔍 References
[[Session02 Additional Info]]


