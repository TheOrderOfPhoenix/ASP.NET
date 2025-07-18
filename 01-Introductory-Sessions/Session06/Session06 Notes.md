# Session 6: SOLID Principles and Dependency Injection (DI)

## 📝 Overview

In this session, we will learn two foundational concepts for writing clean, maintainable, and scalable software:

- **SOLID Principles:** Five key design principles that help create better object-oriented designs.
- **Dependency Injection (DI):** A design pattern to manage class dependencies, improve modularity, and facilitate testing.

---

## 📚 Topics Covered

### ✅ SOLID Principles

- Single Responsibility Principle (SRP)
- Open/Closed Principle (OCP)
- Liskov Substitution Principle (LSP)
- Interface Segregation Principle (ISP)
- Dependency Inversion Principle (DIP)

### ✅ Dependency Injection (DI)

- What is DI and why use it
- Types of DI: Constructor, Setter, Interface injection
- Benefits of DI
- Real-life analogy
- Detailed examples in C#
- DI frameworks overview

---

## 📌 Notes

### Part 3: SOLID Principles

SOLID is an acronym for five design principles aimed at improving code quality:

---

#### 1. Single Responsibility Principle (SRP)

- A class should have only one reason to change.
- Meaning: Each class should only do one thing or handle one responsibility.

**Example:**

```c#
class Invoice {
    public void CalculateTotal() { /* calculation code */ }
    public void PrintInvoice() { /* printing code */ } // Violates SRP
}
```

Better to separate:

```c#
class InvoiceCalculator {
    public void CalculateTotal() { /* calculation code */ }
}

class InvoicePrinter {
    public void PrintInvoice() { /* printing code */ }
}
```

---

#### 2. Open/Closed Principle (OCP)

- Software entities (classes, modules, functions) should be open for extension but closed for modification.
- You should be able to add new features without changing existing code.

**Example:**

```c#
abstract class Shape {
    public abstract double Area();
}

class Rectangle : Shape {
    public double Width, Height;
    public override double Area() => Width * Height;
}

class Circle : Shape {
    public double Radius;
    public override double Area() => Math.PI * Radius * Radius;
}
```

You can add new shapes without modifying existing ones.

---

#### 3. Liskov Substitution Principle (LSP)

- Objects of a superclass should be replaceable with objects of subclasses without affecting the correctness of the program.

**Example Violation:**

```c#
class Bird {
    public virtual void Fly() { }
}

class Ostrich : Bird {
    public override void Fly() {
        throw new Exception("Ostriches can't fly!");
    }
}
```

Better to redesign so `Ostrich` is not forced to implement unsupported behavior.

---

#### 4. Interface Segregation Principle (ISP)

- Clients should not be forced to depend on interfaces they do not use.
- Split large interfaces into smaller, more specific ones.

**Example:**

```c#
interface IWorker {
    void Work();
    void Eat();
}

class Robot : IWorker {
    public void Work() { /* working */ }
    public void Eat() { throw new NotImplementedException(); } // Violation
}
```

Better to split:

```c#
interface IWorkable {
    void Work();
}

interface IFeedable {
    void Eat();
}

class Robot : IWorkable {
    public void Work() { /* working */ }
}
```

---

#### 5. Dependency Inversion Principle (DIP)

- High-level modules should not depend on low-level modules; both should depend on abstractions.
- Abstractions should not depend on details; details should depend on abstractions.

**Example:**

Instead of:

```c#
class BackendDeveloper {
    public void Develop() { /* backend code */ }
}

class FrontendDeveloper {
    public void Develop() { /* frontend code */ }
}

class Project {
    BackendDeveloper backend = new BackendDeveloper();
    FrontendDeveloper frontend = new FrontendDeveloper();

    public void DevelopProject() {
        backend.Develop();
        frontend.Develop();
    }
}
```

Use abstraction:

```c#
interface IDeveloper {
    void Develop();
}

class BackendDeveloper : IDeveloper { public void Develop() { } }
class FrontendDeveloper : IDeveloper { public void Develop() { } }

class Project {
    private IDeveloper _developer1;
    private IDeveloper _developer2;

    public Project(IDeveloper dev1, IDeveloper dev2) {
        _developer1 = dev1;
        _developer2 = dev2;
    }

    public void DevelopProject() {
        _developer1.Develop();
        _developer2.Develop();
    }
}
```

---

### Part 4: Dependency Injection (DI)

---

#### What is Dependency Injection?

Dependency Injection is a design pattern where an object receives the objects it depends on, rather than creating them itself.

**Analogy:** Ordering coffee from a cafe instead of growing coffee beans yourself.

---

#### Types of Dependency Injection

| Type                      | Description                                    | Example Usage                        |
| ------------------------- | ---------------------------------------------- | ------------------------------------ |
| **Constructor Injection** | Dependencies passed via constructor parameters | `public Car(IEngine engine) { ... }` |
| **Setter Injection**      | Dependencies set via properties or setters     | `car.Engine = new DieselEngine();`   |
| **Interface Injection**   | Dependencies injected via interface methods    | `void SetEngine(IEngine engine);`    |

---

#### Benefits of DI

- Loosely coupled code
- Easier testing (mock dependencies)
- Clear dependency declaration
- Easier maintenance and flexibility
- Supports Inversion of Control (IoC)

---

#### Examples

**Constructor Injection:**

```c#
public interface IEngine {
    void Start();
}

public class DieselEngine : IEngine {
    public void Start() { Console.WriteLine("Diesel engine started."); }
}

public class Car {
    private IEngine _engine;

    public Car(IEngine engine) { _engine = engine; }

    public void StartCar() { _engine.Start(); }
}

// Usage:
IEngine engine = new DieselEngine();
Car car = new Car(engine);
car.StartCar();
```

---

**Setter Injection:**

```c#
public class Car {
    private IEngine _engine;

    public IEngine Engine {
        set { _engine = value; }
    }

    public void StartCar() {
        if (_engine == null) Console.WriteLine("Engine not set!");
        else _engine.Start();
    }
}

// Usage:
Car car = new Car();
car.Engine = new DieselEngine();
car.StartCar();
```

---

**Interface Injection:**

```c#
public interface IEngineSetter {
    void SetEngine(IEngine engine);
}

public class Car : IEngineSetter {
    private IEngine _engine;

    public void SetEngine(IEngine engine) {
        _engine = engine;
    }

    public void StartCar() { _engine?.Start(); }
}

// Usage:
Car car = new Car();
car.SetEngine(new DieselEngine());
car.StartCar();
```

---

#### DI Frameworks & Containers

- Manual injection can be tedious.
- Frameworks automate dependency management.
- Popular .NET DI frameworks:
  - Microsoft.Extensions.DependencyInjection (built-in ASP.NET Core)
  - Autofac
  - Ninject
  - Unity

**Example in ASP.NET Core:**

```c#
// Startup.cs or Program.cs
services.AddTransient<IEngine, DieselEngine>();
services.AddTransient<Car>();

// Usage in constructor
public class MyController {
    private readonly Car _car;

    public MyController(Car car) {
        _car = car;
    }

    public void Drive() {
        _car.StartCar();
    }
}
```

---

## 🧪 Practice

- Refactor a tightly coupled class using constructor injection.
- Create multiple implementations of a service interface and switch between them using DI.
- Implement setter injection and observe behavior when dependency is missing.
- Explore ASP.NET Core built-in DI container: register services and inject them in controllers.

---

## 🙏 References

- [SOLID Principles - GeeksforGeeks](https://www.geeksforgeeks.org/solid-principle-in-programming-understand-with-real-life-examples/)
- [Dependency Injection (DI) - GeeksforGeeks](https://www.geeksforgeeks.org/dependency-injectiondi-design-pattern/)
- [Microsoft Docs: Dependency Injection in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
- [Refactoring Guru: Dependency Injection](https://refactoring.guru/design-patterns/dependency-injection)
