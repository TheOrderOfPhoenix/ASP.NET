# Overview of Clean Architecture

Clean Architecture is a software design pattern that promotes separation of concerns, testability, and maintainability. It structures an application into layers, ensuring dependencies flow inwards (towards business logic) and that the core logic is independent of frameworks and external dependencies.

---

## **Clean Architecture Layers**

Clean Architecture consists of **four main layers**:

1. **Domain Layer (Core Business Rules)**
2. **Application Layer (Use Cases)**
3. **Infrastructure Layer (External Services & Data Access)**
4. **Presentation Layer (UI & API)**

### **1. Domain Layer (Enterprise Business Rules)**

- **Purpose**: Contains core business logic and rules that should be independent of frameworks and external systems.
- **Key Components**:
    - **Entities** (Aggregates, Value Objects) → Represent business models.
    - **Domain Events** → Events triggered by business logic.
    - **Domain Services** → Logic that spans multiple entities.
- **Dependencies**: No external dependencies (completely independent).
- **Example**:
    
    ```csharp
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
    
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
    ```

---

### **2. Application Layer (Use Cases & Business Logic)**

- **Purpose**: Implements application-specific business logic, coordinating workflows, and executing use cases.
- **Key Components**:
    - **Use Cases (Application Services)** → Define what the application does.
    - **Commands & Queries** → For operations (CQRS pattern).
    - **DTOs (Data Transfer Objects)** → Pass data without exposing domain models.
    - **Interfaces for Repositories & Services** → Abstract dependencies (repositories, external APIs).
- **Dependencies**:
    - Can reference **Domain Layer**.
    - No dependency on Infrastructure or Presentation layers.
- **Example (Use Case)**:
    
    ```csharp
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    
        public async Task<int> CreateProduct(CreateProductCommand command)
        {
            var product = new Product(command.Name, command.Price);
            await _productRepository.AddAsync(product);
            return product.Id;
        }
    }
    ```
    

---

### **3. Infrastructure Layer (Data & External Services)**

- **Purpose**: Provides implementations for repositories, external APIs, database access, logging, and file storage.
- **Key Components**:
    - **Repositories (EF Core, Dapper, etc.)** → Implement database operations.
    - **External Service Integrations** → Calls to third-party APIs.
    - **Logging, Email, File Storage** → External services.
- **Dependencies**:
    - References **Application Layer** (implementing interfaces).
    - No direct reference to **Presentation Layer**.
- **Example (EF Core Repository)**:
    
    ```csharp
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
    
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
    ```
    

---

### **4. Presentation Layer (UI & API)**

- **Purpose**: Handles HTTP requests, user interactions, and returns responses.
- **Key Components**:
    - **Controllers (Web API in ASP.NET Core)** → Handle HTTP requests.
    - **Views (Razor Pages, React, Blazor, etc.)** → UI rendering.
    - **DTO Mapping (AutoMapper, MediatR, etc.)** → Maps domain models to response objects.
- **Dependencies**:
    - References **Application Layer** (calls use cases).
    - Should not directly reference **Infrastructure Layer**.
- **Example (Controller in ASP.NET Core)**:
    
    ```csharp
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
    
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
    
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productId = await _productService.CreateProduct(command);
            return CreatedAtAction(nameof(CreateProduct), new { id = productId });
        }
    }
    ```
    

---

## **Dependencies Flow**

- **Presentation Layer** depends on **Application Layer**.
- **Application Layer** depends on **Domain Layer**.
- **Infrastructure Layer** depends on **Application Layer**.
- **Domain Layer** has NO dependencies.

This ensures the **business logic is central** and **not coupled** to frameworks, databases, or UI.

---

## **Example ASP.NET Core Clean Architecture Folder Structure**

```
/src
  /Domain
    /Entities
    /ValueObjects
    /DomainServices
  /Application
    /Interfaces
    /Services
    /DTOs
    /UseCases
  /Infrastructure
    /Persistence
      /Repositories
    /ExternalServices
  /Presentation
    /Controllers
    /Views (if MVC)
    /ReactApp (if using React)
```

---

# Backend Project Structure
## **Solution Name: MyApp**

📂 **MyApp.sln** (Solution file)

### **1. Presentation Layer** (📂 `MyApp.WebAPI`)
- **Project Type:** ASP .NET Core Web API - with default settings
- **Purpose:** Exposes the application via a web API.

📂 `MyApp.WebAPI`
- 📂 **Controllers** – Defines API endpoints.
- 📂 **Middlewares** – Implements custom middleware (logging, exception handling).

### Dependencies:
- Projects: 
	- Application Project
	- Domain Project 
- Packages:
	- AutoMapper
	- Microsoft.AspNetCore.Authentication.JwtBearer
	- System.IdentityModel.Tokens.Jwt
	- Microsoft.EntityFrameworkCore.Design
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools
	- Newtonsoft.Json
---

### **2. Application Layer** (📂 `MyApp.Application`)
- **Project Type:** C# Class Library - with default settings
- **Purpose:** Contains the application logic, use cases, and service abstractions.

📂 `MyApp.Application`
- 📂 **Interfaces** – Defines services like `IUserService`, `IOrderService`, etc.
- 📂 **Services** – Defines services like `IUserService`, `IOrderService`, etc.
- 📂 **DTOs** – Data Transfer Objects for input/output models.
- 📂 **Mappers** – Maps domain models to DTOs (using AutoMapper or manual mapping).
- 📂 **Validators** – Contains validation rules using FluentValidation.

### Dependencies: 
- Projects
	- Domain Project
- Packages: 
	- AutoMapper
	- Microsoft.Extensions.DependencyInjection
---

### **3. Core Domain Layer** (📂 `MyApp.Domain`)
- **Project Type:** C# Class Library - with default settings
- **Purpose:** Represents the core business logic and entities without dependencies on infrastructure or frameworks.

📂 `MyApp.Domain`
- 📂 **Aggregates** – Groups related entities following DDD principles.
- 📂 **Framework** - **Interfaces**  – Contains domain-level abstractions like repository interfaces and entity interface.
- 📂 **Framework** - **Interfaces**  – **Repositories**
- 📂 **Framework** - **Base**
- 📂 **Enums** – Defines domain-specific enumerations.
- 📂 **Factories** 
### Dependencies:
- Packages: 
	- Microsoft.Extensions.DependencyInjection
---

### **4. Infrastructure Layer** (📂 `MyApp.Infrastructure`)
- **Project Type:** C# Class Library - with default settings
- **Purpose:** Implements external dependencies such as databases, logging, APIs, and caching.

📂 `MyApp.Infrastructure`
- 📂 **Framework** - **Base** – Implements `IRepository<TEntity>` for data access.
- 📂 **Configurations** – Stores EF Core entity configurations.
- 📂 **Services** - Repositories 
- 📂 **Migrations** - Auto Created 
-  **DB-Context**

### Dependencies: 
- Projects: 
	- Domain Project
- Packages:
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Design
	- Microsoft.EntityFrameworkCore.Proxies
	- Microsoft.EntityFrameworkCore.Tools
---

### **Additional Projects (Optional)**

- 📂 `MyApp.Tests` – Unit and integration tests.
- 📂 `MyApp.Shared` – Shared utilities (cross-cutting concerns like constants, helpers).

# Acknowledgements
- ChatGPT for snippet refinement and explanations


