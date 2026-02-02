# 🚗 DVLD – Driving License Management System

**WinForms | .NET Framework**

A desktop-based Driving & Vehicle License Department (DVLD) management system built using **C# WinForms (.NET Framework)**.  
The application simulates real-world driving license workflows including license issuance, testing procedures, renewals, and full administrative management.

---

## 📌 Project Overview

DVLD manages official government driving license services such as:

- Driving license issuance
- License renewal
- Lost and damaged license replacement
- International license issuance
- License blocking and releasing
- Applicant management
- Employee system management
- Driving tests scheduling and results

The system enforces real business rules and validation logic based on DVLD requirements.

---

## 🛠 Technology Stack

- **Language:** C#
- **Framework:** .NET Framework
- **UI:** Windows Forms (WinForms)
- **Architecture:** 3-Tier Architecture
- **Database:** SQL Server
- **Data Access:** ADO.NET
- **IDE:** Visual Studio

---

## 🏗 System Architecture

The project follows **Three-Tier Architecture**:

### Layer Responsibilities

- **UI Layer:** Forms and user interaction
- **BLL:** Business rules and validation logic
- **DAL:** Database operations
- **Database:** Persistent storage

---

## 🛠 Main Services

| Service                        | Fee |
| ------------------------------ | --- |
| First Time License Issuance    | $5  |
| Re-Test Service                | $5  |
| License Renewal                | $10 |
| Lost License Replacement       | $20 |
| Damaged License Replacement    | $20 |
| International License Issuance | $20 |

---

## 👤 Applicant (Person) Information

Each applicant is uniquely stored using National ID and includes:

- National Number
- Full Name
- Date of Birth
- Address
- Phone Number
- Email
- Nationality
- Personal Photo

> Duplicate applicant records are not allowed.

---

## 📝 Application Details

Each service request generates an application containing:

- Application ID
- Application Date
- Applicant National ID
- Application Type
- Application Status (New, Completed, Cancelled)
- Paid Fees

### Business Rules

- A person cannot submit multiple active applications of the same type
- Each application is linked to one person
- A person may submit multiple applications

---

## 🚦 License Classes

| ID  | Class Name                     | Min Age | Validity | Fee  |
| --- | ------------------------------ | ------- | -------- | ---- |
| 1   | Small Motorcycle               | 18      | 5 Years  | $15  |
| 2   | Heavy Motorcycle               | 21      | 5 Years  | $30  |
| 3   | Regular Car License            | 18      | 10 Years | $20  |
| 4   | Commercial License (Taxi/Limo) | 21      | 10 Years | $200 |
| 5   | Agricultural Vehicles          | 21      | 10 Years | $50  |
| 6   | Small & Medium Buses           | 21      | 10 Years | $250 |
| 7   | Trucks & Heavy Vehicles        | 21      | 10 Years | $300 |

---

## ✅ License Application Conditions

Applicants must:

- Meet minimum age requirements
- Not already own the same license class
- Pass all required tests
- Submit valid personal documents
- Complete training courses

Applicants may own multiple license classes.

---

## 🧪 Driving Tests Workflow

Applicants must pass tests in the following order:

---

### 1️⃣ Vision Test

- Fee: $10
- Result: Pass / Fail
- Test date and result stored
- Retesting allowed after failure

---

### 2️⃣ Theory Test

- Fee: $20
- Score out of 100
- Scheduled manually by staff
- Paper-based exam
- Retesting requires additional payment

---

### 3️⃣ Practical Driving Test

- Fee depends on license class
- Scheduled manually
- Retesting allowed

---

## 🪪 Issued License Information

Each issued license includes:

- License Number
- Holder Photo
- National ID
- Full Name
- Date of Birth
- License Class
- Issue Date
- Expiry Date
- Notes
- Status (New, Renewed, Lost Replacement, Damaged Replacement)

---

## 🌍 International License Rules

- Available only for **Regular Car License holders (Class 3)**
- License must be active and not blocked
- Only one active international license allowed
- Previous international licenses are archived

---

## 🧑‍💼 System Administration

The system provides administrative tools to manage users, people, applications, and system settings.

---

### 🔄 Re-Test Service

- Available only after failure
- Linked to original application
- Scheduled manually
- Duplicate appointments not allowed

---

### ♻ License Renewal

- Vision test required
- Expired license must be submitted

---

### ❌ Lost License Replacement

- Allowed only if license is not blocked

---

### ⚠ Damaged License Replacement

- Damaged license must be submitted
- Replacement date stored

---

### 🔓 License Release (Unblock)

- Fine must be paid
- Release date recorded

---

## 🧑‍💼 System Administration

The system provides administrative tools to manage users, people, applications, and system settings.

---

### 👥 User Management

Administrators can:

- Add users
- Edit users
- Delete users
- Freeze accounts
- Assign permissions

User data includes:

- Personal information
- Username
- Password

---

### 👤 People Management

- Search by National ID
- Add new person
- Edit person
- Delete person
- Prevent duplicate records

---

### 📋 Application Management

- Search by Application ID
- Search by National ID
- Filter by status
- View fees
- Edit applications

---

### 🧪 Test Management

- Modify test fees only
- Test types are fixed

---

### 🪪 License Class Management

Administrators can modify:

- Minimum allowed age
- Validity period
- License fees

---

### ⛔ License Blocking System

- Block licenses
- Store block reason
- Store block date
- Track responsible employee

---

## 🕒 System Logging

All system operations are logged with:

- User ID
- Action date and time
- Operation type

---

## ⚙ Installation & Setup

### Requirements

- Visual Studio 2019 or later
- .NET Framework
- SQL Server

---

### Setup Steps

1. Clone the repository:
   git clone https://github.com/Hamza-El-Beidouri/DVLD.git

2. Open the solution in Visual Studio

3. Restore NuGet packages (if needed)

4. Update database connection string in: clsDataAccessSettings DAL class

5. Import the SQL database script

6. Build and run the project

---

## 📄 License

Educational Project  
Provided by ProgrammingAdvices.com

---

## ⭐ Author

Developed by **Hamza El Beidouri**  
C# | WinForms | SQL Server | .NET Framework
