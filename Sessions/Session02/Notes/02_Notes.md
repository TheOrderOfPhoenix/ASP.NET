## **7. Model Validation**

ASP.NET Core MVC provides model validation, which helps ensure that data received from the user is valid.

### Example: Data Annotations



```c#
public class Product {
	public int Id { get; set; }
	[Required]
	[StringLength(50)]
	public string Name { get; set; }
	[Range(0, 10000)]
	public decimal Price { get; set; } 
	}
```

- **Required**: Ensures the property has a value.
- **StringLength**: Restricts the maximum length.
- **Range**: Limits the value to a specified range.

Validation errors are automatically displayed in views if you include `@Html.ValidationMessageFor` for each property or `@Html.ValidationSummary()` to show all errors.

### **Summary**

- **Models** represent data and business logic.
- **Properties** in models define data structure and encapsulate fields.
- **Controller-View Interaction**: Pass models to views using `View(model);`.



# Side Notes


catchall in routing

appsettings.json

query parameters 

apply the teaching techniques 

Razor View Engine 


how to optimize the projects code

Talk about lists C# 

how to return custom view 


assign C# variable and use it 


ViewBag.... is a dynamic
ViewBag.myName...


hot reload...

	what actually is viweBag, viewData

Layouts..

_ViewStart 
_Shared 



_MyLayout cshtml



RenderBody
RenderSection (required... )

_ViewImports



HtmlHelper / TagHelper 

partial view 

How to pass data to partial views

Models and Model Types 
1. Binding Models
2. Application Models
3. View Models
https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-8.0

---

## **5. View Models vs. Domain Models**

In some cases, the data needed by a view might be a combination of several models or require custom formatting. A **View Model** is a class specifically created to supply the view with only the necessary data.

### Creating a View Model


```c#
public class ProductViewModel 
{ 
	public int Id { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
	public string FormattedPrice => $"${Price:N2}"; 
}
```

Using a view model keeps the domain model (`Product`) separate, especially useful when the view requires additional fields or formatting.

### Using the View Model in the Controller and View

In the controller:


```c#
public IActionResult Details() 
{
	var product = new Product { Id = 1, Name = "Laptop", Price = 1500.00m };
	var productViewModel = new ProductViewModel 
	{ 
		Id = product.Id,
		Name = product.Name,
		Price = product.Price
	};      
	return View(productViewModel); }
```

In the view:

```c#
@model ProductViewModel  <h2>Product Details</h2> <p>Product Name: @Model.Name</p> <p>Price: @Model.FormattedPrice</p>
```

View models allow you to tailor data specifically for the view, improving separation of concerns and maintainability.

---

## **6. Model Binding in ASP.NET Core MVC**

Model binding is a feature in ASP.NET Core that automatically maps HTTP request data to action parameters, making it easier to pass user inputs to models.

- **From URL or Query String**: If the action expects a model parameter and the URL includes a query string, model binding will automatically populate the model’s properties.
    
    
```c#
public IActionResult UpdatePrice(int id, decimal price) {     // id and price are bound from the query string or route data }
```

- **From Form Data**: For form submissions, model binding maps form inputs to model properties by matching the names.
    

---




What is Business Logic
