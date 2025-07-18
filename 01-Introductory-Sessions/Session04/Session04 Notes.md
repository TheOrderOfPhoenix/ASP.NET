# Session 4: Views, Razor Syntax, Bootstrap, and Helpers

## 📝 Overview

In this session, we covered the following concepts:

- Creating and Organizing Views in MVC
- Razor Syntax for dynamic content rendering
- Bootstrap integration for responsive UI
- Using Tag Helpers and HTML Helpers
- View Components
- ViewImports and ViewStart
- ViewData vs ViewBag
- OOP Concepts in MVC: Encapsulation, Inheritance, Polymorphism, Abstraction, DTO

## 📚 Topics Covered

### ✅ Creating and Organizing Views

> Learn how views are structured in ASP.NET Core MVC, how to use layouts and partials.

### ✅ Razor Syntax

> Use C# inside HTML with Razor to build dynamic views  
> ⭐ [Razor Docs](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-9.0)

### ✅ Bootstrap

> Integrate responsive design into your app using Bootstrap  
> ⭐ [Bootstrap W3Schools](https://www.w3schools.com/bootstrap/bootstrap_ver.asp)

### ✅ Tag Helpers and HTML Helpers

> Tools to simplify HTML generation in Razor views

## 📌 Notes

> _Collected from various sources including Microsoft Learn, W3Schools, and ChatGPT_

### Part 1: Introduction to Views

- Views (.cshtml files) are templates that render HTML with Razor syntax.
- Views are organized in `Views/ControllerName/ViewName.cshtml`.
- The `View()` method in a controller renders the corresponding view.
- Layouts (\_Layout.cshtml) allow reuse of common HTML structure like headers/footers.
- Partial Views help modularize repeated sections (like a profile box).

### Part 2: Razor Syntax

- Use `@` to enter C# in HTML.
- Implicit: `@Model.Name`, Explicit: `@(Model.Name + "!")`
- Code blocks: `@{ var msg = "Hello"; }`
- Loops and conditionals are supported: `@if`, `@for`, `@foreach`, `@switch`
- Razor supports local functions in code blocks for reusability

### Part 3: Bootstrap

- Bootstrap helps in building responsive UI components.
- Grid system, forms, buttons, navbars, modals etc. are supported.
- Integration involves adding Bootstrap CSS/JS in layout or view.

### Part 4: Tag Helpers and HTML Helpers

#### Tag Helpers:

- Looks like HTML with `asp-*` attributes
- Example: `<a asp-controller="Home" asp-action="About">Link</a>`
- Requires `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers` in `_ViewImports.cshtml`

#### HTML Helpers:

- Use C# syntax like `@Html.TextBoxFor(...)`, `@Html.BeginForm()`
- More verbose but offers fine control with C# expressions

#### Comparison:

| Feature     | Tag Helpers | HTML Helpers |
| ----------- | ----------- | ------------ |
| Syntax      | HTML-like   | C# methods   |
| Readability | High        | Moderate     |
| Use         | `asp-*`     | `@Html.*`    |

### Part 5: View Components

- View components are reusable UI elements with server-side logic.
- Similar to partial views but support code execution (e.g., DB queries).
- Suitable for dynamic parts like shopping carts or recent posts.

### Part 6: ViewImports and ViewStart

- `_ViewStart.cshtml` sets the layout for Razor Pages. Placing one in a folder affects all views inside it.
- `_ViewImports.cshtml` adds namespaces, tag helpers, etc.
- Hierarchical: Local files override root-level ones.

### Part 7: ViewData vs ViewBag

| Feature            | ViewData          | ViewBag       |
| ------------------ | ----------------- | ------------- |
| Type               | Dictionary        | Dynamic       |
| Access             | `ViewData["Key"]` | `ViewBag.Key` |
| Compile-time check | ❌                | ❌            |
| Shared storage     | ✅                | ✅            |

- Use ViewBag for simpler, cleaner access; ViewData for explicit key-value access.
- Both are weakly typed and best avoided in favor of ViewModels in large apps.

### Part 8: OOP Concepts in MVC

#### Encapsulation

- Use `private` fields with public `get`/`set` properties.
- Prevents external access to internal data structures.

#### DTO (Data Transfer Object)

- Used to carry data between layers.
- Reduces coupling and increases control over exposed data.

#### Inheritance

- Share common logic between base and derived classes.
- Enables better reuse and organization.

#### Polymorphism

- **Compile-time**: Method overloading.
- **Runtime**: Method overriding.
- One interface, many implementations.

#### Abstraction

- Hides complex implementation from the user.
- Achieved via abstract classes or interfaces.

## 🧪 Practice

- Create a view with a layout and partial
- Use Razor syntax to display dynamic data
- Build a form with Tag Helpers
- Refactor a view using HTML Helpers
- Create and render a View Component
- Use `_ViewStart.cshtml` and `_ViewImports.cshtml` for configuration

## 🙏 Acknowledgments

Sources:

- [Microsoft Learn - Razor](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-9.0)
- [Microsoft Learn - Views](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/overview?view=aspnetcore-9.0)
- [W3Schools - Bootstrap](https://www.w3schools.com/bootstrap/bootstrap_ver.asp)
- [GeeksforGeeks - OOP in C#](https://www.geeksforgeeks.org/c-sharp-oops-concepts/)
- ChatGPT Assistance (2025 sessions)
