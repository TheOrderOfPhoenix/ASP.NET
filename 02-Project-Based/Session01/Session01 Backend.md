
# 🛠️ Task Checklist

## Preparation 
### EF Core Code First
 - [ ] Watch this [video](https://www.youtube.com/watch?v=b8fFRX0T38M&ab_channel=PatrickGod)
## 🚧 Branching
- [ ] Create the develop branch
- [ ] Create the feature/domain-entities branch based on develop 
## `IEntity.cs`
- [ ] Create the IEntity **interface**
📂 Suggested Folder: `Domain/Framework/Interfaces`
```C#
public interface IEntity<TKey>
{
    public TKey Id { get; set; }
}
```
## `Entity.cs`
- [ ] Create the Entity **class**
📂 Suggested Folder: `Domain/Framework/Base`
```C#
public class Entity<TKey> : IEntity<TKey>
{
    public TKey Id{ get; set; }
}
```


## Create Entities 
📂 Suggested Folder: `Domain/Aggregates/[RelatedFolder]`
Entities represent the core business objects in our domain model. By defining them explicitly and consistently, we ensure that:
- Our domain logic remains **clear and maintainable**.
- We follow **Domain-Driven Design (DDD)** principles, keeping the business rules close to the data they govern.
- All developers have a **standardized structure** to follow, improving code readability and collaboration.
### Guidelines
1. **Base Class**
    - All entities (except join tables) must inherit from `Entity` and explicitly specify the datatype of their `Id`.
2. **Properties**
    - Use the **latest version of the ERD** to define properties and relationships.
3. **Reference Project**
    - For implementation details, you can refer to this project:  
        👉 [AlibabaClone-Backend Domain Layer](https://github.com/MehrdadShirvani/AlibabaClone-Backend/tree/develop/AlibabaClone.Domain/Aggregates)
4. **ERD Reference**
    - Latest ERD available here:  
        👉 [Project ERD](obsidian://open?vault=ASP.NET&file=Repo%2FProjectOrientedSessions%2Fdocs%2FAlibabaERD-Version02.pdf)### One Example:
### Example
```C#
public class Account : Entity<long>
{
    public required string PhoneNumber { get; set; }
    public required string Password { set; get; }
    public string? Email { get; set; }
    public long? PersonId { get; set; }
    //...
}
```









## Add Navigation Properties 

Navigation properties in Entity Framework Core (EF Core) **represent relationships between entities**. They allow you to **navigate** (follow) the relationships between different tables using **C# objects** instead of writing SQL joins manually.
### Add the needed navigation properties inside entities
- [ ] Figure out what navigation properties are needed based on the ERD, and use this project as a reference:
https://github.com/MehrdadShirvani/AlibabaClone-Backend/tree/develop/AlibabaClone.Domain/Aggregates
- [ ] Don't forget to mark all the navigation properties as `virtual`

### **🔹 Examples of Defining Navigation Properties**


>These are just examples, and not how the project should look like
#### **🔹 One-to-Many Example**
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

#### **🔹 One-to-One Example**

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

#### **🔹 Many-to-Many Example**

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

## Add Packages to Infrastructure Project
- [ ] Add `Microsoft.EntityFrameworkCore.Proxies` to Infrastructure Project
## 🚧 Merge
- [ ] Create a PR and merge the current branch with develop
# 🧠 Hints & Notes
- Mark navigation properties `virtual` 
# 🙌 Acknowledgements

- ChatGPT for snippet refinement and explanations

# 🔍 References
[[Session01 Additional Info]]








