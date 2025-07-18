# Session 5: Interfaces and Object-Oriented Relationships

## 📝 Overview

In this session, we explored:

- The concept and syntax of **Interfaces** in C#
- Implementation of interfaces in real scenarios
- Comparison of **Abstract Classes vs Interfaces**
- Key OOP relationships: **Inheritance**, **Composition**, **Aggregation**, **Association**, and **Dependency**
- Applying these relationships in a **Library Management System**

## 📚 Topics Covered

### Interfaces in C#

> Interfaces define contracts that classes must fulfill. They enable loose coupling and multiple inheritance in C#.

- Syntax: `interface IMyInterface { void Method(); }`
- Implementation: `class MyClass : IMyInterface`
- All members are `public` and `abstract` by default
- Cannot contain fields
- Supports multiple inheritance

### Interface Example

````c#
interface IDisplayer { void Display(); }

class Test : IDisplayer {
    public void Display() => Console.WriteLine("Hello from interface!");
}

### Abstract Class vs Interface

|Feature|Abstract Class|Interface|
|---|---|---|
|Implementation Allowed?|Yes|No|
|Multiple Inheritance|No|Yes|
|Constructors|Yes|No|
|Fields|Yes|No|
|Use Case|Partial abstraction|Full abstraction|

### OOP Relationships

#### Inheritance (IS-A)

- A class inherits members from another class.

- Promotes reusability.


```c#
class Person { }
class Student : Person { }
````

#### Composition (PART-OF)

- Objects are composed of other objects.
- Strong lifecycle dependency.

```c#
class Engine { }
class Car { Engine engine = new Engine(); }
```

#### Association (HAS-A)

- Loose relationship between objects.
- Both can exist independently.

```c#
class Teacher { }
class School {
    public void AddTeacher(Teacher t) { }
}
```

#### Aggregation (WEAK-PART-OF)

- Special association where one object "owns" another but the owned object can exist independently.

```c#
class Address { }
class Student {
    public Address address;
}
```

#### Dependency

- Temporary usage of one class by another.

```c#
class Printer { void Print(string msg) => Console.WriteLine(msg); }
class Report { void Generate(Printer p) => p.Print("Report"); }
```

### Applied Example: Library Management System

A real-world example combining all relationships:

- **Person → Member/Librarian** (Inheritance)
- **Library → Bookshelf → Book** (Composition)
- **Member ↔ LibraryCard** (Association)
- **Member → Address** (Aggregation)
- **Report.Generate(Printer)** (Dependency)

✔ See full code snippet [here](https://chatgpt.com/c/68715298-27f8-8010-a29b-8f6e457f6179#) or review it during the live session.

## 🧪 Practice Tasks

- Implement an `IVehicle` interface in two different classes.
- Use composition to build a `Computer` with `CPU`, `RAM`, and `HardDrive`.
- Demonstrate aggregation with `Employee` and `Address`.
- Create an association between `Doctor` and `Patient`.
- Write a class `Order` that depends on `PaymentProcessor`.

## 🙏 Acknowledgments

Sources:

- StackOverflow
- GeeksforGeeks
- C# Corner
- Medium
- ChatGPT Assistance (2025 sessions)
