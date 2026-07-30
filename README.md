# Library Management System

A Library Management System built using **ASP.NET Core MVC**, **Entity Framework Core (Code First)**, and **SQL Server**. The application enables efficient management of books and borrowing records while following industry-standard practices such as MVC architecture, secure model binding, server-side validation, and exception handling.

---

## Features

- Complete **CRUD operations** for managing books.
- Borrow and return books with automatic availability updates.
- Secure model binding using `[BindNever]` to prevent over-posting attacks.
- Server-side validation using Data Annotations.
- Custom error handling with dedicated **Not Found** and **Error** pages.
- User-friendly notifications using `TempData`.
- Responsive user interface built with **Bootstrap 5**.

---

## Tech Stack

| Category | Technology |
|----------|------------|
| Framework | ASP.NET Core MVC (.NET 8) |
| Language | C# |
| ORM | Entity Framework Core (Code First) |
| Database | SQL Server / LocalDB |
| Frontend | Razor Views |
| Styling | Bootstrap 5 |
| Validation | Data Annotations |

---

## Project Structure

```text
LMSystem
│
├── Controllers
│   ├── BooksController.cs
│   ├── BorrowController.cs
│   └── HomeController.cs
│
├── Models
│   ├── Book.cs
│   ├── BorrowRecord.cs
│   ├── LibraryContext.cs
│   └── ErrorViewModel.cs
│
├── Views
│   ├── Books
│   ├── Borrow
│   └── Shared
│
├── Migrations
├── wwwroot
├── appsettings.json
├── Program.cs
└── LMSystem.csproj
```

---

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server LocalDB or SQL Server
- Visual Studio 2022 (Recommended)

### Installation

Clone the repository:

```bash
git clone https://github.com/Gaurav-Pareta/LMSystem.git
```

Navigate to the project directory:

```bash
cd LMSystem
```

Restore dependencies:

```bash
dotnet restore
```

Apply database migrations:

```bash
dotnet ef database update
```

> If the EF Core CLI is not installed:

```bash
dotnet tool install --global dotnet-ef
```

Run the application:

```bash
dotnet run
```

Open the URL displayed in the terminal (typically `https://localhost:xxxx`).

---

## Key Concepts Demonstrated

- ASP.NET Core MVC Architecture
- Entity Framework Core (Code First)
- CRUD Operations
- SQL Server Integration
- Razor Views
- Model Binding
- Data Validation
- Exception Handling
- Bootstrap 5 UI Development

---

## Future Improvements

- Authentication and Authorization
- Search, Filter, and Pagination
- Role-Based Access Control
- Book Categories
- Fine Management
- Email Notifications
- Unit and Integration Testing

---

## License

This project was developed for learning purposes and portfolio demonstration. Feel free to fork and improve it.

---

## Author

**Gaurav Pareta**

- GitHub: https://github.com/Gaurav-Pareta