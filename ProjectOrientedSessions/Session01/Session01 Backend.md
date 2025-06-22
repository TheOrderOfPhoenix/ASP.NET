# Branching
- [ ] Create the develop branch
- [ ] Create the feature/domain-entities branch based on develop 

# `IEntity.cs`
- [ ] Create the IEntity **interface**

> Location: Domain Project > Framework > Interfaces
```C#
public interface IEntity<TKey>
{
    public TKey Id { get; set; }
}
```
# `Entity.cs`
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
# Create Entities 
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

# Add Navigation Properties 
### What is a navigation property? (the following pieces of code are just there for educational purposes)
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

# Add Packages to Infrastructure Project
- [ ] Add `Microsoft.EntityFrameworkCore.Proxies` to Infrastructure Project
# Merge
- [ ] Create a PR and merge the current branch with develop

