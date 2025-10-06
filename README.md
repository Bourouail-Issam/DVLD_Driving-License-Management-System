# 🪪 DVLD - Driving License Management System

Welcome to the **DVLD (Driving License Management System)** — a comprehensive desktop application designed to streamline the management of driving licenses and related administrative processes.

---

## 🚦 Overview

DVLD is a robust **Windows Forms** application developed in **C#**. It allows users to manage driving license operations including issuance, renewal, testing, and user administration through an intuitive graphical interface.

---

## 🏗️ Technical Architecture

**Three-Tier Architecture**:
- 🖼️ **Presentation Layer**: Windows Forms (WinForms)
- ⚙️ **Business Layer**: Application logic and rules
- 🗂️ **Data Access Layer**: ADO.NET with Microsoft SQL Server

**Technologies Used**:
- 👨‍💻 **Language**: C# (.NET Framework)
- 🖥️ **User Interface**: Windows Forms
- 🗃️ **Database**: Microsoft SQL Server
- 🔗 **Data Access**: ADO.NET

---

## 📋 Main Services

### 🚘 Driving License Services
- New license issuance
- License renewal
- Replacement for lost licenses
- Replacement for damaged licenses
- International license issuance
- Driving test scheduling and results management

### 🧑‍💼 System Management
- Applicant and personal data management
- Application submission and status tracking
- User and role management
- Examination/test result recording

---

## 🔧 Key Features

### ✅ Eligibility Verification
- Check applicant eligibility based on rules
- Prevent duplicate applications
- Ensure required documents/tests are completed

### 📊 Data Management
- Centralized storage of applicant information
- Full tracking of requests and system activity
- Reporting and statistics generation

### 🧪 Testing System
- Manual scheduling of tests (vision, theoretical, practical)
- Record and track test results and status

---

## 💾 Database Design

- ✅ Relational database with normalized structure
- ⚙️ Use of stored procedures for key operations
- 🔄 Efficient handling of connections and queries

---

## 🚀 Getting Started

### 🧰 Prerequisites
- Windows OS
- .NET Framework installed
- SQL Server installed and configured

### 🛠️ Installation Steps
1. Clone this repository
2. Restore the database from the provided `.bak` file
3. Update database connection strings in the config files
4. Build and run the application via Visual Studio

---

## 📁 Project Structure

```plaintext
DVLD/
├── PresentationLayer/     # User Interface (WinForms)
├── BusinessLayer/         # Business Logic
├── DataAccessLayer/       # Data Access with ADO.NET
└── Database/              # SQL Server Scripts & Backup Files
