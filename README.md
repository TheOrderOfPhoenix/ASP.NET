# **🐦‍🔥 Order of the Phoenix — Website Development**

## **📌 About the Project**

This repository contains documents, resources, and projects created during the **first phase of the Order of the Phoenix initiative** — a self-organized learning movement started by a group of computer science students.

What began as an effort to escape a passive academic environment evolved into a **collaborative learning experiment** — building a solid foundation in modern web development and culminating in the development of a **Transportation Management System** inspired by platforms like **Booking.com** and **Alibaba.ir**.

The goal wasn’t just to learn a technology; it was to foster **self-driven learning, teamwork, and real-world problem-solving**.



## **📚 Introductory Sessions**

The first part of the program focused on equipping everyone with fundamental web development **and solid software architecture principles**.


| Session | Notes               |
| ------- | ------------------- |
| 00      | [[Session00 Notes]] |
| 01      | [[Session01 Notes]] |
| 02      | [[Session02 Notes]] |
| 03      | [[Session03 Notes]] |
| 04      | [[Session04 Notes]] |
| 05      | [[Session05 Notes]] |
| 06      | [[Session06 Notes]] |
| 07      | [[Session07 Notes]] |

### **🗺️ Roadmap of What We Learned**

1. **🌐 Web Fundamentals** — HTTP, REST, and the client-server model
2. **🧩 MVC (Model-View-Controller)** — basics of application structuring
3. **⚙️ Web API Development** — designing and consuming APIs
4. **🚀 ASP.NET Core Basics** — setting up and building modern backends
5. **🛠️ Dependency Injection (DI)** —
    - Why DI is crucial for **maintainability & testability**
    - Understanding ASP.NET Core service lifetimes (Transient, Scoped, Singleton)
6. **🏗️ Design Patterns** —
    - **Repository Pattern** for data abstraction
    - **Unit of Work** for transaction management
    - **Factory & Builder** for complex object creation
7. **🏛️ Clean Architecture Fundamentals** —
    - Separating **Domain, Application, and Infrastructure** layers
    - Designing business rules independent of frameworks

These sessions ensured that all participants, regardless of prior experience, could contribute to a real-world, **well-architected project**.



## **💻 Project-Based Sessions**

Once the fundamentals were solid, we moved to building a **real project**.

| Session | Notes                                                                            |
| ------- | -------------------------------------------------------------------------------- |
| 00      | [[Session00 Architecture]]                                                       |
| 01      | [[Session01 Additional Info]]<br>[[Session01 Backend]]                           |
| 02      | [[Session02 Additional Info]]<br>[[Session02 Backend]]                           |
| 03      | [[Session03 Additional Info]]<br>[[Session03 Backend]]                           |
| 04      | [[Session04 Additional Info]]<br>[[Session04 Backend]]                           |
| 05      | [[Session05 Additional Info]]<br>[[Session05 Backend]]<br>[[Session05 Frontend]] |
| 06      | [[Session06 Additional Info]]<br>[[Session06 Frontend]]                          |
| 07      | [[Session07 Additional Info]]<br>[[Session07 Backend]]<br>[[Session07 Frontend]] |
| 08      | [[Session08 Backend]]<br>[[Session08 Frontend]]                                  |
| 09      | [[Session09 Additional Info]]<br>[[Session09 Backend]]<br>[[Session09 Frontend]] |
| 10      | [[Docker – Overview]]                                                            |



### **📝 Planning Phase**

- ✅ Selected the project collaboratively (**Transportation Management System**)
- ✅ Designed a detailed **ERD (Entity Relationship Diagram)**, identifying entities, aggregates, and relationships
- ✅ Wrote **full documentation** — requirements, use cases, and architecture diagrams

### **🔨 Implementation Phase**

Each member implemented their **own version** of the project, following Clean Architecture guidelines while experimenting with different approaches.


### **🔗 Backend — Transportation Management API**

A **Clean Architecture-based** backend designed to be **modular, testable, and scalable**.

**Key Highlights:**

- **Domain Layer** 🏛️
    - Defined **entities** 
    - Applied **domain rules** to ensure data consistency 
- **Application Layer** ⚙️
    - Contained **service classes** (e.g., `TicketOrderService`, `AccountService`) that orchestrated business logic.
	- Services handled **validation, entity manipulation, and coordination between repositories**.
	- Mapped DTOs to entities for input/output separation.
- **Infrastructure Layer** 🗄️
    - Implemented **Repository & Unit of Work patterns** using Entity Framework Core.
    - Database interactions abstracted behind interfaces.
- **Presentation Layer** 🌐
    - ASP.NET Core RESTful Web API

**Why Clean Architecture?**
- Clear **dependency direction** (outer layers depend only on inner ones).
- Easier testing (business logic independent of frameworks).
- High **maintainability and scalability**.



### **🎨 Frontend — Transportation Management Web App**
A **modern, component-driven frontend** built with **React + TypeScript**, designed with a focus on **clarity, modularity, and maintainability**.

- **State Management 🧩**
    - Managed predictably using **Zustand**, organized into **well-defined slices** for clear separation of concerns.
    - Promotes ease of composition and scalability as the app grows.
- **API Communication 🔗**
    - All HTTP requests routed through a **centralized Axios instance**.
    - Handles **global error interception, authentication headers, and response transformations** consistently.
- **UI Components 🎨**
    - Built with **TailwindCSS** using a **modular, reusable component approach**.
    - Encourages **visual consistency** and speeds up UI development.
- **Routing & Navigation 🗺️**
    - **React Router DOM** for nested and dynamic routing.
    - Clear and scalable navigation flow for multi-step processes (e.g., ticket reservation).
- **Data Flow 🔄**
    - Strict **unidirectional data flow**, making interactions between components, state, and services predictable and easy to debug.
- **User Experience ✨**
    - A simple but modern UI optimized for clarity and responsiveness, simulating real-world use cases with a clean architecture mindset.

## **👥 Projects by Members**

| **Member**                                                           | **Projects**                                                                                                                                          |
| -------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Mehrdad Shirvani [🐙 GitHub](https://github.com/MehrdadShirvani)** | [Backend API](https://github.com/MehrdadShirvani/AlibabaClone-Backend) • [Frontend Web App](https://github.com/mehrdadShirvani/AlibabaClone-Frontend) |
| **Ali Taherzadeh   [🐙 GitHub](https://github.com/AliThz)**          | [Backend API](https://github.com/alithz/AlibabaClone-Backend) • [Frontend Web App](https://github.com/alithz/AlibabaClone-Frontend)                   |
| **Amin Ghoorchian [🐙 GitHub](https://github.com/AminGh05)**         | [Backend API](https://github.com/AminGh05/Alibaba-Clone-Backend) • [Frontend Web App](https://github.com/AminGh05/Alibaba-Clone-Frontend)             |

## **🙏 Acknowledgements**

A heartfelt thanks to:
- **All members** who dedicated their time and energy, despite busy schedules
- **The core team** who stayed committed through challenges
- Everyone who believed in creating a culture of **self-driven, high-quality software development**

## **🚀 Aspirations for the Project**

This is just the beginning. Our future goals:

- **Publishing all documents and ERDs publicly** for others to learn from
- Growing this into a **community-driven tradition** of collaboration and solving real problems

_"🔥 May this small spark inspire greater movements."_



