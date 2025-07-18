# Session 7: Dependency Injection, EF Core, and Abstract Classes vs Interfaces in OOP

## 📝 Overview

This session covers key concepts of Dependency Injection (DI), Entity Framework Core (EF Core), and the differences between Abstract Classes and Interfaces in object-oriented programming.

---

## 📚 Topics Covered

### ✅ Dependency Injection (DI)

- What is DI and why it matters in OOP
- Key components: Service, Client, Interface, Injector
- Advantages and disadvantages of DI
- Types of DI: Constructor, Setter, Method, Interface injection
- Relationship to the Dependency Inversion Principle

### ✅ Entity Framework Core (EF Core)

- Cross-platform capabilities
- Modeling with POCO classes and EDM
- Querying using LINQ and raw SQL
- Change tracking and concurrency
- Transactions and caching
- Configuration and migrations

### ✅ Abstract Classes vs Interfaces

- Defining interfaces and abstract base classes
- Code reuse and shared functionality with abstract classes
- Flexibility and multiple inheritance with interfaces
- When to use interfaces, abstract classes, or a combination
- Real C# example demonstrating both

---

## 📌 Notes

### Dependency Injection (DI)

- DI reduces hardcoded dependencies by injecting required services into classes rather than creating them internally.
- Promotes loose coupling, better maintainability, and testability.
- Key players:
  - **Service:** Provides functionality
  - **Client:** Uses the service
  - **Interface:** Abstracts service implementation
  - **Injector:** Injects service instances into clients
- DI supports the **Dependency Inversion Principle** by decoupling high-level modules from low-level implementations.
- Common types:
  - **Constructor injection:** Dependencies passed via constructor parameters
  - **Setter injection:** Dependencies passed via public setter methods
  - **Method injection:** Dependencies passed through methods implementing an interface
  - **Interface injection:** Client implements interface with a method to accept dependency
- Benefits: Easier mocking/testing, centralized config, modular development
- Drawbacks: Harder debugging, potential performance impact with reflection-based DI frameworks

### Entity Framework Core (EF Core)

- EF Core is a cross-platform ORM for .NET to interact with databases using .NET objects.
- Supports LINQ queries which translate to SQL behind the scenes.
- Tracks changes to objects for efficient updates.
- Uses optimistic concurrency control to avoid overwriting data accidentally.
- Supports transactions automatically and provides first-level caching.
- Offers configuration via conventions, annotations, or Fluent API.
- Includes migration tools to evolve database schema alongside code changes.

### Abstract Classes vs Interfaces

- **Interfaces** define contracts without implementation, supporting multiple inheritance and maximum flexibility.
- **Abstract classes** allow shared code with some method implementations and force subclasses to implement abstract methods.
- Abstract classes cannot be multiply inherited in C# but reduce code duplication when shared behavior exists.
- Use interfaces when implementations differ widely or multiple inheritance is needed.
- Use abstract classes when shared logic reduces repetition.
- Combining both gives flexibility (interface for DI) and code reuse (abstract base class).
- Example provided demonstrates `IService` interface, `BaseService` abstract class with a `Log()` method, and concrete service classes overriding `Execute()`.

---

## 🧪 Practice

- Implement a small ASP.NET Core project demonstrating constructor-based DI.
- Create POCO entities and perform CRUD operations using EF Core.
- Design and implement an interface and abstract class hierarchy with shared functionality and test the differences.
- Explore swapping implementations using DI containers.

---

## 🙏 References

- [Dependency Injection - GeeksforGeeks](https://www.geeksforgeeks.org/dependency-injectiondi-design-pattern/)
- [A Quick Intro to Dependency Injection - FreeCodeCamp](https://www.freecodecamp.org/news/a-quick-intro-to-dependency-injection-what-it-is-and-when-to-use-it-7578c84fa88f/)
- [EF Core Features - StackOverflow](https://stackoverflow.com/questions/3058/what-is-inversion-of-control)
