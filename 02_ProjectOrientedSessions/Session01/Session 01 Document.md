## Branching
- [ ] Create the develop branch
- [ ] Create the feature/domain-entities branch based on develop 

## `IEntity.cs`
- [ ] Create the IEntity **interface**

> Location: Domain Project > Framework > Interfaces
```C#
public interface IEntity<TKey>
{
    public TKey Id { get; set; }
}
```
## `Entity.cs`
- [ ] Create the Entity **class**

> Location: Domain Project > Framework > Base 
```C#
public class Entity<TKey> : IEntity<TKey>
{
    public TKey Id{ get; set; }
}
```

### Why do we need IEntity and Entity? (Chat GPT):
> this approach is **valid and commonly used** in **Domain-Driven Design (DDD)** and **Clean Architecture**. It provides **consistency**, **reusability**, and **common functionality** across all entities.
## Create Entities 
> Location: Domain Project > Aggregates > (RelatedFolder)

### They all (with the exception of join tables) should inherit Entity, and you should specify the datatype of the Id
### Use the latest version of ERD to specify the properties
### You can use this project as a reference:
https://github.com/MehrdadShirvani/AlibabaClone-Backend/tree/develop/AlibabaClone.Domain/Aggregates
### Link to the ERD:
https://github.com/TheOrderOfPhoenix/ASP.NET/tree/main/02_ProjectOrientedSessions/docs

### One Example:

```C#
public class Account : Entity<long>
{
    public required string PhoneNumber { get; set; }
    public required string Password { set; get; }
    public string? Email { get; set; }
    public long? PersonId { get; set; }
}
```

## Add Navigation Properties 
### What is a navigation property?
#### **🔹 Navigation Properties in Entity Framework Core: Everything You Need to Know**

---

#### **📌 What Are Navigation Properties?**

Navigation properties in Entity Framework Core (EF Core) **represent relationships between entities**. They allow you to **navigate** (follow) the relationships between different tables using **C# objects** instead of writing SQL joins manually.

For example, if you have a **Ticket** entity related to a **Buyer**, the navigation property allows you to access the buyer from a ticket without writing a separate SQL query.

---

#### **🔹 Types of Navigation Properties**

Navigation properties can be of two types:

| Relationship Type                  | Description                                                                   |
| ---------------------------------- | ----------------------------------------------------------------------------- |
| **Reference Navigation Property**  | Represents a **single entity** related to another (one-to-one or many-to-one) |
| **Collection Navigation Property** | Represents **a list of related entities** (one-to-many or many-to-many)       |

---

#### **🔹 How to Define Navigation Properties?**

##### **🔹 One-to-Many Example**

A **Buyer** can have **multiple Tickets**, but each **Ticket** belongs to one **Buyer**.

```csharp
public class Buyer
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation Property (One Buyer → Many Tickets)
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

public class Ticket
{
    public int Id { get; set; }
    public int BuyerId { get; set; }

    // Navigation Property (Many Tickets → One Buyer)
    public virtual Buyer Buyer { get; set; }
}
```

##### **🔹 One-to-One Example**

A **Ticket** can have only **one Transaction**, and a **Transaction** belongs to exactly **one Ticket**.

```csharp
public class Ticket
{
    public int Id { get; set; }

    // One-to-One Navigation Property
    public virtual Transaction Transaction { get; set; }
}

public class Transaction
{
    public int Id { get; set; }
    public int TicketId { get; set; }

    // One-to-One Navigation Property
    public virtual Ticket Ticket { get; set; }
}
```

##### **🔹 Many-to-Many Example**

A **Buyer** can buy **many Tickets**, and each **Ticket** can be bought by **many Buyers** (if resale is allowed).

```csharp
public class Buyer
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Many-to-Many Navigation Property
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

public class Ticket
{
    public int Id { get; set; }

    // Many-to-Many Navigation Property
    public virtual ICollection<Buyer> Buyers { get; set; } = new List<Buyer>();
}
```

### Add the needed navigation properties inside entities
- [ ] Figure out what navigation properties are needed based on the ERD, and use this project as a reference:
https://github.com/MehrdadShirvani/AlibabaClone-Backend/tree/develop/AlibabaClone.Domain/Aggregates
- [ ] Don't forget to mark all the navigation properties as `virtual`

## Add a NuGet package to Infrastructure Project
- [ ] Add `Microsoft.EntityFrameworkCore.Proxies` to Infrastructure Project
## Create a pull request and merge the current branch with develop 

# Side Notes
## **Choosing the right datatype for integer values, specially IDs (Chat GPT):**

### **1️⃣ Integer Data Types (int, short, long) in C# and SQL Server**

| **C# Type** | **SQL Server Type** | **Size** | **Range**                                               |
| ----------- | ------------------- | -------- | ------------------------------------------------------- |
| `byte`      | `TINYINT`           | 1 byte   | 0 to 255                                                |
| `short`     | `SMALLINT`          | 2 bytes  | -32,768 to 32,767                                       |
| `int`       | `INT`               | 4 bytes  | -2,147,483,648 to 2,147,483,647                         |
| `long`      | `BIGINT`            | 8 bytes  | -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 |

❗ **Important Notes:**

- The **ranges are the same in C# and SQL Server** because both use the same underlying storage.
- `SMALLINT` and `TINYINT` **save space**, but be careful about hitting the limit.
- `BIGINT` is needed only if you expect **billions** of records.
---

### **2️⃣ What to Use for User ID, Ticket ID, Gender ID? (Some examples)**

| **Field**   | **Recommended C# Type** | **SQL Server Type**     | **Why?**                                                    |
| ----------- | ----------------------- | ----------------------- | ----------------------------------------------------------- |
| `UserId`    | `int` or `long`         | `INT` or `BIGINT`       | `INT` is usually enough unless expecting billions of users. |
| `TicketId`  | `int` or `long`         | `INT` or `BIGINT`       | Use `BIGINT` if expecting massive ticket volumes.           |
| `GenderId`  | `byte` or `short`       | `TINYINT` or `SMALLINT` | Gender options are limited, so `TINYINT` is sufficient.     |
| `CompanyId` | `int`                   | `INT`                   | Companies are limited, `INT` is fine.                       |
| `VehicleId` | `int`                   | `INT`                   | Use `INT`, as vehicle count is manageable.                  |
| `Price`     | `decimal(18,2)`         | `DECIMAL(18,2)`         | Avoid `float`/`double` due to rounding issues.              |

---

### **3️⃣ Should I Use GUIDs for User IDs or Ticket IDs?**

- **Use `GUID` (`UNIQUEIDENTIFIER`) for IDs only if:**
    - Data is distributed across multiple databases.
    - Security is critical (e.g., preventing sequential guessing of IDs).
- Otherwise, **stick with `int` or `long`** for performance.

📌 **Example in C# (EF Core Model):**

```csharp
public class Ticket
{
    public int TicketId { get; set; }  // Primary key
    public int UserId { get; set; }    // Foreign key
    public decimal Price { get; set; } // Use decimal for money
    public DateTime PurchaseDate { get; set; }
}
```

📌 **Fluent API (SQL Mapping)**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Ticket>()
        .Property(t => t.Price)
        .HasColumnType("DECIMAL(18,2)");
}
```



## **`required` keyword (Chat GPT) :

C# **IntelliSense** suggests adding `required` to string properties because of **nullable reference types (NRT)** introduced in **C# 8.0+**.

### **1️⃣ What Does `required` Do?**

- `required` **forces initialization** of the property when creating an object.
- It is **not a data annotation** (like `[Required]` in EF Core), but a **C# keyword** that affects **compile-time checks**.

📌 **Example Without `required`**

```csharp
public class User
{
    public string Name { get; set; }  // Warning: "Non-nullable property 'Name' is uninitialized"
}
```

🔴 **Problem**: The compiler warns that `Name` is not initialized.  
✅ **Fix**: Add `required` or initialize the property.

📌 **Example With `required`**

```csharp
public class User
{
    public required string Name { get; set; }  // No warning
}
```

✅ **Effect**: You **must** provide `Name` when creating a `User` object.

```csharp
var user = new User { Name = "Mehrdad" }; // ✅ Works
var invalidUser = new User(); // ❌ Compilation Error: Name is required
```



## **A note about strings in C# (Chat GPT) :**
**`string` is nullable in C#**, but in **nullable reference types (C# 8+), `string` is treated as non-nullable unless explicitly marked `string?`**.

- **`string`** → Default behavior (non-nullable by default in nullable context).
- **`string?`** → Explicitly nullable.



## **Why virtual navigation properties? (Chat GPT):**

- If you mark a navigation property as `virtual`, EF Core **creates a proxy class** at runtime that overrides the property and loads related data **only when accessed**.
- This is called **Lazy Loading**, meaning data is not fetched until needed.
- If you don’t mark it as `virtual`, you **must** load relationships using `.Include()` (Eager Loading).

### **What Type Should Navigation Properties Be?**

| **Scenario**                                                          | **Recommended Type**          | **Why?**                                                              |
| --------------------------------------------------------------------- | ----------------------------- | --------------------------------------------------------------------- |
| **Single reference** (e.g., `Ticket → Transportation`)                | `virtual Transportation`      | Represents a **one-to-one** or **many-to-one** relationship.          |
| **Collection of related entities** (e.g., `Transportation → Tickets`) | `virtual ICollection<Ticket>` | Best for **one-to-many** relationships, supports lazy loading.        |
| **Alternative for collections**                                       | `virtual List<Ticket>`        | Works the same, but **EF prefers `ICollection<T>`**.                  |
| **Using `IEnumerable<T>`**                                            | ❌ **Avoid**                   | EF **does not recognize** `IEnumerable<T>` for navigation properties. |
### **One Scenario to look after if using lazy loading:**
#### **What is the N+1 Query Problem?**

The **N+1 query problem** happens when EF Core **makes too many separate database queries** instead of loading data efficiently.

##### **Example Scenario**

Let’s say you have **100 tickets**, and each ticket has a related **Transportation** entity.

You run this code:

```c#
var tickets = context.Tickets.ToList(); // Loads all tickets foreach (var ticket in tickets) 
{     
	Console.WriteLine(ticket.Transportation.Name); // Lazy loads Transportation for each ticket 
}
```

##### **What Happens?**

1. **1 Query:** EF Core first loads all `Tickets`.
2. **N Queries:** Then, for each **Ticket**, EF Core makes a separate query to fetch `Transportation` (so 100 additional queries).
3. **Total Queries:** **1 + 100 = 101 queries!** 🚨 **Bad performance!**

---

