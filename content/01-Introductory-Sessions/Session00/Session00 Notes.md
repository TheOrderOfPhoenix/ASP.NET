# Session 0: Controllers, Routing, and Return Types

## 📝 Overview

In this session, we’ll cover the following concepts:

- MVC pattern in ASP.NET Core
- Controllers and their structure
- Default and attribute-based routing
- Handling various HTTP requests (GET, POST, DELETE)
- Return types from controllers
- Building a real-world Product flow
- Mini project assignment

## 📚 Topics Covered

### ✅ MVC Architecture

> Learn how the Model-View-Controller (MVC) architecture separates concerns in a web application.  
> 🔗 [Microsoft Docs - MVC Pattern](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)

### ✅ Controllers and Actions

> Understand how to create controllers and action methods, follow naming conventions, and return data to views.  
> 🔗 [w3schools - MVC Controllers](https://www.w3schools.com/asp/asp_net_mvc_intro.asp)

### ✅ Routing in ASP.NET Core

> Understand how ASP.NET Core maps incoming requests to the appropriate controller and action using default and custom routing.  
> 🔗 [Microsoft Docs - Routing](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing)

### ✅ HTTP Methods in MVC

> Learn how to respond to different types of HTTP requests using attributes like `[HttpGet]`, `[HttpPost]`.  
> 🔗 [HTTP Methods - MDN](https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods)

### ✅ Return Types

> Explore different return types like `ViewResult`, `JsonResult`, and more, and know when to use each one.  
> 🔗 [Action Results in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions)

## 📌 Notes

> _Collected from various sources including W3Schools, Microsoft Docs, and ChatGPT_

---

### **1. Start with the Basics: Understanding MVC Architecture**

**Goal**: Give a quick overview of the MVC pattern, focusing on the role of controllers.

- ✅ **Explain MVC**:

  - **Model**: Manages data and business logic.
  - **View**: Handles UI.
  - **Controller**: Acts as the intermediary, processing requests and returning responses.

- ✅ **Controller’s Role**:  
  Handles incoming HTTP requests, decides which logic to execute, and returns the appropriate result (usually a view or data).

- ✅ **Activity**:  
  Create a “Hello World” ASP.NET Core MVC app and show the default routing behavior (`HomeController` with `Index` action).

---

### **2. Controllers: Structure, Naming, and Basics**

**Goal**: Help students understand how to structure and create controllers, and introduce them to actions.

- ✅ **Creating a Simple Controller**:

  - Use Visual Studio to add a new controller.
  - Controller classes must:
    - Be `public`
    - Inherit from `Controller`
    - Have `Controller` as a suffix in their name (e.g., `ProductController`).

- ✅ **Basic Action Methods**:
  - Action methods must be `public`.
  - Default return type is usually `IActionResult`.

````csharp
public class ProductController : Controller {
    public IActionResult Index() {
        return View();
    }

    public IActionResult Details(int id) {
        var product = new Product { Id = id, Name = "Sample Product", Price = 25.00m };
        return View(product);
    }
}

- ✅ **Activity**:
    Create your own `CustomerController` with an `Index` and a `Details` action.


---

### **3. Routing Basics: Default Routing and Attribute-Based Routing**

**Goal**: Teach students how routing works in ASP.NET Core, from default routing to custom attribute routing.

- ✅ **Default Route Configuration**:
    Defined in `Program.cs`:


```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
````

- `{controller=Home}`: Default controller
- `{action=Index}`: Default action
- `{id?}`: Optional parameter
- ✅ **Customizing Routes**:
  - You can modify the pattern in `Program.cs`.
  - Example: Use `"{controller=Product}/{action=List}/{id?}"` to change defaults.
- ✅ **Attribute-Based Routing**:
  - Useful for APIs or when you want specific paths.

```csharp
[Route("product")]
public class ProductController : Controller {
    [HttpGet("list")]
    public IActionResult List() {
        return View();
    }

    [HttpGet("{id:int}")]
    public IActionResult Details(int id) {
        return View();
    }
}
```

- 🔲 **Activity**:  
   Add routes to your `CustomerController`:
  - `[HttpGet("all")]` to show all customers
  - `[HttpGet("{id}")]` to show customer details

---

### **4. Controllers and Actions: Handling Different HTTP Requests**

**Goal**: Show how controllers handle various HTTP methods.

- ✅ **HTTP Verbs**:
  - **GET**: Retrieve data
  - **POST**: Submit data
  - **PUT**: Update data
  - **DELETE**: Remove data
- ✅ **HTTP-Specific Attributes**:
  - Use `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]` to restrict access.

```csharp
public class ProductController : Controller {
    [HttpGet]
    public IActionResult List() { return View(); }

    [HttpPost]
    public IActionResult Create(Product product) {
        // Save to DB
        return RedirectToAction("List");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        // Remove from DB
        return NoContent();
    }
}
```

- 🔲 **Activity**:  
   Add full CRUD actions to `CustomerController`, using appropriate HTTP verbs and routing.

---

### **5. Return Types: Exploring Different Return Options in Controllers**

**Goal**: Familiarize students with different return types in ASP.NET Core.

- ✅ **Basic Return Types**:
  - `ViewResult` → `return View();`
  - `JsonResult` → `return Json(data);`
  - `ContentResult` → `return Content("Text");`
- ✅ **When to Use**:
  - Use `View()` when rendering pages.
  - Use `Json()` for APIs or AJAX.
  - Use `Content()` for simple text/plain responses.

```csharp
public class ProductController : Controller {
    public IActionResult Index() => View();

    public JsonResult GetProductJson(int id) => Json(new { id, name = "Product" });

    public ContentResult GetMessage() => Content("This is a simple text message.");
}
```

- ✅ **Advanced Return Types**:
  - `RedirectToAction("Index")`: Navigate to another action.
  - `StatusCode(404)`: Return HTTP status codes.
  - `File()`: Return downloadable content (e.g., PDF, image).
- 🔲 **Activity**:  
   Add:
  - One action returning JSON
  - One returning text
  - One redirecting to another action

---

### **6. Real-World Application: Creating a Full Flow**

**Goal**: Connect controllers, models, and views to build something meaningful.

- ✅ **Create a Model**:

```csharp
public class Product {
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

- ✅ **Controller-Model Interaction**:

```csharp
public IActionResult Details(int id) {
    var product = new Product {
        Id = id,
        Name = "Laptop",
        Price = 1200.00m
    };
    return View(product);
}
```

- ✅ **Pass Data to View**:
  - Use a strongly typed view (`@model Product`)
  - Display `@Model.Name`, `@Model.Price`, etc.
- 🔲 **Activity**:
  - Create `Product` model
  - Create `Details` and `List` actions
  - Create strongly-typed Razor views

---

### **7. Practice and Review: Small Project Assignment**

**Goal**: Reinforce everything learned.

- ✅ **Project: Product Management App**
  - CRUD: Create, Read, Update, Delete products
  - Use default and attribute routing
  - Return different types (View, JSON, Redirect)
  - Include HTTP verb handling
- ✅ **Suggested Workflow**:
  1. Create the `Product` model
  2. Build controller actions
  3. Set up routing
  4. Implement Razor views
  5. Test everything end-to-end
- ✅ **Encourage Discussion**:
  - Why return JSON instead of HTML?
  - When to use attribute routing?
  - What's the benefit of redirecting?

---

## 🧪 Practice

- Create a basic MVC Hello World app
- Add `ProductController` with sample actions
- Use `[Route]` and `[HttpGet]` in `CustomerController`
- Implement full CRUD for customers or products
- Add JSON, Content, and Redirect examples
- Build a simple Product Detail View
- Complete mini-project: Product Management App

## 🙏 Acknowledgments

Sources:

- [w3schools.com](https://www.w3schools.com/)
- [Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/)
- ChatGPT conversations (2025 sessions)
