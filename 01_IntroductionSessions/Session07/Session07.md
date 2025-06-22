# Dependency injection
Dependency injection is a technique used in object-oriented programming ([OOP](https://www.techtarget.com/searchapparchitecture/definition/object-oriented-programming-OOP)) to reduce the hardcoded dependencies between objects. A dependency in this context refers to a piece of [code](https://www.techtarget.com/whatis/definition/code) that relies on another resource to carry out its intended function. Often, that resource is a different object in the same application.

Dependencies within an OOP application enable objects to perform their assigned tasks by providing additional functionality. For example, an application might include two class definitions: Class A and Class B. As part of its definition, Class B creates an instance of Class A to carry out a specific task, which means that Class B is dependent on Class A to carry out its function. The dependency is hardcoded into the Class B definition, resulting in code that is tightly coupled. Such code is more difficult to test, modify or reuse than [loosely coupled](https://www.techtarget.com/searchnetworking/definition/loose-coupling) code.

Instead of the dependency being hardcoded, it can be injected through a mechanism such as a class constructor or public property. In this scenario, Class A gets passed into Class B via a parameter, rather than Class B creating the object itself. Class B can then be compiled without including the entire Class A definition, resulting in a class that functions independently of its dependencies. The result is code that is more readable, maintainable, testable, reusable and flexible than tightly coupled code.

Dependency inversion is of particular importance when it comes to dependency injection. Dependency inversion focuses on decoupling and [abstracting](https://www.techtarget.com/whatis/definition/abstraction) code, rather than relying too heavily on concretions, which are hardcoded concrete implementations. Dependency inversion also ensures that high-level modules do not depend on low-level modules.

Dependency injection supports the dependency inversion principle by [injecting dependencies into the class definitions](https://www.theserverside.com/video/Dependency-injection-in-Spring) instead of hardcoding them. In this way, it abstracts the details and ensures that high-level modules don't depend on low-level modules.

1. **Service.** A class that carries out some type of functionality. Any object can be either a service or client. Which one it is depends on the role the object has in a particular injection.
2. **Client.** A class that requests something from a service. A client can be any class that uses a service.
3. **Interface.** A component implemented by a service for use by one or more clients. The component enables the client to access the service's functions, while abstracting the details of about how the service implements those functions, thus breaking dependencies between lower and higher classes.
4. **Injector.** A component that introduces a service to a client. The injector creates a service instance and then inserts the service into a client. The injector can be many objects working together.


## Advantages of dependency injection

Many development teams use dependency injection because it offers several important benefits:

- Code modules do not need to instantiate references to resources, and dependencies can be easily swapped out, even mock dependencies. By enabling the framework to do the resource creation, configuration data is centralized, and updates occur only in one place.
- Injected resources can be customized through [Extensible Markup Language](https://www.techtarget.com/whatis/definition/XML-Extensible-Markup-Language) files outside the source code. This enables changes to be applied without having to recompile the entire codebase.
- Programs are more testable, maintainable and reusable because the client classes do not need to know how dependencies are implemented.
- Developers working on the same application can build classes independently of each other because they only need to know how to use the interfaces to the referenced classes, not the workings of the classes themselves.
- Dependency injection helps in [unit testing](https://www.techtarget.com/searchsoftwarequality/definition/unit-testing) because configuration details can be saved to configuration files. This also enables the system to be reconfigured without recompiling.

## Disadvantages of dependency injection

Although dependency injection can be beneficial, it also comes with several challenges:

- Dependency injection makes troubleshooting difficult because much of the code is pushed into an unknown location that creates resources and distributes them as needed across the application.
- Debugging code when misbehaving objects are buried in a complicated third-party framework can be frustrating and time-consuming.
- Dependency injection can slow integrated development environment automation, as dependency injection frameworks use either reflection or dynamic programming.


## Types of dependency injection

OOP supports the following [approaches to dependency injection](https://www.theserverside.com/video/Constructor-injection-vs-setter-injection-in-Spring-Boot?_gl=1*j2jgxa*_ga*Njg3MzcwOTcxLjE3MzA4NzQxNjM.*_ga_TQKE4GS5P9*MTczOTM1NTQ2NS45LjEuMTczOTM1NTUxOS4wLjAuMA..):

- **Constructor injection.** An injector uses a class constructor to inject the dependency. The referenced object is passed in as a parameter to the constructor.
- **Setter (property) injection.** The client exposes a setter method that the injector uses to pass in the dependency.
- **Method injection.** A client class is used to implement an interface. A [method](https://www.techtarget.com/whatis/definition/method) then provides the dependency, and an injector uses the interface to supply the dependency to the class.
- **Interface injection.** An injector method, provided by a dependency, injects the dependency into another client. Clients then need to implement an interface that uses a setter method to accept the dependency.


https://stackoverflow.com/questions/130794/what-is-dependency-injection




# EF Core
## Entity Framework Features

- **Cross-platform:** EF Core is a cross-platform framework which can run on Windows, Linux and Mac.
- **Modelling:** EF (Entity Framework) creates an EDM (Entity Data Model) based on POCO (Plain Old CLR Object) entities with get/set properties of different data types. It uses this model when querying or saving entity data to the underlying database.
- **Querying:** EF allows us to use LINQ queries (C#/VB.NET) to retrieve data from the underlying database. The database provider will translate this LINQ queries to the database-specific query language (e.g. SQL for a relational database). EF also allows us to execute raw SQL queries directly to the database.
- **Change Tracking:** EF keeps track of changes occurred to instances of your entities (Property values) which need to be submitted to the database.
- **Saving:** EF executes INSERT, UPDATE, and DELETE commands to the database based on the changes occurred to your entities when you call the `SaveChanges()` method. EF also provides the asynchronous `SaveChangesAsync()` method.
- **Concurrency:** EF uses Optimistic Concurrency by default to protect overwriting changes made by another user since data was fetched from the database.
- **Transactions:** EF performs automatic transaction management while querying or saving data. It also provides options to customize transaction management.
- **Caching:** EF includes first level of caching out of the box. So, repeated querying will return data from the cache instead of hitting the database.
- **Built-in Conventions:** EF follows conventions over the configuration programming pattern, and includes a set of default rules which automatically configure the EF model.
- **Configurations:** EF allows us to configure the EF model by using data annotation attributes or Fluent API to override default conventions.
- **Migrations:** EF provides a set of migration commands that can be executed on the NuGet Package Manager Console or the Command Line Interface to create or manage underlying database Schema.

https://stackoverflow.com/questions/3058/what-is-inversion-of-control

https://www.freecodecamp.org/news/a-quick-intro-to-dependency-injection-what-it-is-and-when-to-use-it-7578c84fa88f/


# Why use abstract class in the examples?


## **1. Scenario Overview**

Let’s say we have an interface `IService`, and three different classes (`ServiceA`, `ServiceB`, `ServiceC`) that share common functionality.

### **Approach 1: Each Class Implements the Interface Directly**
![[Pasted image 20250212171204.png]]
Each class provides its own implementation of `Execute()`. **No shared code exists among them**.

---

### **Approach 2: Using an Abstract Base Class**

If there’s common functionality across `ServiceA`, `ServiceB`, and `ServiceC`, we can introduce an **abstract class**:


```c#
public interface IService
{
    void Execute();
}

public abstract class BaseService : IService
{
    public void Log() => Console.WriteLine("Logging action"); // Common functionality

    public abstract void Execute(); // Forces derived classes to implement this
}

public class ServiceA : BaseService
{
    public override void Execute()
    {
        Log();
        Console.WriteLine("Executing Service A");
    }
}

public class ServiceB : BaseService
{
    public override void Execute()
    {
        Log();
        Console.WriteLine("Executing Service B");
    }
}

public class ServiceC : BaseService
{
    public override void Execute()
    {
        Log();
        Console.WriteLine("Executing Service C");
    }
}
```

Here, `BaseService` provides **shared functionality** (e.g., `Log()`) so that `ServiceA`, `ServiceB`, and `ServiceC` don’t have to repeat the same logic.

---

## **2. Comprehensive Comparison**

| Feature                          | Interface Only                                                                  | Abstract Base Class                                                          |
| -------------------------------- | ------------------------------------------------------------------------------- | ---------------------------------------------------------------------------- |
| **Forces contract adherence**    | ✅ Yes, all classes must implement the methods                                   | ✅ Yes, but can provide default behavior                                      |
| **Allows multiple inheritance**  | ✅ Yes (C# supports multiple interfaces)                                         | ❌ No (C# doesn’t support multiple inheritance for classes)                   |
| **Allows shared implementation** | ❌ No, each class must provide its own implementation                            | ✅ Yes, common functionality can be placed in the base class                  |
| **Flexibility**                  | ✅ More flexible; any class can implement it without worrying about a base class | ❌ Less flexible; forces all classes to derive from the base class            |
| **Scalability**                  | ✅ Easy to scale and extend                                                      | ✅ Good if many classes share behavior                                        |
| **Testability**                  | ✅ Can be mocked easily                                                          | ✅ Can be mocked, but harder if logic is mixed                                |
| **Encapsulation**                | ❌ No default behavior                                                           | ✅ Can provide reusable, hidden logic                                         |
| **Dependency Injection (DI)**    | ✅ Works well with DI                                                            | ✅ Works well, but makes DI a little more complex if constructor logic exists |

---

## **3. When to Use Each Approach**

### **Use Only Interfaces When:**

✔️ **You need maximum flexibility** – Any class can implement `IService` without being tied to a base class.  
✔️ **Each class has very different implementations** – If `ServiceA`, `ServiceB`, and `ServiceC` have nothing in common besides the method signature.  
✔️ **You might need multiple inheritance** – Since C# **doesn’t support multiple class inheritance**, an interface allows a class to implement multiple behaviors.  
✔️ **You’re focusing on dependency inversion** – Interfaces work well with DI and keep components loosely coupled.

### **Use an Abstract Base Class When:**

✔️ **There’s shared behavior across implementations** – If all service classes require a `Log()` method or any other common logic.  
✔️ **You want partial implementation** – The base class can implement default behavior while forcing subclasses to implement required methods.  
✔️ **The hierarchy makes sense logically** – If all services are conceptually similar and should extend a base class.  
✔️ **You want to reduce code duplication** – The base class can contain reusable logic.

---

## **4. Hybrid Approach: Combining Both**

Sometimes, you might **use both an interface and an abstract class** for more flexibility:

csharp

CopyEdit

```c#
public interface IService
{
    void Execute();
}

public abstract class BaseService : IService
{
    public void Log() => Console.WriteLine("Logging action");

    public abstract void Execute();
}

public class ServiceA : BaseService
{
    public override void Execute()
    {
        Log();
        Console.WriteLine("Executing Service A");
    }
}
```

This way:

- Clients can depend on **IService** (for DI flexibility).
- `BaseService` provides common behavior **without enforcing its use**.

---

## **5. Conclusion**

- ✅ **Use interfaces** when **flexibility and loose coupling** are most important.
- ✅ **Use abstract classes** when **code reuse and a shared structure** are beneficial.
- ✅ **Use both** when you want **dependency inversion with shared functionality**.

Would you like a real-world ASP.NET Core example, like how DI containers work with abstract classes vs. interfaces? 🚀


![[Pasted image 20250212184706.png]]