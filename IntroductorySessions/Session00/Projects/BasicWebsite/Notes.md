\# Part 0: Roadmap of this Session

## Getting Started with MVC Projects 
 - [x] Directory Structure: Detailed explanation of the MVC directory 
 - [x] (Controllers, Views, Models, wwwroot) and the role of Program.cs in  configuration. 
 - [x] Routing and Controllers: Custom routing, attribute routing, and  handling various HTTP requests. 
 - [x] Actions in Controllers: Exploring different return types and handling  different HTTP requests. 
## Controllers and Routing
- [x] Routing Basics: Attribute-based routing and custom route parameters. 
- [x] Controller Actions: How to handle different return types like JSON, HTML, and redirect results.
- [x] The address can contain static parts 
- [ ] How to set the Default Page in ASP 

---

# Part 1: Understanding Models in ASP.NET Core MVC

Models are a fundamental part of the MVC architecture, representing the data structure and logic of your application. They interact with the database and contain properties that hold data and methods that implement business logic.

### **Model Structure and Purpose**

- **Data Representation**: Models define the structure of data (often aligning with database tables).
- **Data Handling**: Models encapsulate data manipulation logic, like validation and relationships.
- **Data Transport**: Models pass data between the controller and the view.

In ASP.NET Core MVC, models are typically located in a **Models** folder, and each model represents a specific data entity, like `Product`, `Customer`, or `Order`.

---

# 2. Properties in Models and Object-Oriented Programming (OOP) in `C#`

To understand how models work, let’s cover key OOP concepts in C#, which is essential for defining and managing models in ASP.NET Core MVC.

### **Properties in C#**

Properties in C# provide a flexible way to access and modify the fields of a class. They use `get` and `set` accessors to control how data is read or assigned.

Here’s an example of a basic `Product` model with properties:


```c#
public class Product {

public int Id { get; set; }
// Auto-implemented property     
public decimal Price { get; set; }
```

- **Auto-implemented properties** (`Price`): Define a property without explicit backing fields, useful when no custom logic is needed.


### **Encapsulation and Access Modifiers**

Encapsulation in OOP hides the internal state of an object and restricts access to its properties and methods. C# provides access modifiers like `public`, `private`, `protected`, and `internal` to control access.

In ASP.NET Core MVC, models typically use **public properties** for easy access from other parts of the application (like controllers and views).

---

# 3. Passing a Model to a View

In ASP.NET Core MVC, data is passed from the controller to the view using models. You can pass a single model, a list of models, or even multiple models in complex scenarios.

### **Steps to Pass a Model from Controller to View**

1. **Define the Model** First, define the model class. Let’s use the `Product` model as our example.
    
```c#
public class Product 
{
	public int Id { get; set; }
	public string Name { get; set; }     
	public decimal Price { get; set; } 
}
```

1. **Create a Controller Action** In your controller, create an action that will pass the model data to the view.
    
```c#

public class ProductsController : Controller {
	public IActionResult Details()
	{
		var product = new Product{
		Id = 1,
		Name = "Laptop",
		Price = 1500.00m
	};
// Pass the model to the view
	return View(product);
	}
 }
```

In this example, the `Details` action creates an instance of `Product` and passes it to the view using `View(product);`.
    
3. **Strongly-Typed Views** When passing a model to a view, it’s common to make the view "strongly typed" to enable IntelliSense and compile-time checking.
    
    - Open or create a view for your action (like `Details.cshtml`).
        
    - At the top of the view, specify the model type with the `@model` directive.
        
```
@model Product  <h2>Product Details</h2> <p>Product Name: @Model.Name</p> <p>Price: @Model.Price</p>
```

    
In this example:

- `@model Product` specifies that the view expects a `Product` model.
- `@Model.Name` and `@Model.Price` retrieve properties of the `Product` instance.

### **Passing a List of Models**

To pass a collection of models, define the controller action to return a list and set the view model type accordingly.


```c#
public IActionResult List() {
	var products = new List<Product> {
		new Product { Id = 1, Name = "Laptop", Price = 1500.00m },         new Product { Id = 2, Name = "Smartphone", Price = 800.00m }     
	}; 
    return View(products); }
```

In the `List.cshtml` view:


```c#
@model IEnumerable<Product>  
	<h2>Product List</h2> 
	<ul>
	@foreach (var product in Model)
	{
	         <li>@product.Name - @product.Price</li>
    }
    </ul>
```

This example uses `IEnumerable<Product>` as the model type to handle a list of `Product` objects.

---

