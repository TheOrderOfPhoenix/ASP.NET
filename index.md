# **🐦‍🔥 Order of the Phoenix — Website Development**

## **📌 About the Project**

This repository contains documents, resources, and projects created during the **first phase of the Order of the Phoenix initiative** — a self-organized learning movement started by a group of computer science students.

What began as an effort to escape a passive academic environment evolved into a **collaborative learning experiment** — building a solid foundation in modern web development and culminating in the development of a **Transportation Management System** inspired by platforms like **Booking.com** and **Alibaba.ir**.

The goal wasn’t just to learn a technology; it was to foster **self-driven learning, teamwork, and real-world problem-solving**.



## **📚 Introductory Sessions**

The first part of the program focused on equipping everyone with fundamental web development **and solid software architecture principles**.

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

### **📝 Planning Phase**

- ✅ Selected the project collaboratively (**Transportation Management System**)
- ✅ Designed a detailed **ERD (Entity Relationship Diagram)**, identifying entities, aggregates, and relationships
- ✅ Wrote **full documentation** — requirements, use cases, and architecture diagrams

### **🔨 Implementation Phase**

Each member implemented their **own version** of the project, following Clean Architecture guidelines while experimenting with different approaches.



### **🚦 Project Overview**

#### **🔗 Backend — Transportation Management API**

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



#### **🎨 Frontend — Transportation Management Web App**
- **Tech Stack**: React, TypeScript, Zustand, Axios, Tailwind CSS
- **Focus**: A simple but modern UI to consume the API and simulate real-world use cases


## **👥 Projects by Members**

| **Member**                                                                                   | **Projects**                                                                                                                                                 |
| -------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Mehrdad Shirvani [🐙 GitHub](https://github.com/MehrdadShirvani)**                         | [Backend API](https://github.com/MehrdadShirvani/AlibabaClone-Backend) • [Frontend Web App](https://github.com/mehrdadShirvani/AlibabaClone-Frontend)        |
| **[Member 2 Name] [🐙 GitHub](https://chatgpt.com/c/687a15ad-ab40-8010-8a9d-283c227b7bc3#)** | [Backend API](https://chatgpt.com/c/687a15ad-ab40-8010-8a9d-283c227b7bc3#) • [Frontend Web App](https://chatgpt.com/c/687a15ad-ab40-8010-8a9d-283c227b7bc3#) |
| **[Member 3 Name] [🐙 GitHub](https://chatgpt.com/c/687a15ad-ab40-8010-8a9d-283c227b7bc3#)** | [Backend API](https://chatgpt.com/c/687a15ad-ab40-8010-8a9d-283c227b7bc3#) • [Frontend Web App](https://chatgpt.com/c/687a15ad-ab40-8010-8a9d-283c227b7bc3#) |

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

