# Library Management System (LMSystem)

A Library Management System built with **ASP.NET Core MVC**, **Entity Framework Core (Code First)**, and **SQL Server**, following industry conventions for validation, model binding security, and exception handling.

## Features

- **Book Management (CRUD)** — list, view details, add, edit, and delete books.
- **Borrow / Return workflow** — borrowers can check out an available book and return it later; a book's availability updates automatically.
- **Model binding security** — `BookId` and `IsAvailable` are decorated with `[BindNever]` so they can never be set via form/POST data (prevents over-posting attacks); availability is only ever changed by server-side logic during borrow/return.
- **Validation** — required fields, string length limits, ISBN format (`XXX-XXXXXXXXXX`), email and phone format for borrower details, all enforced via Data Annotations and reflected in the UI with `asp-validation-for`.
- **Graceful error handling** — custom `NotFound` view for missing books/records, generic `Error` view for unhandled exceptions, and try/catch blocks with user-facing messages (`TempData`) in every controller action.
- **Bootstrap 5 UI** — responsive layout, styled tables, cards, and forms.

## Tech Stack

| Layer          | Technology                              |
|----------------|------------------------------------------|
| Framework      | ASP.NET Core MVC (.NET 8.0)              |
| ORM            | Entity Framework Core 8.0.0 (Code First) |
| Database       | SQL Server (LocalDB by default)          |
| Validation     | Data Annotations                         |
| Styling        | Bootstrap 5, Bootstrap Icons             |

## Project Structure

```
LMSystem/
├── Controllers/
│   ├── BooksController.cs      # CRUD for books
│   ├── BorrowController.cs     # Borrow / Return workflow
│   └── HomeController.cs       # Redirects to Books list; handles global errors
├── Models/
│   ├── Book.cs
│   ├── BorrowRecord.cs
│   ├── LibraryContext.cs       # DbContext, seed data
│   └── ErrorViewModel.cs
├── Views/
│   ├── Books/                  # Index, Details, Create, Edit, Delete
│   ├── Borrow/                 # Create (borrow form)
│   └── Shared/                 # _Layout, NotFound, Error
├── Migrations/                 # EF Core migrations
└── appsettings.json             # Connection string
```

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (installed with Visual Studio) or a full SQL Server instance

### Setup

1. **Clone the repo**
   ```bash
   git clone https://github.com/<your-username>/<your-repo>.git
   cd LMSystem
   ```

2. **Update the connection string** in `LMSystem/appsettings.json` if you're not using the default LocalDB instance:
   ```json
   "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LMS;..."
   ```

3. **Apply migrations** to create the database and seed data:
   ```bash
   cd LMSystem
   dotnet ef database update
   ```
   *(If you don't have the EF Core CLI tool: `dotnet tool install --global dotnet-ef`)*

4. **Run the app**
   ```bash
   dotnet run
   ```
   Navigate to the URL shown in the console (e.g. `https://localhost:xxxx`). The default route lands on the **Books list**.

## Known Limitations / Ideas for Future Improvement

- `BorrowRecord.BookId` is bound from a hidden form field rather than `[BindNever]`, since the controller needs to know which book is being borrowed. It is currently re-validated against the route parameter on `GET`, but not strictly re-checked byte-for-byte against a tampered `POST` value — fine for a coursework/demo project, worth hardening if this goes to production.
- No authentication/authorization — the app assumes a single trusted administrator persona, per the original requirements.
- No search, filtering, or pagination on the Books list yet.
- No automated tests.

## License

This is a learning/practice project. Feel free to fork and adapt.
