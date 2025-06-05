# ✅ To-Do List Application with SQL Server & Entity Framework Core

## 📌 Objective

This project is a multi-entity **To-Do List** application demonstrating advanced use of **Entity Framework Core** with **SQL Server** in a **.NET 6+ ASP.NET Core Web Application**. It includes CRUD operations, secure user authentication, and comprehensive relational data modeling using 1-to-1, 1-to-many, and many-to-many relationships.

---

## 📂 Project Type

- **Framework**: ASP.NET Core Web Application (MVC or Razor Pages)
- **Backend**: .NET 6 or later
- **ORM**: Entity Framework Core (Code-First)
- **Database**: SQL Server

---

## 🧱 Entity & Relationship Design

### 1. `User`
- `UserId` (Primary Key)
- `Username` (Required, Unique)
- `Email` (Required)
- `PasswordHash` (Required)

**Relationships:**
- 1-to-1 with `UserProfile`
- 1-to-many with `ToDoList`

### 2. `UserProfile`
- `UserProfileId` (Primary Key)
- `FullName`
- `DateOfBirth`
- `Bio`
- `UserId` (Foreign Key, Unique)

**Relationship:**
- Linked to `User` (1-to-1)

### 3. `ToDoList`
- `ToDoListId` (Primary Key)
- `Title`
- `Description`
- `CreatedDate`
- `UserId` (Foreign Key)

**Relationships:**
- Belongs to one `User`
- Has many `ToDoItems`

### 4. `ToDoItem`
- `ToDoItemId` (Primary Key)
- `TaskName`
- `DueDate`
- `IsCompleted`
- `ToDoListId` (Foreign Key)

**Relationships:**
- Belongs to one `ToDoList`
- Has many `Tags` (many-to-many)

### 5. `Tag`
- `TagId` (Primary Key)
- `Name` (Required, Unique)

**Relationships:**
- Many-to-many with `ToDoItem` via `ToDoItemTags` join table

---

## 🔄 Functional Requirements

### 🟩 User Management
- User registration and login with password hashing
- View and edit user profile (`UserProfile`)

### 🟩 To-Do List Management
- Create, edit, delete, and view `ToDoLists` for a logged-in user

### 🟩 To-Do Item Management
- Add, edit, mark as complete/incomplete, and delete `ToDoItems`
- Assign multiple `Tags` to a `ToDoItem`

### 🟩 Tag Management
- Create and view `Tags`
- View all items associated with a tag
- Assign tags using multi-select interface

---

## 🔍 Technical Requirements

### ✅ Database & ORM
- Code-First EF Core approach
- SQL Server connection via `appsettings.json`
- Use Migrations for schema generation and updates
- Seed development test data
- Use Lazy/Eager loading where appropriate

### ✅ Validation & Error Handling
- Server-side validation using `DataAnnotations`
- Handle nulls, foreign key violations, and invalid operations
- Display user-friendly error messages

### ✅ Security
- Secure password storage (ASP.NET Identity or manual hashing)
- Ensure users can only access their own data

---

## 🧪 Bonus Enhancements (Optional)

- Due date notifications
- Priority levels: Low, Medium, High
- Export to PDF or Excel
- Filter/search by tag or completion status
- Unit tests for repository/service layers

---

## 🛠 Tools and Technologies

- ASP.NET Core (MVC / Razor Pages)
- Entity Framework Core
- SQL Server
- Visual Studio / VS Code
- DataAnnotations or FluentValidation
- Dependency Injection (Microsoft.Extensions.DependencyInjection)
- AutoMapper (Optional)
- Bootstrap / Tailwind CSS

---

## 📘 Project Outcome

Upon completion, this project will help you:

- Master relational modeling in EF Core
- Understand implementation of various entity relationships
- Gain experience in designing and managing a robust database schema
- Implement secure authentication and user data isolation
- Perform end-to-end CRUD operations using ASP.NET Core and SQL Server

---