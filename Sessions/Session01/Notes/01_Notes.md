# Meeting Agenda

## The Practice
## Reviewing Last Meeting

## Explanation of Middleware 

## Routing 
### How to change the default routing
### Attribute Based Routing 
### Action Parameters 
### Query Parameters

## Different Kinds of Routing
## Show Roadmap 

### how far we've come
### the github projecet
### Ask ALI to talk about DI a bit



---

## Have a talk about the projects and practices...
## Give me feedback...



# Actual Content
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

## **3. Return Types in Controllers**

Return types in ASP.NET Core MVC actions define how data is sent back to the client. Different return types offer flexibility for returning views, JSON, status codes, and redirects.

### **Common Return Types**

1. **ViewResult** (`View()`)
    
    - Renders a Razor View.
    - Typically used in MVC applications where HTML is returned.
    

`public IActionResult Index() {     return View(); }`

1. **PartialViewResult** (`PartialView()`)
    
    - Renders a partial view, commonly used in AJAX requests.
    

```c#
public IActionResult ProductListPartial() {     return PartialView("_ProductListPartial"); }
```
    
2. **JsonResult** (`Json()`)
    
    - Returns JSON data, used often in API responses or AJAX calls.
    
```c#
public JsonResult GetProductJson(int id) {     return Json(new { id, name = "Sample Product" }); }
```
    
3. **ContentResult** (`Content()`)
    
    - Returns raw content like plain text or HTML.
    
```c#
public ContentResult GetPlainText() 
{     
	return Content("Plain Text Content"); 
}
```
    
4. **FileResult** (`File()`)
    
    - Used to send files as a response, such as for downloading PDFs or images.
     
```c#
public FileResult DownloadFile() {     byte[] fileBytes = System.IO.File.ReadAllBytes("sample.pdf");     return File(fileBytes, "application/pdf", "sample.pdf"); }
```

5. **RedirectToActionResult** (`RedirectToAction()`)
    
- Redirects to a different action.

```c#
public IActionResult RedirectToHome() 
{     
	return RedirectToAction("Index", "Home"); 
}
```    
6. **StatusCodeResult** and **ObjectResult**
    
    - `StatusCodeResult`: Sends HTTP status codes like `404`, `500`.
    - `ObjectResult`: Often used in APIs to return a model or object with a status code.

```c#
public IActionResult NotFoundResult() 
{     
	return StatusCode(404); 
}
```

7. **ChallengeResult** and **SignOutResult**
    
    - `ChallengeResult`: Used to trigger authentication challenges.
    - `SignOutResult`: Logs out the user.
    

```c#
public IActionResult SignOutUser() { return SignOut(); }
```





---

## **Putting It All Together: A Complete Flow**

Here’s how you can use routing, controllers, and return types together to create an application with clear structure and varied responses.

1. **Configure Routing** in `Program.cs` with both default and custom routes.
2. **Define Controllers** for different areas of the application, organizing actions based on functionality and HTTP methods.
3. **Handle Different Requests and Return Types**:
    - For UI, use `ViewResult` or `PartialViewResult`.
    - For API endpoints, use `JsonResult` and `ObjectResult`.
    - Redirect or return status codes based on user actions.

By mastering routing, controllers, and return types in this way, you’ll be able to create well-structured ASP.NET Core MVC applications that respond appropriately to a wide variety of requests. This roadmap provides a full foundation, allowing you to expand into more complex scenarios as you gain experience.

| Return Type      | Description                                       |
| ---------------- | ------------------------------------------------- |
| `Ok()`           | Returns 200 OK with optional content.             |
| `NotFound()`     | Returns 404 Not Found.                            |
| `BadRequest()`   | Returns 400 Bad Request.                          |
| `Unauthorized()` | Returns 401 Unauthorized.                         |
| `Forbid()`       | Returns 403 Forbidden.                            |
| `Created()`      | Returns 201 Created with a URI for the new item.  |
| `NoContent()`    | Returns 204 No Content, typically for DELETE/PUT. |
| `Redirect()`     | Redirects to another URL.                         |
| `Content()`      | Returns plain text or other content.              |
| `Json()`         | Returns JSON data.                                |
| `File()`         | Returns a file.                                   |
