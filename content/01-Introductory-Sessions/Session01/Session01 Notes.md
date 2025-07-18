# Session 1: Working with Models and MVC Basics in ASP.NET Core

## 📝 Overview

In this session, we’ll cover the following concepts:

- MVC project structure and configuration
- Controllers and routing (including static segments and default page setting)
- Introduction to Models in ASP.NET Core
- C# OOP essentials: properties, encapsulation, and access modifiers
- Passing models (single and list) from controllers to views
- Strongly-typed Razor views

## 📚 Topics Covered

### ✅ MVC Project Structure & Routing

> Learn how ASP.NET Core organizes files and configures routes for handling web requests.  
> 🔗 [Microsoft Docs - MVC Introduction](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)

### ✅ C# Properties & OOP Essentials

> Explore object-oriented programming basics in C#, including properties and encapsulation.  
> 🔗 [C# OOP Overview](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop)

### ✅ Models and Views

> Understand how to define models and pass them to views in ASP.NET Core MVC.  
> 🔗 [Microsoft Docs - Work with Data in MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/)

## 📌 Notes

> _Collected from various sources including W3Schools, Microsoft Docs, and ChatGPT_

---

### 📍 Part 0: Roadmap of This Session

#### ✅ Getting Started with MVC Projects

- [x] **Directory Structure**:  
       Learn about the standard folders: `Controllers`, `Views`, `Models`, and `wwwroot`. Understand the role of `Program.cs` in bootstrapping and routing.

- [x] **Routing and Controllers**:

  - Custom routing using templates like `{controller}/{action}/{id?}`
  - Attribute routing using `[Route]`, `[HttpGet]`, etc.
  - Static segments in routes:  
    Example: `[Route("products/all")]` creates `/products/all`.

- [x] **Actions in Controllers**:

  - Return types like `ViewResult`, `JsonResult`, `ContentResult`
  - Handle multiple HTTP methods (GET, POST, etc.)

- 🔲 **How to Set Default Page in ASP.NET Core**  
  _(To do: Discuss how to change the default route in `Program.cs`, e.g. set controller = Products, action = List)_

---

### 📍 Part 1: Understanding Models in ASP.NET Core MVC

Models are a fundamental part of the MVC architecture, representing the **data structure and logic** of your application. They interact with the database and contain properties that hold data and methods that implement business logic.

#### 🧠 Model Structure and Purpose

- **Data Representation**: Models reflect real-world data structures, often aligning with database tables.
- **Data Handling**: Models encapsulate validation, relationships, and business logic.
- **Data Transport**: Used to transfer data between controllers and views.

In ASP.NET Core MVC, models are typically stored in a `Models` folder, with one class per entity (`Product`, `Customer`, `Order`, etc.).

---

### 📍 Part 2: Properties in Models & C# OOP Concepts

#### ✅ Properties in C#

Properties in C# provide a controlled way to access and modify private fields using `get` and `set`.

````csharp
public class Product {
    public int Id { get; set; } // Auto-property
    public decimal Price { get; set; }
}

- **Auto-Implemented Properties**:
    Simplify property declarations when no extra logic is needed.


#### ✅ Encapsulation and Access Modifiers

Encapsulation hides the internal workings of a class from the outside world.

- **Access Modifiers**:

    - `public`: Accessible from anywhere

    - `private`: Only inside the class

    - `protected`: Inside the class and derived classes

    - `internal`: Only within the current assembly


**MVC models typically use public properties** so they can be accessed in views and controllers.

---

### 📍 Part 3: Passing a Model to a View

#### ✅ Step 1: Define the Model

```csharp
public class Product {
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
````

#### ✅ Step 2: Create a Controller Action

```csharp
public class ProductsController : Controller {
    public IActionResult Details() {
        var product = new Product {
            Id = 1,
            Name = "Laptop",
            Price = 1500.00m
        };
        return View(product); // Passing model to view
    }
}
```

#### ✅ Step 3: Strongly-Typed View (`Details.cshtml`)

```cshtml
@model Product

<h2>Product Details</h2>
<p>Product Name: @Model.Name</p>
<p>Price: @Model.Price</p>
```

- `@model` tells Razor this view receives a `Product`
- `@Model` gives access to passed data

---

### 📍 Passing a List of Models to the View

#### ✅ Controller Action:

```csharp
public IActionResult List() {
    var products = new List<Product> {
        new Product { Id = 1, Name = "Laptop", Price = 1500.00m },
        new Product { Id = 2, Name = "Smartphone", Price = 800.00m }
    };
    return View(products);
}
```

#### ✅ View (`List.cshtml`):

```cshtml
@model IEnumerable<Product>

<h2>Product List</h2>
<ul>
@foreach (var product in Model) {
    <li>@product.Name - @product.Price</li>
}
</ul>
```

- Use `IEnumerable<Product>` to pass lists
- Razor supports `foreach` directly on the `Model`

---

## 🧪 Practice

- Explore and explain the MVC folder structure
- Create a model class `Product`
- Build a controller that returns a single model to a view
- Create a strongly-typed Razor view using `@model`
- Return a list of `Product` models and display them in a loop
- Show how to set the default controller and action in `Program.cs`

## 🙏 Acknowledgments

Sources:

- [w3schools.com](https://www.w3schools.com/)
- [Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/)
- ChatGPT sessions (2025)
