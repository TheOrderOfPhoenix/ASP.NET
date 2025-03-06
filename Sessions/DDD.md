# Domain

> subject area in which we build an application

> We have some core domains and some sub domains


> Working out all domains is an iterative process 

Bounded Context -> Each subdomain has its own bounded context so that they can each have their own "language"

Context Map -> outlines which domains communicate with each other and how 

Anti-Corruption Level 

Tactical Design


Domain Objects -> 1. Entity Objects 2. Value Objects 
![[Pasted image 20250306182113.png]]
![[Pasted image 20250306182209.png]]
![[Pasted image 20250306182246.png]]
![[Pasted image 20250306182324.png]]


## **Solution Name: MyApp (or your project name)**

📂 **MyApp.sln** (Solution file)

### **1. Presentation Layer** (📂 `MyApp.API`)

- **Purpose:** Exposes the application via a web API.

📂 `MyApp.API`
- 📂 **Controllers** – Defines API endpoints.
- 📂 **Middlewares** – Implements custom middleware (logging, exception handling).


---

### **2. Application Layer** (📂 `MyApp.Application`)

- **Purpose:** Contains the application logic, use cases, and service abstractions.

📂 `MyApp.Application`
- 📂 **Interfaces** – Defines services like `IUserService`, `IOrderService`, etc.
- 📂 **Services** – Defines services like `IUserService`, `IOrderService`, etc.
- 📂 **DTOs** – Data Transfer Objects for input/output models.
- 📂 **Mappers** – Maps domain models to DTOs (using AutoMapper or manual mapping).
- 📂 **Validators** – Contains validation rules using FluentValidation.
>- 📂 **Queries** – Handles read operations (CQRS pattern).
>- 📂 **Commands** – Handles write operations (CQRS pattern).
>- 📂 **Exceptions** – Defines application-layer exceptions.
>- 📂 **UseCases** – Implements application logic (e.g., `CreateOrderHandler`).
---

### **3. Core Domain Layer** (📂 `MyApp.Domain`)

- **Purpose:** Represents the core business logic and entities without dependencies on infrastructure or frameworks.

📂 `MyApp.Domain`

- 📂 **Aggregates** – Groups related entities following DDD principles.
- 📂 **Framework** - **Interfaces**  – Contains domain-level abstractions like repository interfaces and entity interface.
- 📂 **Framework** - **Interfaces**  – **Repositories**
- 📂 **Framework** - **Base**
- 📂 **Enums** – Defines domain-specific enumerations.
- 📂 **Factories** 
>- 📂 **ValueObjects** – Defines immutable objects that don’t have an identity.
>- 📂 **Exceptions** – Custom exceptions related to domain logic.
>- 📂 **Specifications** – Encapsulates domain rules and query logic.

---

### **4. Infrastructure Layer** (📂 `MyApp.Infrastructure`)

- **Purpose:** Implements external dependencies such as databases, logging, APIs, and caching.

📂 `MyApp.Infrastructure`

- 📂 **Framework** - **Base** – Implements `IRepository<TEntity>` for data access.
- 📂 **Configurations** – Stores EF Core entity configurations.
- 📂 **Services** - Repositories 
- 📂 **Migrations** - Auto Created 
-  **DB-Context**
>- 📂 **Persistence** – Implements repositories and EF Core DbContext.
>- 📂 **Logging** – Implements logging strategies (e.g., Serilog, NLog).
>- 📂 **Identity** – Manages authentication and authorization.
>- 📂 **Caching** – Implements caching strategies (Redis, MemoryCache).
>- 📂 **Email** – Handles email services (SMTP, SendGrid).
>- 📂 **ExternalServices** – Integrations with third-party APIs.
---

### **Additional Projects (Optional)**

- 📂 `MyApp.Tests` – Unit and integration tests.
- 📂 `MyApp.Shared` – Shared utilities (cross-cutting concerns like constants, helpers).