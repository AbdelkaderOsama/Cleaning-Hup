# CleaningHub API 🧼

A RESTful API backend for managing a cleaning products wholesale store — built with **ASP.NET Core 8** following **Clean Architecture** principles.

The system handles products, inventory tracking, customers, orders, payments, and secure role-based access for store employees and admins.

---

## 🚀 Features

- **Product & Category Management** — full CRUD operations for cleaning products and their categories.
- **Inventory Tracking** — automatic stock deduction when an order is placed, plus manual stock-in/stock-out operations and low-stock alerts.
- **Customer Management** — store customer details and track outstanding balances.
- **Order Management** — create multi-item orders linked to customers, with automatic total calculation and inventory deduction.
- **Payment Tracking** — record payments against orders and automatically update order status (Pending / Completed).
- **Authentication & Authorization** — secure JWT-based login system with role-based access control (Admin / Employee).
- **Input Validation** — robust request validation using FluentValidation with clear Arabic error messages.
- **Global Error Handling** — centralized exception handling middleware that returns clean, consistent error responses.
- **AutoMapper Integration** — clean separation between database models and API contracts (DTOs).

---

## 🏗️ Architecture

The project follows **Clean Architecture** principles with clear separation of concerns:

```
Cleaning_Hup/
├── Controllers/        # API endpoints
├── Services/           # Business logic implementation
├── Abstraction/         # Service interfaces (contracts between layers)
├── Contracts/           # Request & Response DTOs
├── Models/              # Database entities
├── Persistance/         # EF Core DbContext & Migrations
├── Validators/          # FluentValidation rules
├── Mapping/             # AutoMapper profiles
└── Middlewares/         # Custom middleware (exception handling)
```

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 (Web API) |
| Database | SQL Server |
| ORM | Entity Framework Core |
| Authentication | JWT Bearer Tokens |
| Validation | FluentValidation |
| Object Mapping | AutoMapper |
| Password Hashing | BCrypt.Net |
| API Documentation | Swagger / OpenAPI |

---

## 📦 Core Entities

| Entity | Description |
|---|---|
| `Category` | Product categories (e.g. detergents, disinfectants) |
| `Product` | Cleaning products with wholesale & retail pricing |
| `Inventory` | Current stock levels per product |
| `InventoryTransaction` | Audit log of all stock movements (IN/OUT) |
| `Customer` | Wholesale customers/clients |
| `Order` | Customer orders with line items |
| `OrderItem` | Individual products within an order |
| `Payment` | Payments recorded against orders |
| `User` | System users with role-based access (Admin/Employee) |

---

## 🔐 Authentication

The API uses **JWT Bearer Authentication**. All endpoints (except `/api/Auth/register` and `/api/Auth/login`) require a valid token.

**Register a new user:**
```http
POST /api/Auth/register
Content-Type: application/json

{
  "username": "admin",
  "password": "yourpassword",
  "role": "Admin"
}
```

**Login:**
```http
POST /api/Auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "yourpassword"
}
```

The response includes a JWT token to be sent in the `Authorization` header for subsequent requests:
```
Authorization: Bearer <your-token-here>
```

> **Note:** Delete operations on Category, Product, Customer, and Order are restricted to users with the `Admin` role.

---

## 📡 API Endpoints

| Resource | Endpoint | Methods |
|---|---|---|
| Auth | `/api/Auth` | POST (register, login) |
| Category | `/api/Category` | GET, POST, PUT, DELETE |
| Product | `/api/Product` | GET, POST, PUT, DELETE |
| Inventory | `/api/Inventory` | GET, POST (update) |
| Customer | `/api/Customer` | GET, POST, PUT, DELETE |
| Order | `/api/Order` | GET, POST, PUT (status), DELETE |
| Payment | `/api/Payment` | GET, POST |

Full interactive documentation available via **Swagger UI** when running the project locally.

---

## ⚙️ Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 (recommended) or any C# IDE

### Setup

1. **Clone the repository**
```bash
git clone https://github.com/AbdelkaderOsama/Cleaning-Hup.git
cd Cleaning-Hup
```

2. **Configure the database connection**

Update the connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=CleaningHubDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

3. **Apply database migrations**
```bash
Update-Database
```
(Run this from the Package Manager Console in Visual Studio)

4. **Run the project**

Press `F5` in Visual Studio, or:
```bash
dotnet run
```

5. **Explore the API**

Open the Swagger UI at:
```
https://localhost:<port>/swagger
```

---

## 🧪 Testing the API

The API was thoroughly tested using **Postman**, covering:
- Full CRUD flows for every entity
- Order creation with automatic inventory deduction
- Payment processing and order status updates
- Authentication and role-based authorization (Admin vs. Employee permissions)
- Validation error handling for invalid input
- Global exception handling for unexpected errors

---

## 📌 Project Status

This is a **graduation project**, actively developed and incrementally improved. Current focus areas include:
- Reporting and analytics endpoints
- Pagination for large result sets
- Potential frontend integration (cashier & admin dashboards)

---

## 👤 Author

**Abdelkader Osama**
— Backend Development (.NET)
