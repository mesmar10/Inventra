# Inventra
Warehouse & Sales Management System built with ASP.NET Core Web API.

A comprehensive Warehouse and Sales Management System built with ASP.NET Core Web API to streamline inventory operations, sales processing, warehouse management, and delivery workflows.

---

## Overview

Inventra is a backend system designed to help organizations manage products, warehouses, inventory movements, customer orders, deliveries, and sales analytics.

The system implements a clean layered architecture using Repository Pattern, Unit of Work Pattern, DTOs, JWT Authentication, and Role-Based Authorization.

---

## Key Features

### Authentication & Authorization

- JWT Authentication
- Secure Login System
- Role-Based Authorization
- Protected API Endpoints

### User Roles

#### Admin

- Full system access
- Employee management
- Product management
- Warehouse management
- Reports access

#### Warehouse Manager

- Manage products
- Manage customers
- Approve sales orders
- View reports
- Manage inventory

#### Warehouse Worker

- View inventory
- Manage stock records

#### Delivery Driver

- View assigned orders
- Complete payments
- Confirm deliveries

---

## Inventory Management

- Product Management
- Warehouse Management
- Product-Warehouse Tracking
- Stock Quantity Monitoring
- Reorder Level Management
- Stock Transfers Between Warehouses
- Low Stock Notifications

---

## Sales Management

- Customer Management
- Sales Order Creation
- Sales Order Approval Workflow
- Payment Tracking
- Delivery Tracking
- Coupon and Discount Support

---

## Order Workflow

```text
Customer
    ↓
Create Order
    ↓
Pending Order
    ↓
Warehouse Manager Approval
    ↓
Stock Deduction
    ↓
Delivery Driver
    ↓
Payment Collection
    ↓
Order Completed
```

---

## Reporting Module

The system provides business insights through:

- Total Sales Report
- Top Customers Report
- Top Products Report
- Low Stock Report

---

## Technologies Used

### Backend

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- LINQ

### Security

- JWT Authentication
- Role-Based Authorization

### Design Patterns

- Repository Pattern
- Unit Of Work Pattern
- Dependency Injection

### API Documentation

- Swagger / OpenAPI

---

## Project Architecture

```text
Inventra
│
├── Controllers
├── Services
├── Repository
├── DTOs
├── Models
├── Data
├── Mappings
├── Migrations
└── Program.cs
```

---

## Main Modules

### Employees

- Create Employees
- Update Employees
- Manage Roles

### Warehouses

- Create Warehouses
- Update Warehouses
- Warehouse Inventory Tracking

### Products

- Create Products
- Update Products
- Manage Pricing

### Customers

- Customer Management
- Customer Order History

### Sales Orders

- Create Orders
- Apply Coupons
- Approve Orders
- Complete Payments

### Stock Transfers

- Transfer Inventory Between Warehouses

### Notifications

- Low Stock Alerts

### Reports

- Sales Analytics
- Product Performance
- Customer Analytics

---

## API Security

The API uses JWT Bearer Tokens for authentication.

Example:

```http
Authorization: Bearer <token>
```

Role-based policies ensure that each user only has access to permitted operations.

---

## Future Enhancements

- Angular Frontend
- Dashboard Analytics
- Notification Center
- Email Notifications
- Audit Logs
- Pagination & Filtering
- Advanced Reporting

---

## Author

Hossameddin Mesmar

Computer Science Graduate | .NET Developer

---

## Project Status

✅ Authentication & Authorization

✅ Inventory Management

✅ Warehouse Management

✅ Sales Management

✅ Delivery Workflow

✅ Coupon System

✅ Reporting Module

✅ Low Stock Notifications

✅ GitHub Integration

🚀 Backend Version Complete
