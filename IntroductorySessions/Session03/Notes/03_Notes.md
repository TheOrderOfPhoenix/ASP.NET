# Session 3: Views, Razor Syntax, Bootstrap, and Helpers

## 📝 Overview

In this session, we covered the following concepts:

- Creating and Organizing Views in MVC
- Razor Syntax for dynamic content rendering
- Bootstrap integration for responsive UI
- Using Tag Helpers and HTML Helpers

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

## 🧪 Practice

- Create a view with a layout and partial
- Use Razor syntax to display dynamic data
- Build a form with Tag Helpers
- Refactor a view using HTML Helpers

## 🙏 Acknowledgments

Sources:

- [Microsoft Learn - Razor](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-9.0)
- [Microsoft Learn - Views](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/overview?view=aspnetcore-9.0)
- [W3Schools - Bootstrap](https://www.w3schools.com/bootstrap/bootstrap_ver.asp)
- ChatGPT Assistance (2025 sessions)
