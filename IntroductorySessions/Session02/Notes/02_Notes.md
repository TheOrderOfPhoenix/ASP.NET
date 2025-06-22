
## **0. Middleware and Request Pipeline**
![[Pasted image 20241105101014.png]]
![[Pasted image 20241105111037.png]]

![[Pasted image 20241105111022.png]]

## **1. Routing in ASP.NET Core MVC**

Routing is a mechanism to map incoming HTTP requests to specific controller actions. Understanding routing is crucial for managing URL patterns and making your app user-friendly and SEO-optimized.

### **Basic Routing Structure**

Routes in ASP.NET Core are configured in `Program.cs` (or `Startup.cs` in earlier versions) using the `app.MapControllerRoute` method, which defines how URLs map to controllers and actions.

### **Steps and Key Concepts in Routing**

1. **Define a Default Route** The default route defines the basic URL pattern that ASP.NET Core will follow.
    
```c#
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
```
- **Pattern**: 
	- `{controller=Home}/{action=Index}/{id?}`:
	- `{controller=Home}`: Specifies the controller, defaulting to `Home`.
	- `{action=Index}`: Specifies the action method, defaulting to `Index`.
	- `{id?}`: Optional parameter for passing an `id`.
1. **Attribute Routing** Define routes directly within the controller using attributes. This is useful when each action needs a unique route.
    
 ```c#
[Route("products")] 
public class ProductsController : Controller 
{     
	[HttpGet("all")]
	public IActionResult GetAllProducts() { /*...*/ }      

	[HttpGet("{id}")]     
	public IActionResult GetProductById(int id) { /*...*/ } 
}
```
1. **Custom Route Parameters and Constraints** You can add custom parameters and enforce constraints directly in the route to manage data types and URL patterns.
        
```c#
app.MapControllerRoute(name: "custom", pattern: "{controller=Products}/{action=List}/{id:int:min(1)}");
```

- **Constraints**: `{id:int:min(1)}` ensures `id` is an integer greater than or equal to 1.
- Other constraints include `bool`, `datetime`, `guid`, `minlength(x)`, `maxlength(x)`, and custom regular expressions.

1. **Setting Default Actions Based on Controller** To give specific controllers a unique default action, create additional routes before the default route.
    
```c#
app.MapControllerRoute(name: "gallery", pattern: "Gallery/{action=Main}/{id?}",defaults: new { controller = "Gallery" });  

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
```
    
1. **SEO-Friendly and Readable URLs** For user-friendly URLs, use meaningful route names instead of parameters. This is particularly useful for e-commerce or content-heavy sites.    
```c#
app.MapControllerRoute(name: "productDetail", pattern: "products/details/{id:int}/{name}");
```

This structure allows URLs like `/products/details/10/laptop`, which is clear and keyword-rich.

---

## **2. Controllers in ASP.NET Core MVC**

Controllers are central in ASP.NET Core MVC and handle requests, retrieve data, and determine how it’s returned to the client.

### **Controller Basics**

- **Controller Naming**: Controllers usually end with "Controller" (e.g., `HomeController`, `ProductController`).
- **Actions**: Methods within controllers are called action methods, typically returning a response to the user.

### **Steps and Key Concepts in Controllers**

1. **Creating a Basic Controller** A controller inherits from the `Controller` base class and contains action methods.
    
```c#
public class HomeController : Controller 
{     
	public IActionResult Index()     
	{         
		return View();     
	} 
}
```

1. **Handling HTTP Methods with Attributes** Controllers use HTTP attributes like `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, and `[HttpDelete]` to specify which HTTP methods they handle.
    
```c# 
[HttpPost] 
public IActionResult CreateProduct(Product product) 
{     
    //Handle POST request     
	return RedirectToAction("Index"); 
}
```
    
3. **Using Dependency Injection in Controllers** Dependency injection is commonly used in ASP.NET Core to inject services (like a repository) into controllers, supporting clean code and testability.
    
```c#
public class ProductsController : Controller 
{     
	private readonly IProductRepository _repository;
    public ProductsController(IProductRepository repository)     
    {         
	    _repository = repository;     
	    } 
	}
}
```

1. **Defining Action Parameters** Parameters passed to action methods are automatically bound from the URL or query string. Use model binding to parse complex objects from the request body.
    
    
```c#
public IActionResult EditProduct(int id, string name) {     
// Parameters are bound from the URL }
```


---

