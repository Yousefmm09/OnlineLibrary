# 📚 OnlineLibrary

OnlineLibrary is a **.NET-based web application** designed to provide a digital platform for browsing, managing, and borrowing books online. The system is suitable for universities, libraries, and individual users who want a centralized place for book management.

---

## 🚀 Features

* 🔑 **Authentication & Authorization**

  * User Registration & Login
  * Role-based access (Admin / Super Admin / User)

* 📖 **Book Management**

  * Add, edit, delete, and view books
  * Categorize books by genre or subject
  * Upload book covers and details

* 🔍 **Search & Filter**

  * Search by title, author, or category
  * Sort by price, rating, or availability

* 📅 **Borrowing System**

  * Reserve books online
  * Track borrowing history

* ⚙️ **Admin Panel**

  * Manage users and permissions
  * CRUD operations for categories & books

---


## 🗄 Database Schema

Core Tables:

* **Users** → (Id, Username, Email, PasswordHash, Role)
* **Books** → (Id, Title, Description, Author, CategoryId, Price, ImageUrl)
* **Categories** → (Id, Name)
* **BorrowRecords** → (Id, UserId, BookId, BorrowDate, ReturnDate)
* **Permissions** → (Id, RoleName, CanAdd, CanDelete, CanView)

---

## 🌐 API Endpoints

| Method | Endpoint          | Description         | Roles         |
| ------ | ----------------- | ------------------- | ------------- |
| GET    | `/api/books`      | Get all books       | All Users     |
| GET    | `/api/books/{id}` | Get book by ID      | All Users     |
| POST   | `/api/books`      | Add a new book      | Super Admin   |
| PUT    | `/api/books/{id}` | Update book details | Super Admin   |
| DELETE | `/api/books/{id}` | Delete a book       | Super Admin   |
| GET    | `/api/categories` | List all categories | All Users     |
| POST   | `/api/borrow`     | Borrow a book       | Authenticated |

---

## 🛠 Tech Stack

* **Backend:** ASP.NET Core (.NET 6/7)
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Authentication:** ASP.NET Identity / JWT
* **Version Control:** Git & GitHub

---

## 📦 Requirements

* [.NET SDK 6/7](https://dotnet.microsoft.com/download)
* [SQL Server](https://www.microsoft.com/en-us/sql-server)
* Visual Studio 2022+
* Git

---

## ⚙️ Installation & Setup

```bash
# Clone repo
git clone https://github.com/Yousefmm09/OnlineLibrary.git
cd OnlineLibrary

# Setup database
dotnet ef database update

# Run the app
dotnet run
```

Default URL:

* `https://localhost:5001`
* `http://localhost:5000`

---

## 🧪 Testing

Run tests with:

```bash
dotnet test
```

Test Coverage:

* Unit Tests (xUnit/NUnit)
* Integration Tests for API endpoints



## 🗺 Roadmap / Future Improvements

* ✅ Implement authentication & roles
* ✅ Book management (CRUD)
* 🚧 Online payment integration
* 🚧 Notifications for due dates
* 🚧 AI-based book recommendation system

---

## 🤝 Contribution

Contributions are welcome!

1. Fork this repo
2. Create a feature branch (`feature/my-feature`)
3. Commit & push
4. Submit a Pull Request

---

## 📜 License

This project is licensed under the **MIT License**.

---

## 👨‍💻 Author

**Yousef Mohamed Mohsen**
GitHub: [Yousefmm09](https://github.com/Yousefmm09)
