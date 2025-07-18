## Cardinality
### Cardinality in Database Relationships

Cardinality in databases refers to the number of relationships between records in two tables. It defines how many instances of one entity can be associated with instances of another entity. Cardinality is a crucial concept in database design because it ensures data integrity and optimizes query performance.

---

### **Types of Cardinality**

1. **One-to-One (1:1)**
    
    - Each record in Table A is related to exactly one record in Table B, and vice versa.
    - Example: A _person_ has one _passport_, and a _passport_ belongs to only one _person_.
    - Implementation: Typically enforced with a **unique foreign key**.
        
2. **One-to-Many (1:M)**
    
    - A record in Table A can have multiple related records in Table B, but a record in Table B is linked to only one record in Table A.
    - Example: A _customer_ can place multiple _orders_, but each _order_ is placed by only one _customer_.
    - Implementation: A **foreign key** in Table B referring to the primary key in Table A.
        
3. **Many-to-Many (M:M)**
    
    - Multiple records in Table A can relate to multiple records in Table B.
    - Example: _Students_ enroll in multiple _courses_, and each _course_ has multiple _students_.
    - Implementation: A **junction (bridge) table** with foreign keys referencing both tables.

---

### **Cardinality Constraints**

Cardinality can be further specified using **minimum and maximum** constraints:

- **(0,1): Optional One** → A record may or may not be related.
- **(1,1): Mandatory One** → A record must always be related to exactly one record.
- **(0,N): Optional Many** → A record may have many related records or none.
- **(1,N): Mandatory Many** → A record must have at least one related record.

---

### **Practical Example**

Consider a database with `Students` and `Courses`:

- **One-to-Many:** A _teacher_ teaches multiple _courses_, but each _course_ has only one _teacher_.
- **Many-to-Many:** _Students_ enroll in multiple _courses_, and _courses_ have multiple _students_. This is implemented using a **StudentCourses** junction table.
## Using GUIDs That Should Be Auto-Generated

GUIDs (Globally Unique Identifiers) can be used as primary keys in your entities. In EF Core, you can configure them to auto-generate when a new entity is created.

### **Example Entity Using GUID**

```csharp
public class SomeEntity
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate GUID
    public string Name { get; set; }
}
```

### **Configuration for GUID**

When configuring an entity with a GUID as the primary key, you don’t need a specific setup in the configuration, but you can enforce that the `Id` is generated on addition.

```csharp
builder.Property(e => e.Id)
    .ValueGeneratedOnAdd()
    .HasDefaultValueSql("NEWSEQUENTIALID()"); // Optionally use NEWID() for random GUID
```

### **How It Works**

- **`Guid.NewGuid()`** generates a new GUID when a new entity instance is created.
- **Database**: If you use `NEWSEQUENTIALID()` in SQL Server, it generates sequential GUIDs, which can improve indexing performance.

### **Example Configuration in DbContext**

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
## `AccountRole` 

 The **accountRole** table is a **many-to-many join table** with only two foreign keys (`AccountId`, `RoleId`) and no extra fields. Since it's just linking **Accounts** and **Roles**, you might not need a repository for it. Let's explore the best approach.

### **Option 1: No Separate Repository (Preferred)**

Since EF Core **automatically** manages many-to-many relationships using `DbSet<Account>` and `DbSet<Role>`, you usually **don’t need a repository** for the join table.

You can simply work with navigation properties in **AccountRepository** and **RoleRepository**:

#### **Example: Adding a Role to an Account**

```csharp
public async Task AssignRoleToAccountAsync(int accountId, int roleId)
{
    var account = await _context.Accounts
        .Include(a => a.Roles) // Load roles
        .FirstOrDefaultAsync(a => a.Id == accountId);

    var role = await _context.Roles.FindAsync(roleId);

    if (account != null && role != null)
    {
        account.Roles.Add(role);
        await _context.SaveChangesAsync();
    }
}
```

EF Core **automatically inserts into the join table** when you modify the `Roles` collection.

---

### **Option 2: Create a Repository for the Join Table (If Needed)**

If you **need direct control over the join table** (e.g., custom queries, logging, performance tuning), then a repository may be useful.

#### **Interface for `AccountRole` Repository**

Since the join table **doesn't behave like a typical entity**, we can define a custom repository:

```csharp
public interface IAccountRoleRepository
{
    Task AddAsync(int accountId, int roleId);
    Task RemoveAsync(int accountId, int roleId);
    Task<bool> ExistsAsync(int accountId, int roleId);
}
```

#### **Implementation**

```csharp
public class AccountRoleRepository : IAccountRoleRepository
{
    private readonly AppDbContext _context;

    public AccountRoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(int accountId, int roleId)
    {
        var accountRole = new AccountRole { AccountId = accountId, RoleId = roleId };
        _context.AccountRoles.Add(accountRole);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int accountId, int roleId)
    {
        var accountRole = await _context.AccountRoles
            .FirstOrDefaultAsync(ar => ar.AccountId == accountId && ar.RoleId == roleId);

        if (accountRole != null)
        {
            _context.AccountRoles.Remove(accountRole);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int accountId, int roleId)
    {
        return await _context.AccountRoles
            .AnyAsync(ar => ar.AccountId == accountId && ar.RoleId == roleId);
    }
}
```

---

### **When Should You Use a Repository for the Join Table?**

✔ **If you need to execute custom queries** (e.g., checking if an account has a role).  
✔ **If you need to add business logic** when assigning/removing roles.  
✔ **If the join table will have extra fields** (e.g., `DateAssigned`, `IsActive`).

🚀 **If the join table is purely a linking table, let EF Core handle it automatically through navigation properties.** Otherwise, use a repository for more control.

---

