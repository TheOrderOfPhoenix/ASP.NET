# Session 2: Middleware, Routing, and Controllers in ASP.NET Core MVC

## 📝 Overview

In this session, we’ll cover the following concepts:

- Middleware and the ASP.NET Core request pipeline
- Routing structure: default, attribute-based, constraints, SEO-friendly
- Creating and structuring controllers
- Handling HTTP methods
- Dependency Injection in controllers
- Model binding and action parameters

## 📚 Topics Covered

### ✅ Middleware and Request Pipeline

> Visual understanding of the middleware flow and pipeline order  
> 🖼️ _See diagrams in original session notes_

🔗 [ASP.NET Core Middleware Overview](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/)

### ✅ Routing in ASP.NET Core

> Learn how incoming requests map to controller actions through routing.  
> 🔗 [ASP.NET Core Routing Docs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing)

### ✅ Controllers

> Understand how to structure controllers and handle requests with actions.  
> 🔗 [ASP.NET Core Controllers](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions)

## 📌 Notes

> _Collected from various sources including Microsoft Docs and ChatGPT_

---

## 📍 Part 0: Middleware and Request Pipeline

🖼️ **Diagrams**:

- ![[Pasted image 20241105101014.png]]
- ![[Pasted image 20241105111037.png]]
- ![[Pasted image 20241105111022.png]]

---

## 📍 Part 1: Routing in ASP.NET Core MVC

Routing maps incoming HTTP requests to controller actions.

### ✅ Basic Routing Structure

Routing is configured in `Program.cs` via `app.MapControllerRoute`.

````csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

- `{controller=Home}`: default controller

- `{action=Index}`: default action

- `{id?}`: optional URL parameter


---

### ✅ Attribute Routing

Define routes directly in the controller using attributes:

```csharp
[Route("products")]
public class ProductsController : Controller
{
    [HttpGet("all")]
    public IActionResult GetAllProducts() { /*...*/ }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id) { /*...*/ }
}
````

---

### ✅ Route Parameters & Constraints

You can enforce patterns and types using constraints:

```csharp
app.MapControllerRoute(
    name: "custom",
    pattern: "{controller=Products}/{action=List}/{id:int:min(1)}"
);
```

- `int:min(1)` means `id` must be an integer ≥ 1
- Other constraints: `bool`, `datetime`, `guid`, `minlength(x)`, etc.

---

### ✅ Per-Controller Default Routes

You can assign a default action to a specific controller:

```csharp
app.MapControllerRoute(
    name: "gallery",
    pattern: "Gallery/{action=Main}/{id?}",
    defaults: new { controller = "Gallery" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
```

---

### ✅ SEO-Friendly URLs

Use descriptive URLs for readability and SEO:

```csharp
app.MapControllerRoute(
    name: "productDetail",
    pattern: "products/details/{id:int}/{name}"
);
```

✅ Example output: `/products/details/10/laptop`

---

## 📍 Part 2: Controllers in ASP.NET Core MVC

Controllers handle requests and return responses, acting as a bridge between models and views.

### ✅ Creating a Basic Controller

```csharp
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
```

---

### ✅ Handling HTTP Methods

Use attributes to define the HTTP method an action should handle:

```csharp
[HttpPost]
public IActionResult CreateProduct(Product product)
{
    return RedirectToAction("Index");
}
```

Other attributes include:

- `[HttpGet]`
- `[HttpPut]`
- `[HttpDelete]`

---

### ✅ Dependency Injection in Controllers

Inject services like repositories into controllers for better separation of concerns:

```csharp
public class ProductsController : Controller
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }
}
```

---

### ✅ Action Parameters and Model Binding

Parameters are bound automatically from the URL, query string, or form body:

```csharp
public IActionResult EditProduct(int id, string name)
{
    // id and name are bound from query or route
}
```

For complex types, ASP.NET Core binds data from the request body (e.g., forms or JSON).

---

## 🧪 Practice

- Define multiple routes with custom constraints
- Build an SEO-friendly route pattern
- Create controller with basic actions (`Index`, `Details`, `Create`)
- Add attribute-based routes to actions
- Use `[HttpGet]`, `[HttpPost]` on appropriate methods
- Inject a service into a controller via constructor
- Bind parameters from query and route

## 🙏 Acknowledgments

Sources:

- [Microsoft Docs](https://learn.microsoft.com/en-us/aspnet/core/)
- ChatGPT 2025 sessions
