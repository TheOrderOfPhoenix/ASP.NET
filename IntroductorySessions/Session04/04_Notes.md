## View Components
[View components](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-9.0) are similar to partial views in that they allow you to reduce repetitive code, but they're appropriate for view content that requires code to run on the server in order to render the webpage. View components are useful when the rendered content requires database interaction, such as for a website shopping cart. View components aren't limited to model binding in order to produce webpage output.


## ViewImports and ViewStart
In Razor Pages, the application of `ViewStart.cshtml` and `ViewImports.cshtml` is governed by their **location in the folder hierarchy**. These files are automatically discovered and applied by ASP.NET Core's Razor engine based on the folder structure.

---

### **How `ViewStart.cshtml` Works in Razor Pages**

1. **Global Scope:**
    
    - A `ViewStart.cshtml` file placed in the root of the `Pages` folder applies to all Razor Pages in the project.
    - Example structure:
        `Pages/ ├── _ViewStart.cshtml ├── Index.cshtml ├── About.cshtml`
        
        If `_ViewStart.cshtml` contains:
        `@{     Layout = "_Layout"; }`
        Then `Index.cshtml` and `About.cshtml` will use the `_Layout` layout.
        
2. **Local Override:**    
    - If a `ViewStart.cshtml` exists in a subfolder, it **overrides** the `ViewStart.cshtml` in the parent folder for that subfolder and its descendants.
    - Example structure:
        `Pages/ ├── _ViewStart.cshtml ├── Admin/ │   ├── _ViewStart.cshtml │   ├── Dashboard.cshtml └── Index.cshtml`
	- The `_ViewStart.cshtml` in `Pages/Admin/` will apply to `Dashboard.cshtml`.
	- The `_ViewStart.cshtml` in the root will apply to `Index.cshtml`.

---

### **How `ViewImports.cshtml` Works in Razor Pages**
1. **Global Scope:**
    - A `ViewImports.cshtml` file placed in the root of the `Pages` folder applies to all Razor Pages in the project. 
    - Example structure:
        `Pages/ ├── _ViewImports.cshtml ├── Index.cshtml ├── Contact.cshtml`
        If `_ViewImports.cshtml` contains:
        `@using MyApp.Models @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`
        Then both `Index.cshtml` and `Contact.cshtml` will have access to the `MyApp.Models` namespace and the Tag Helpers.
2. **Local Override or Supplement:**
    - A `ViewImports.cshtml` file in a subfolder **adds to** or **overrides** the settings from the parent folder.
    - Example structure:    
        `Pages/ ├── _ViewImports.cshtml ├── Admin/ │   ├── _ViewImports.cshtml │   ├── Dashboard.cshtml └── Index.cshtml`
	- The `@using` directives and Tag Helpers in `Pages/Admin/_ViewImports.cshtml` will only apply to `Dashboard.cshtml`.
	- The root `Pages/_ViewImports.cshtml` still applies to `Index.cshtml`.
---

### **How Razor Pages Discover These Files**

- **Runtime Discovery:** The Razor engine searches for these files in the **current folder** and all parent folders (up to the root).
- **Hierarchical Application:**
- `ViewStart.cshtml` and `ViewImports.cshtml` in a closer (nested) folder override or extend those in parent folders.
- This hierarchy allows flexible configuration for specific parts of the application.

---

### **Best Practices for Razor Pages**

1. Place shared settings (e.g., default layout or common namespaces) in the root-level `ViewStart.cshtml` and `ViewImports.cshtml`.
2. Use folder-specific overrides sparingly to avoid confusion.
3. Keep these files clean and limited to truly shared settings or imports.
Let me know if you'd like an example project structure for clarification!
## Tag Helpers and HTML Helpers
### **1. Tag Helpers**

### **What Are Tag Helpers?**

Tag Helpers are server-side components in ASP.NET Core that help you generate and manipulate HTML elements using a natural and familiar syntax that resembles standard HTML.

#### Key Characteristics:

- Blend seamlessly with standard HTML.
- Use attributes to bind data or add functionality.
- Processed on the server and output pure HTML.

---

### **Examples of Tag Helpers**

1. **Anchor Tag Helper (`<a>`)**
    `<a asp-controller="Home" asp-action="About" class="btn btn-primary">Go to About</a>`
    - `asp-controller`: Specifies the controller (`Home`).
    - `asp-action`: Specifies the action (`About`).
    - This generates:
        `<a href="/Home/About" class="btn btn-primary">Go to About</a>`
1. **Form Tag Helper**
    
    `<form asp-controller="Account" asp-action="Login" method="post">     <input type="text" name="username" />     <button type="submit">Login</button> </form>`
    
    - Automatically generates the form's `action` attribute based on the controller and action.
3. **Input Tag Helper* 

    `<input asp-for="UserName" class="form-control" />`
    
    - `asp-for`: Binds the input element to the `UserName` property of the model.
4. **Validation Tag Helpers**
    `<span asp-validation-for="Email" class="text-danger"></span>`
    
    - Displays validation messages for the `Email` property.

---

### **How Tag Helpers Work**

- Tag Helpers are identified by their **attributes**, like `asp-controller` or `asp-for`.
- These attributes are processed on the server to generate the appropriate HTML.

#### Configuration:

Tag Helpers are enabled globally in Razor views by default using the `_ViewImports.cshtml` file:

`@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`

#### Benefits:

- Intuitive and HTML-like syntax.
- Cleaner and more readable Razor views.
- Easier to maintain and debug.

---

### **2. HTML Helpers**

### **What Are HTML Helpers?**

HTML Helpers are server-side C# methods that generate HTML elements dynamically. They are written in Razor syntax (`@Html.*`) and allow you to create UI components programmatically.

#### Key Characteristics:

- Written as C# methods.
- More explicit than Tag Helpers.
- Processed on the server and output HTML.

---
### **Examples of HTML Helpers**

1. **Anchor Links (`Html.ActionLink`)**
    
    
    `@Html.ActionLink("Go to About", "About", "Home", null, new { @class = "btn btn-primary" })`
    
    - Generates:
        
        html
        
        `<a href="/Home/About" class="btn btn-primary">Go to About</a>`
        
2. **Forms (`Html.BeginForm`)**
    
    razor
    `@using (Html.BeginForm("Login", "Account", FormMethod.Post)) {     @Html.TextBoxFor(m => m.UserName, new { @class = "form-control" })     <button type="submit">Login</button> }`
    
    - Generates:
        
        
        `<form action="/Account/Login" method="post">     <input class="form-control" id="UserName" name="UserName" type="text" value="">     <button type="submit">Login</button> </form>`
        
3. **Input Fields (`Html.TextBoxFor`)**
    
    
    `@Html.TextBoxFor(m => m.Email, new { @class = "form-control" })`
    
    - Generates:
        
        
        `<input class="form-control" id="Email" name="Email" type="text" value="">`
        
4. **Validation Messages**
    
    
    `@Html.ValidationMessageFor(m => m.Email, null, new { @class = "text-danger" })`
    
    - Displays validation messages for the `Email` property.

---

### **How HTML Helpers Work**

- They are methods in the `System.Web.Mvc.HtmlHelper` class.
- Use lambda expressions to bind data to model properties.

#### Benefits:

- Provide programmatic control over HTML generation.
- Allow detailed customization using C#.

---

### **Comparison: Tag Helpers vs. HTML Helpers**

| **Feature**       | **Tag Helpers**                      | **HTML Helpers**                       |
| ----------------- | ------------------------------------ | -------------------------------------- |
| **Syntax**        | HTML-like attributes                 | C# method calls                        |
| **Readability**   | Cleaner and more intuitive           | Less readable in complex scenarios     |
| **Usage**         | Uses attributes like `asp-for`       | Uses Razor methods like `Html.TextBox` |
| **Configuration** | Requires `_ViewImports.cshtml` setup | No special configuration required      |
| **Flexibility**   | Easier to extend and customize       | More explicit but less integrated      |
| **Examples**      | `<input asp-for="Email" />`          | `@Html.TextBoxFor(m => m.Email)`       |

---

### **Best Practices**

1. Use **Tag Helpers** for modern, clean, and readable Razor views.
2. Use **HTML Helpers** for scenarios requiring complex C# logic or when you prefer programmatic control.
3. Avoid mixing both approaches in the same view for consistency.

---
### **Difference Between ViewData and ViewBag in ASP.NET Core MVC**

1. **Nature and Syntax:**
    - **ViewData:**
        - A dictionary-based object (`ViewData` is of type `ViewDataDictionary`) that allows storing data as key-value pairs.
        - Syntax: `ViewData["Key"] = value;`
    - **ViewBag:**
        - A dynamic object (uses the `dynamic` keyword internally) that provides a more flexible way to pass data without needing a strongly typed key.
        - Syntax: `ViewBag.Key = value;`
2. **Ease of Use:**
	- **ViewData** requires string keys and is less intuitive for accessing data.
    - **ViewBag** allows using properties directly (e.g., `ViewBag.Key`) which feels more natural in many cases.
3. **Compile-Time Safety:**
    - **ViewData:** Offers no compile-time checking; you'll encounter runtime errors if the key is mistyped or doesn't exist.
    - **ViewBag:** Same limitation as `ViewData`—runtime errors will occur if you try to access a non-existing property.

---
### **Storage**

Both `ViewData` and `ViewBag` store their data in the same place internally: the `ViewData` dictionary.

- When you assign a value to `ViewBag.Key`, it's actually stored in the `ViewData["Key"]` dictionary.
- Accessing `ViewData["Key"]` and `ViewBag.Key` refers to the same underlying data, so they are interchangeable.

For example:

csharp

Copy code

```c#
ViewData["Message"] = "Hello, ViewData!";
ViewBag.Message = "Hello, ViewBag!";  
// This works because they share the same storage: 
string msgFromViewData = ViewData["Message"].ToString(); // "Hello, ViewBag!" 
string msgFromViewBag = ViewBag.Message; // "Hello, ViewBag!"
```

### **How ViewBag Can Use Any Name**

The `ViewBag` uses the `dynamic` type in C#, which means properties on the `ViewBag` do not need to be declared beforehand.

When you assign `ViewBag.SomeProperty = value;`, the **runtime** dynamically creates an entry for `SomeProperty` in the `ViewData` dictionary. The `ViewBag` acts as a wrapper, intercepting calls to properties and storing them in `ViewData`.

**Example:**


```c#
ViewBag.Greeting = "Hello!";
 // Internally translates to: ViewData["Greeting"] = "Hello!";
```

When you try to access `ViewBag.Greeting`, the framework dynamically checks if a corresponding entry exists in `ViewData` and retrieves it. If it doesn’t exist, a runtime error occurs.

---

### **Which One Should You Use?**

1. **Prefer `ViewBag`** when you want concise and clean code for passing small amounts of data dynamically.
2. **Prefer `ViewData`** when you:
    - Need to pass data explicitly by string keys.
    - Work in scenarios where dynamic properties are not desirable.
3. **For strongly-typed views**, avoid both in favor of **ViewModels**, as they provide compile-time safety and better maintainability.

## OOP

argument vs parameter
### **Encapsulation**
https://www.geeksforgeeks.org/c-sharp-encapsulation/

 Encapsulation is defined as the wrapping up of data and information under a single unit. It is the mechanism that binds together the data and the functions that manipulate them. In a different way, encapsulation is a protective shield that prevents the data from being accessed by the code outside this shield.

- Technically in encapsulation, the variables or data of a class are hidden from any other class and can be accessed only through any member function of its own class in which they are declared.
- As in encapsulation, the data in a class is hidden from other classes, so it is also known as ****data-hiding****.
- ****Encapsulation can be achieved by:**** Declaring all the variables in the class as private and using [****C# Properties****](https://www.geeksforgeeks.org/c-properties/) in the class to set and get the values of variables.

// C# program to illustrate encapsulation

```c#
using System;
 
public class DemoEncap {
 
    // private variables declared
    // these can only be accessed by
    // public methods of class
    private String studentName;
    private int studentAge;
 
    // using accessors to get and
    // set the value of studentName
    public String Name
    {
 
        get { return studentName; }

        set { studentName = value; }
    }
 
    // using accessors to get and
    // set the value of studentAge
    public int Age
    {
 
        get { return studentAge; }
 
        set { studentAge = value; }
    }
}
 
// Driver Class
class GFG {
 
    // Main Method
    static public void Main()
    {
 
        // creating object
        DemoEncap obj = new DemoEncap();
 
        // calls set accessor of the property Name,
        // and pass "Ankita" as value of the
        // standard field 'value'
        obj.Name = "Ankita";
 
        // calls set accessor of the property Age,
        // and pass "21" as value of the
        // standard field 'value'
        obj.Age = 21;
 
        // Displaying values of the variables
        Console.WriteLine(" Name : " + obj.Name);
        Console.WriteLine(" Age : " + obj.Age);
    }
}
```

#### Advantages of Encapsulation

- ****Data Hiding:**** The user will have no idea about the inner implementation of the class. It will not be visible to the user that how the class is stored values in the variables. He only knows that we are passing the values to accessors and variables are getting initialized to that value.
- ****Increased Flexibility:**** We can make the variables of the class as read-only or write-only depending on our requirement. If we wish to make the variables as read-only then we have to only use Get Accessor in the code. If we wish to make the variables as write-only then we have to only use Set Accessor.
- ****Reusability:**** Encapsulation also improves the re-usability and easy to change with new requirements.
- ****Testing code is easy:**** Encapsulated code is easy to test for unit testing.

Encapsulation is a fundamental concept in object-oriented programming (OOP) that refers to the bundling of data and the methods that operate on that data within a single unit. In C#, this is typically achieved through the use of classes.

The idea behind encapsulation is to keep the implementation details of a class hidden from the outside world, and to only expose a public interface that allows users to interact with the class in a controlled and safe manner. This helps to promote modularity, maintainability, and flexibility in the design of software systems.
#### DTO
A Data Transfer Object is an object that is used to encapsulate data, and send it from one subsystem of an application to another.

DTOs are most commonly used by the Services layer in an N-Tier application to transfer data between itself and the UI layer. The main benefit here is that it reduces the amount of data that needs to be sent across the wire in distributed applications. They also make great models in the MVC pattern.

Another use for DTOs can be to encapsulate parameters for method calls. This can be useful if a method takes more than four or five parameters.

### **Inheritance**
Inheritance is a fundamental concept in object-oriented programming that allows us to define a new class based on an existing class. The new class inherits the properties and methods of the existing class and can also add new properties and methods of its own. Inheritance promotes code reuse, simplifies code maintenance, and improves code organization.
#### Advantages of Inheritance:

1. Code Reusability: Inheritance allows us to reuse existing code by inheriting properties and methods from an existing class.
2. Code Maintenance: Inheritance makes code maintenance easier by allowing us to modify the base class and have the changes automatically reflected in the derived classes.
3. Code Organization: Inheritance improves code organization by grouping related classes together in a hierarchical structure.

[****Inheritance:****](https://www.geeksforgeeks.org/inheritance-in-java/) 

For any bird, there are a set of predefined properties which are common for all the birds and there are a set of properties which are specific for a particular bird. Therefore, intuitively, we can say that all the birds inherit the common features like wings, legs, eyes, etc. Therefore, in the object-oriented way of representing the birds, we first declare a bird class with a set of properties which are common to all the birds. By doing this, we can avoid declaring these common properties in every bird which we create. Instead, we can simply __inherit__ the bird class in all the birds which we create. The following is an example of how the concept of inheritance is implemented.
### **Polymorphism**

The word polymorphism is made of two words poly and morph, where poly means many and morphs means forms. In programming, polymorphism is a feature that allows one interface to be used for a general class of actions. In the above concept of a bird and pigeon, a pigeon is inherently a bird. And also, if the birds are further categorized into multiple categories like flying birds, flightless birds, etc. the pigeon also fits into the flying bird’s category. And also, if the animal class is further categorized into plant-eating animals and meat-eating animals, the pigeon again comes into the plant-eating animal’s category. Therefore, the idea of polymorphism is the ability of the same object to take multiple forms. There are two types of polymorphism:

1. ****Compile Time Polymorphism:**** It is also known as static polymorphism. This type of polymorphism is achieved by function overloading or operator overloading. It occurs when we define multiple methods with different signatures and the compiler knows which method needs to be executed based on the method signatures.
2. [****Run Time Polymorphism****](https://www.geeksforgeeks.org/dynamic-method-dispatch-runtime-polymorphism-java/)****:**** It is also known as Dynamic Method Dispatch. It is a process in which a function call to the overridden method is resolved at Runtime. This type of polymorphism is achieved by Method Overriding. When the same method with the same parameters is overridden with different contexts, the compiler doesn’t have any idea that the method is overridden. It simply checks if the method exists and during the runtime, it executes the functions which have been overridden.

### **Abstraction**

Data abstraction is a design pattern in which data are visible only to semantically related functions, to prevent misuse. The success of data abstraction leads to frequent incorporation of [data hiding](https://en.wikipedia.org/wiki/Data_hiding "Data hiding") as a design principle in object-oriented and pure functional programming.
#### Encapsulation vs Data Abstraction

- [**Encapsulation**](https://www.geeksforgeeks.org/c-encapsulation/) is data hiding(information hiding) while Abstraction is detail hiding(implementation hiding).
- While encapsulation groups together data and methods that act upon the data, data abstraction deal with exposing to the user and hiding the details of implementation.

	StackOverflow:
	https://stackoverflow.com/questions/15176356/difference-between-encapsulation-and-abstraction
	
	**Encapsulation** hides variables or some implementation that may be changed so often **in a class** to prevent outsiders access it directly. They must access it via getter and setter methods.
	
	**Abstraction** is used to hide something too, but in a **higher degree (class, interface)**. Clients who use an abstract class (or interface) do not care about what it was, they just need to know what it can do.

#### Advantages of Abstraction

- It reduces the complexity of viewing things.
- Avoids code duplication and increases reusability.
- Helps to increase the security of an application or program as only important details are provided to the user.




[****Abstraction:****](https://www.geeksforgeeks.org/abstraction-in-java-2/)

Abstraction in general means hiding. In the above scenario of the bird and pigeon, let’s say there is a user who wants to see pigeon fly. The user is simply interested in seeing the pigeon fly but not interested in how the bird is actually flying. Therefore, in the above scenario where the user wishes to make it fly, he will simply call the fly method by using ****pigeon.fly()**** where the pigeon is the object of the bird pigeon. Therefore, abstraction means the art of representing the essential features without concerning about the background details. In Java, the abstraction is implemented through the use of [interface](https://www.geeksforgeeks.org/interfaces-in-java/) and [abstract classes](https://www.geeksforgeeks.org/abstract-classes-in-java/). We can achieve complete abstraction with the use of Interface whereas a partial or a complete abstraction can be achieved with the use of abstract classes. The reason why abstraction is considered as one of the important concepts is:

1. It reduces the complexity of viewing things.
2. Avoids code duplication and increases reusability.
3. Helps to increase security of an application or program as only important details are provided to the user.


