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



