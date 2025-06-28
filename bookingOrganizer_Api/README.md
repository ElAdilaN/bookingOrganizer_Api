# 📦 bookingOrganizer_Api

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)

A meticulously crafted **ASP.NET Core Web API**, designed to provide a robust backend for a booking organization and sharing application. This project showcases a clean, layered architectural approach, sophisticated object mapping strategies, and adherence to modern coding best practices.

---

## 📚 Table of Contents

- [✨ Project Vision](#-project-vision)
- [🏗️ Project Architecture](#-project-architecture)
- [📊 Database Schema](#-database-schema-entity-relationship-diagram)
- [⚙️ Technologies Used](#-technologies-used-a-deep-dive)
- [💡 Design Highlights & Best Practices](#-design-highlights--best-practices)
- [🔄 Sample Endpoint Flow](#-sample-endpoint-flow-getbookinginfobyid)
- [🧪 Testing](#-testing)
- [🚀 Getting Started](#-getting-started)
- [🧭 Future Enhancements](#-future-enhancements)
- [📚 Final Thoughts](#-final-thoughts)

---

## ✨ Project Vision

**Organize & Share Your Bookings Seamlessly**

The core idea behind `bookingOrganizer_Api` is to empower users to effortlessly organize their bookings (e.g., flights, hotels, events) and share them with relevant groups — like family, teams, or friends.

---

## 🏗️ Project Architecture

Layered and modular architecture ensures maintainability and testability:

```
├── Controllers/         → API endpoints
├── DAO/                 → Data access implementations
├── DTO/                 → Data Transfer Objects
├── Exceptions/          → Custom exception classes
├── IDAO/                → DAO interfaces
├── Models/              → EF Core database entities
├── Repository/          → Business interfaces
├── Service/             → Business logic implementations
├── UTILS/               → Manual mapping utilities
├── MappingProfiles/     → AutoMapper configuration
└── Program.cs           → App bootstrap
```

---

## 📊 Database Schema (Entity-Relationship Diagram)

### Core Entities:

- **Users**: Manages individual accounts
- **Groups**: Shared collections (e.g., "Work Trip")
- **GroupMembers**: Many-to-many mapping (Users ↔ Groups)
- **TypeBooking**: Categorizes bookings
- **BookingInfo**: Central entity holding booking details

> Group-linked bookings enable seamless sharing across users.

---

## ⚙️ Technologies Used: A Deep Dive

| Component         | Tech / Package           | Why It Was Used                                                                 |
|------------------|--------------------------|----------------------------------------------------------------------------------|
| Framework        | ASP.NET Core             | High-performance, cross-platform web API framework                              |
| ORM              | Entity Framework Core    | Simplifies database access with LINQ, type safety, migrations                   |
| Database         | SQL Server               | Reliable, scalable relational database with full EF Core support                |
| DI               | Built-in .NET DI         | Loose coupling via constructor-based dependency injection                       |
| API Docs         | Swagger (Swashbuckle)    | Auto-generates and visualizes API endpoints                                     |
| Object Mapping   | AutoMapper + Manual      | Streamlined DTO ↔ Entity conversion, with custom mapping support                |
| Exception Mgmt   | Custom Exception Types   | Domain-specific errors improve robustness                                       |
| CORS             | Built-in .NET CORS       | Enables secure frontend-backend communication                                   |

---

## 💡 Design Highlights & Best Practices

### 🔹 Clean Dependency Injection

```csharp
public ServiceBookingInfo(IDAOBookingInfo daoBookingInfo, IMapper mapper)
{
    _daoBookingInfo = daoBookingInfo;
    _mapper = mapper;
}
```

- Promotes loose coupling and testability via IoC

---

### 🔹 Unified Wrapper Response

```json
{
  "status": "200",
  "message": "Success",
  "data": {
    "bookingInfo": {
      "bookingId": 123,
      "date": "2025-07-01",
      "clientName": "John Doe",
      "groupName": "Family Vacation",
      "type": "Hotel"
    }
  }
}
```

- Standardizes responses for easier frontend integration

---

### 📦 `ICollection<T>` Return Type

```csharp
public static ICollection<DTOBookingInfo> ConvertBookingsToDTOBookings(ICollection<BookingInfo> bookings)
```

- Promotes programming to interfaces over implementations

---

### 🔄 Mapping Strategies

#### ✅ AutoMapper

```csharp
public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingInfo, DTOBookingInfo>();
        CreateMap<DTOBookingInfo, BookingInfo>();
        CreateMap<Group, DTOGroup>();
    }
}
```

Registered in `Program.cs`:

```csharp
builder.Services.AddAutoMapper(typeof(Program));
```

---

#### 🛠 Manual Mapping (UTILS)

```csharp
public static ICollection<DTOGroup> ConvertGroupsToDTOGroups(ICollection<Group> groups)
{
    return groups.Select(g => new DTOGroup
    {
        GroupId = g.GroupId,
        GroupName = g.GroupName
    }).ToList();
}
```

---

## 🔄 Sample Endpoint Flow: `getBookingInfoById`

### 🔹 Controller

```csharp
[HttpGet("getBookingInfoById")]
public IActionResult GetBookingInfoById(int id)
{
    try
    {
        var dto = _repoBookingInfo.getBookingInfoById(id);
        return Ok(new Wrapper("200", "Success", new { bookingInfo = dto }));
    }
    catch (NotFoundException nf) { return NotFound(new Wrapper("404", nf.Message)); }
    catch (Exception ex) { return StatusCode(500, new Wrapper("500", ex.Message)); }
}
```

### 🔹 Service

```csharp
public DTOBookingInfo getBookingInfoById(int id)
{
    var booking = _daoBookingInfo.GetBookingInfoById(id);
    return _mapper.Map<DTOBookingInfo>(booking);
}
```

### 🔹 DAO

```csharp
public BookingInfo GetBookingInfoById(int id)
{
    var result = _context.BookingInfos.FirstOrDefault(b => b.BookingId == id);
    if (result == null) throw new NotFoundException($"Booking with ID {id} not found.");
    return result;
}
```

---

## 🧪 Testing

> _(Add this section as tests are developed)_

- Project can use `xUnit`, `NUnit`, or `MSTest`
- Mocks: Recommended to use `Moq` for interfaces like `IDAOBookingInfo`
- Suggest using dependency injection to test services in isolation

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/bookingOrganizer_Api.git
cd bookingOrganizer_Api
```

### 2. Configure Database

Edit `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\MSSQLLocalDB;Database=BookingOrganizerDB;Integrated Security=True;"
}
```

### 3. Apply EF Core Migrations

```bash
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run
```

> Visit `https://localhost:7000/` for Swagger UI

---

## 🧭 Future Enhancements

- [ ] Add IdentityServer or JWT-based OAuth
- [ ] Booking export (CSV, Excel)
- [ ] Rate limiting & logging middleware
- [ ] API Versioning
- [ ] Pagination for large lists

---

## 📚 Final Thoughts

`bookingOrganizer_Api` serves as a blueprint for a modern, layered, scalable ASP.NET Core backend. It demonstrates:

- ✅ Clean architecture & separation of concerns  
- ✅ DTOs with dual mapping strategies  
- ✅ Exception safety across all layers  
- ✅ Real-world usability for collaborative booking scenarios  

---
