# EShopMicroservices
# 🛒 Microservices E-Commerce System

This is a **. . .NET-based Microservices E-Commerce System** that demonstrates modern software practices like **DDD, CQRS, Event-Driven Architecture, and Clean Architecture**.  

It simulates a real-world flow: browsing a catalog, adding items to a basket, applying discounts, checking out, and processing orders.

---

## 🚀 Project Overview

The system is built with multiple microservices, each responsible for a bounded context.  
All services run inside **Docker containers** and communicate through **REST, gRPC, and RabbitMQ**.

---

## 🧩 Services & Architecture

### 🔹 Catalog Service
- Vertical Slice Architecture
- CQRS + MediatR
- Carter Minimal APIs
- Marten (Document DB over PostgreSQL)

### 🔹 Discount Service
- gRPC communication with Basket Service
- SQLite + EF Core
- N-Tier Architecture

### 🔹 Basket Service
- Vertical Slice Architecture
- CQRS + MediatR
- Redis Cache for performance
- Marten persistence
- Carter APIs
- Decorator Pattern (Scrutor)

### 🔹 Ordering Service
- Domain-Driven Design (DDD)
- Clean Architecture + CQRS
- EF Core + SQL Server
- Event publishing & consuming with RabbitMQ

### 🔹 API Gateway
- YARP (Yet Another Reverse Proxy)
- Rate Limiter for throttling requests

### 🔹 Frontend
- Implemented using **Razor Pages** (still improving my skills here 😊)  
- Uses **Refit library** for typed HTTP clients

---

## 📦 System Flow

1️⃣ Customer browses **Catalog** and adds products to **Basket**  
2️⃣ Basket requests discounts from **Discount Service** via gRPC  
3️⃣ On checkout: shipping address, billing info & payment data are submitted  
4️⃣ Basket publishes an event via **RabbitMQ** → consumed by **Ordering Service**  
5️⃣ Ordering Service creates the order, saves it to SQL Server, and raises an `OrderCreated` event  

---

## 🛠️ Tech Stack

- **.NET 8**
- **PostgreSQL** (Catalog with Marten)
- **SQLite** (Discount Service)
- **SQL Server** (Ordering Service)
- **Redis** (Basket Cache)
- **RabbitMQ** (Messaging)
- **Docker & Docker Compose**
- **gRPC + REST APIs + YARP**

---

## 🔮 Future Work / Enhancements

- 📑 **Invoice Service** → Generate PDF invoices for orders  
- 🔔 **Notification Service** → Real-time updates with SignalR / WebSockets  
- 📧 **Email Service** → Order confirmation emails  
- 💳 **Payment Integration** → Stripe or PayPal  
- 🌍 **Frontend** → Explore Angular or Blazor for a richer UI  
- ☁️ **Deployment** → CI/CD with GitHub Actions or Azure DevOps  

---

## 📌 Status

✅ Core services implemented and running in Docker  
✅ RabbitMQ integrated for event-driven communication  
✅ Redis caching and gRPC communication working  
⚙️ Still actively improving and adding new features  

---

## 🤝 Contributions

Suggestions and improvements are welcome!  
Feel free to fork, open issues, or submit pull requests.

