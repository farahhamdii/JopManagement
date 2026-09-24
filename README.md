# Job Application API

A backend RESTful API for managing job vacancies and job applications.

The project provides authentication and role-based authorization for Candidates, Recruiters, and Admins, while using a layered architecture to keep the application organized and maintainable.

## Features

* User Registration and Login
* JWT Authentication
* Role-Based Authorization
* Admin Management
* Admin can create Recruiters
* Candidate Management
* Job Management
* Recruiter can create and update jobs
* Recruiter can view their own jobs
* Soft Delete for Jobs
* Candidate Job Applications
* Apply to a Job
* Cancel an Application
* Recruiter can update Application Status
* View Application Details
* View My Applications
* Filter and Paginate Applications
* Filter and Paginate Jobs
* Automatic rejection of applications that remain UnderReview for more than 7 days
* Background notifications using Hangfire
* Recurring background jobs
* Swagger API Documentation

---

## Technologies

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core 8
* SQL Server
* ASP.NET Core Identity
* JWT Authentication
* MediatR
* CQRS
* Hangfire
* Swagger / Swashbuckle

---

## Architecture

The project follows a layered architecture:

```text
JobApplication
│
├── JobApplication.API
│
├── JobApplication.Application
│
├── JobApplication.Domain
│
└── JobApplication.Infrastructure
```

### 1. API

Responsible for:

* Controllers
* HTTP Requests and Responses
* Authentication/Authorization configuration
* Swagger
* API endpoints

### 2. Application

Contains the application business logic:

* Features
* Commands
* Queries
* Handlers
* DTOs
* Interfaces
* Validation and application rules

CQRS with MediatR is used to separate commands and queries.

### 3. Domain

Contains the core business entities and enums.

Main entities include:

* Job
* Candidate
* JobCandidateApplication
* ApplicationUser

### 4. Infrastructure

Responsible for external and persistence-related concerns:

* Entity Framework Core
* SQL Server
* ApplicationDbContext
* ASP.NET Core Identity
* Repositories
* JWT services
* Hangfire
* Background services

---

## User Roles

The system contains three main roles:

### Admin

Responsible for administrative operations.

* Manage users
* Create Recruiters

### Recruiter

Responsible for managing job vacancies and applications.

* Create Jobs
* Update Jobs
* Cancel Jobs
* View My Jobs
* View Applications
* Update Application Status

### Candidate

Responsible for applying to jobs.

* View Jobs
* Apply to Jobs
* Cancel Applications
* View My Applications

---

## Authentication

The API uses:

```text
ASP.NET Core Identity
        +
JWT Bearer Authentication
```

After successful login, the API returns a JWT token.

The token is then sent with protected requests using:

```http
Authorization: Bearer {token}
```

Role-based authorization is used to restrict endpoints according to the user's role.

---

## Job Management

The system supports:

### Get All Jobs

Supports filtering and pagination.

Example:

```http
GET /api/jobs?pageNumber=1&pageSize=10
```

### Get Job By Id

```http
GET /api/jobs/{id}
```

### Create Job

Recruiters can create new jobs.

```http
POST /api/jobs
```

### Update Job

```http
PUT /api/jobs/{id}
```

### Cancel Job

Jobs are soft deleted instead of being permanently removed.

```http
DELETE /api/jobs/{id}
```

Soft delete is implemented using:

```text
IsDeleted
DeletedAt
```

---

## Job Application Management

Candidates can apply for active jobs.

### Apply to a Job

```http
POST /api/applications
```

The system checks that:

* The candidate exists
* The job exists
* The job is active
* The job is not deleted
* The candidate has not already applied

A new application starts with:

```text
Applied
```

and records:

```text
AppliedAt
StatusUpdatedAt
```

---

## Application Status

Applications can have the following statuses:

```text
Applied
UnderReview
Interview
Accepted
Rejected
Cancelled
```

Recruiters can update the application status.

When the status changes, `StatusUpdatedAt` is updated.

---

## Cancel Application

Candidates can cancel their applications.

```http
PUT /api/applications/{id}/cancel
```

The cancellation time is stored in:

```text
CancelledAt
```

---

## Application Filtering & Pagination

Applications support filtering and pagination.

Example:

```http
GET /api/applications?pageNumber=1&pageSize=10
```

Filters can be used according to the available application filter request.

The response contains:

```json
{
  "items": [],
  "pageNumber": 1,
  "pageSize": 10,
  "totalCount": 0,
  "totalPages": 0
}
```

---

# CQRS & MediatR

The project uses CQRS to separate operations into:

### Commands

Commands change data.

Examples:

```text
ApplyJobCommand
CancelApplicationCommand
CreateJobCommand
UpdateJobCommand
CancelJobCommand
UpdateApplicationStatusCommand
```

### Queries

Queries retrieve data without changing it.

Examples:

```text
GetAllJobsQuery
GetJobByIdQuery
GetMyJobsQuery
GetAllApplicationsQuery
GetApplicationByIdQuery
GetMyApplicationsQuery
```

MediatR is responsible for dispatching commands and queries to their corresponding handlers.

---

# Repository Pattern

Repositories are used to abstract database access from the application layer.

Examples:

```text
IJobRepository
IApplicationRepository
ICandidateRepository
```

Their implementations are located in the Infrastructure layer.

The project does not use a Unit of Work pattern.

---

# Hangfire

Hangfire is used to execute background and recurring jobs.

It is configured with a separate SQL Server database:

```text
JobApplicationHangfireDb
```

### Background Notification

When a candidate applies for a job, a Hangfire background job is created to notify the recruiter.

The application request does not need to perform the notification operation directly.

The flow is:

```text
Candidate
   ↓
Apply for Job
   ↓
Application Created
   ↓
Hangfire Enqueue
   ↓
Notification Job
   ↓
Recruiter Notification
```

### Recurring Job

The project also contains a recurring background task that automatically rejects applications that have remained in:

```text
UnderReview
```

for more than:

```text
7 days
```

The service checks:

```text
StatusUpdatedAt <= DateTime.UtcNow - 7 days
```

and changes the status to:

```text
Rejected
```

For testing, the recurring job can temporarily be configured to run every minute:

```csharp
Cron.Minutely
```

In the final configuration, it can be scheduled daily:

```csharp
Cron.Daily
```

The recurring job can be monitored through the Hangfire Dashboard.

---

# Hangfire Dashboard

The Hangfire Dashboard is available at:

```text
/hangfire
```

From the dashboard you can monitor:

* Enqueued Jobs
* Processing Jobs
* Succeeded Jobs
* Failed Jobs
* Scheduled Jobs
* Recurring Jobs

The recurring application-expiration job appears under:

```text
Recurring Jobs
```

with the identifier:

```text
auto-reject-expired-applications
```

---

# Database

The project uses SQL Server with Entity Framework Core.

There are two databases:

### Application Database

Stores:

* Users
* Roles
* Candidates
* Jobs
* Applications
* Other application data

### Hangfire Database

Stores Hangfire's background-job data.

```text
JobApplicationHangfireDb
```

---

# Configuration

Update `appsettings.json` with your SQL Server and JWT configuration.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_APPLICATION_CONNECTION_STRING",
    "HangfireConnection": "Server=.\\SQLEXPRESS;Database=JobApplicationHangfireDb;Trusted_Connection=True;TrustServerCertificate=True"
  },

  "Jwt": {
    "Key": "YOUR_SECRET_KEY",
    "Issuer": "YOUR_ISSUER",
    "Audience": "YOUR_AUDIENCE"
  }
}
```

Do not commit real passwords, secret keys, or production connection strings to GitHub.

---

# Entity Framework Core

After configuring the connection string, apply the migrations.

```bash
dotnet ef database update
```

If migrations are created from the Infrastructure project while the API project is the startup project, use the appropriate project parameters, for example:

```bash
dotnet ef database update --project JobApplication.Infrastructure --startup-project JobApplication.API
```

---

# Running the Project

### 1. Clone the repository

```bash
git clone YOUR_REPOSITORY_URL
```

### 2. Open the solution

Open:

```text
JobApplication.sln
```

### 3. Configure the database

Update:

```text
appsettings.json
```

with your SQL Server connection string.

### 4. Apply migrations

```bash
dotnet ef database update
```

### 5. Run the API

```bash
dotnet run
```

Or run the project directly from Visual Studio.

---

# Swagger

Swagger is used to test and document the API.

After running the project, open the Swagger URL displayed by ASP.NET Core.

Swagger allows you to:

* Register users
* Login
* Authorize using JWT
* Create jobs
* View jobs
* Apply for jobs
* Manage applications
* Test protected endpoints

For protected endpoints:

1. Login and copy the JWT token.
2. Click **Authorize** in Swagger.
3. Enter:

```text
Bearer YOUR_TOKEN
```

4. Execute the protected endpoint.

---

# Application Flow

The main application flow is:

```text
User
 │
 ├── Register
 │
 └── Login
       │
       ↓
     JWT Token
       │
       ↓
 ┌───────────────┐
 │               │
Candidate      Recruiter
 │               │
 ↓               ↓
View Jobs      Create Job
 │               │
 ↓               ↓
Apply Job      Manage Job
 │
 ↓
Application
 │
 ↓
Applied
 │
 ↓
UnderReview
 │
 ├── Interview
 ├── Accepted
 ├── Rejected
 │
 └── After 7 Days UnderReview
              ↓
           Hangfire
              ↓
           Rejected
```

---

# Project Goals

The project demonstrates practical backend development concepts including:

* RESTful API development
* Layered Architecture
* Clean separation of responsibilities
* Entity Framework Core
* SQL Server
* Identity
* JWT Authentication
* Role-Based Authorization
* Repository Pattern
* CQRS
* MediatR
* DTOs
* Filtering
* Pagination
* Soft Delete
* Background Processing
* Recurring Jobs
* API Documentation with Swagger

---

## Author

**Farah Hamdy**

Backend .NET Developer

GitHub: `farahhamdii`
