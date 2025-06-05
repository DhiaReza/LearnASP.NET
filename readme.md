# ASP.NET Core Learning-by-Doing Roadmap

# Source : https://dotnettutorials.net/lesson/asp-dot-net-mvc-tempdata/

A comprehensive, hands-on roadmap for mastering ASP.NET Core, including backend fundamentals, web development, security, testing, and advanced architecture.

---

## 1. C# Fundamentals (Duration: 2–3 Weeks)

**📌 Project:** Basic Console-Based Library Management System

### Topics to Master
- Syntax, data types, operators, control structures
- Object-Oriented Programming (OOP): Classes, Inheritance, Polymorphism, Encapsulation, Abstraction
- Collections: `List`, `Dictionary`; LINQ queries
- Exception handling: `try-catch-finally`, custom exceptions
- Asynchronous programming: `async/await`, `Task`, `Thread`

### Hands-on Activities
- Console calculator using control flow
- Mini CRM system using classes and collections
- LINQ queries for in-memory customer/product data
- Asynchronous file reader and simulated API calls

---

## 2. .NET Core Basics (Duration: 2 Weeks)

**📌 Project:** Console-Based Configuration-Driven App

### Topics to Master
- .NET Core architecture and runtime
- CLI Commands: `dotnet new`, `dotnet build`, `dotnet run`
- Project structure and SDK-style project files
- Configuration: `appsettings.json`, environment variables, user secrets
- Dependency Injection basics
- Middleware and request pipeline
- Logging and error handling

### Hands-on Activities
- Scaffold project with CLI
- Implement DI-based logging service
- Load and display configurations
- Add custom middleware to a minimal web app

---

## 3. Web Development

### 3.1 ASP.NET Core MVC / Razor Pages (Duration: 3–4 Weeks)

**📌 Project:** Full-Featured Blog Platform (CRUD)

#### Topics to Master
- MVC architecture, Razor syntax
- Routing (attribute-based, conventional)
- Controllers, Views, ViewModels
- Model Binding and Validation
- Filters (Action, Exception, Authorization)
- Areas for modular architecture

#### Hands-on Activities
- Blog CRUD with comments
- DataAnnotations for validation
- Custom action filters (logging/auditing)
- Modular areas: admin/user

### 3.2 Web API Development (Duration: 2–3 Weeks)

**📌 Project:** Task Manager RESTful API

#### Topics to Master
- REST principles, HTTP methods/status codes
- API versioning, content negotiation
- CORS configuration
- Swagger for API documentation
- Rate limiting and API security

#### Hands-on Activities
- CRUD API endpoints
- Swagger/OpenAPI documentation
- CORS policy configuration
- Add rate limiting middleware

---

## 4. Database Technologies

### 4.1 Entity Framework Core (Duration: 2 Weeks)

**📌 Project:** Enhance Blog/Task Manager with EF Core

#### Topics to Master
- Code First vs Database First
- Migrations and schema management
- CRUD operations with `DbContext`
- Relationships (1:1, 1:N, M:N)
- Lazy vs Eager loading
- Query performance tuning

#### Hands-on Activities
- Integrate SQL Server/SQLite
- Author-post-comment relationships
- Navigational properties and joins
- LINQ queries with profiling

### 4.2 Database Systems (Duration: 1–2 Weeks)

**📌 Project:** Product Catalog with Multiple DBs

#### Tools
- SQL Server (core development)
- PostgreSQL (via Npgsql)
- MongoDB (`MongoDB.Driver`)
- Redis (via `IDistributedCache`)

#### Hands-on Activities
- Integrate multiple database systems
- Redis caching in APIs
- MongoDB for logs/documents

---

## 5. Security (Duration: 2–3 Weeks)

**📌 Project:** Secure Blog or API App

### Topics to Master
- JWT Authentication, OAuth2, OpenID Connect
- Role, Policy, Claims-based authorization
- Data protection (`IDataProtector`)
- HTTPS and security headers
- Protection from XSS, CSRF, SQL Injection

### Hands-on Activities
- JWT-secured API endpoints
- Role-based admin panel
- Anti-forgery tokens for forms
- Enforce HTTPS and add headers

---

## 6. Testing (Duration: 2 Weeks)

**📌 Project:** Add Tests to Blog/API Project

### Topics to Master
- Unit Testing (MSTest, NUnit, xUnit)
- Integration Testing (`WebApplicationFactory`)
- Mocking (Moq, NSubstitute)
- Code coverage (e.g., Coverlet)
- TDD methodology

### Hands-on Activities
- Unit tests for services/controllers
- Mock dependencies
- API integration testing
- CI with GitHub Actions or Azure Pipelines

---

## 7. Advanced Concepts

### 7.1 Design Patterns (Duration: 2 Weeks)

**📌 Project:** Refactor Projects Using Patterns

#### Topics to Master
- Repository, Factory, Singleton
- Observer, Strategy
- SOLID principles, clean code

#### Hands-on Activities
- Abstract EF using Repository
- Logging factory implementation
- Observer for event notifications

### 7.2 Architecture (Duration: 2–3 Weeks)

**📌 Project:** Modular E-commerce Microservice App

#### Topics to Master
- Clean Architecture (presentation, application, domain, infrastructure)
- Microservice communication: REST, gRPC
- Domain-Driven Design (DDD) basics
- CQRS with MediatR
- Introduction to Event Sourcing

#### Hands-on Activities
- Implement CQRS with MediatR
- Layer separation using Clean Architecture
- Simulated event-driven messaging

---

## 8. Additional Skills (Ongoing)

### Frontend Development (Optional but Valuable)

**📌 Project:** Frontend for Your API

#### Options
- HTML/CSS + JavaScript
- React + Axios
- Angular or Vue.js
- Blazor Server/WebAssembly

---

## Best Practices

- Use `.editorconfig` for consistency
- Use `async/await` with `ConfigureAwait(false)`
- Structured logging (e.g., Serilog)
- Swagger/OpenAPI and markdown documentation
- Performance optimization and monitoring (Application Insights)
- Deployment to Azure or AWS

---

## Recommended Timeline Summary

| Stage                      | Estimated Duration |
|---------------------------|--------------------|
| C# Fundamentals            | 2–3 weeks          |
| .NET Core Basics          | 2 weeks            |
| ASP.NET Core MVC & API    | 5–6 weeks          |
| EF Core + Databases       | 3 weeks            |
| Security                  | 2–3 weeks          |
| Testing                   | 2 weeks            |
| Advanced Architecture     | 4 weeks            |
| Frontend Integration      | Ongoing            |

---

## License

This roadmap is provided for educational purposes.
