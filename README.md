CandidateHub API
A robust .NET 6 Web API built to manage candidate data efficiently with PostgreSQL as the backend. This project includes all required logic, testing, and caching mechanisms as specified, along with several enhancements to boost scalability, maintainability, and developer experience.

✅ Features Implemented
✔️ All core logic and features as outlined in the project requirements.

🧪 Comprehensive testing for key functionalities.

⚙️ Integrated caching mechanism for performance optimization.

📂 Step-by-step development and progress recorded in GitHub commits.

🚀 Enhancements & Additions
🔗 Database Hosting
Azure PostgreSQL Flexible Server: The project is connected to Microsoft Azure Database for PostgreSQL, eliminating the need for local DB configuration.

Added with sample data to demonstrate caching functionality.

✅ Field Validations
Added validation logic for:

Email

Phone Number

GitHub URLs

LinkedIn URLs

🧱 Middleware
Exception Handling Middleware: Captures and handles exceptions thrown during request validation for cleaner API responses.

🔧 Service Configuration
Service Extension: Registers custom services and repositories for dependency injection and applies pending migrations automatically.

💡 Design Patterns
Generic Repository Pattern: Streamlines data access logic and promotes DRY principles.

Auditable Base Class: Tracks entity creation and update timestamps automatically.

AutoMapper Profile: Configured to map between DTOs and entities bidirectionally.

🛠️ Tech Stack
.NET 6

Entity Framework Core

PostgreSQL (Azure Hosted)

xUnit for Testing

AutoMapper

Microsoft.Extensions.Caching


Example of Dto:
{
  "firstName": "Jane",
  "lastName": "Smith",
  "phoneNumber": "+19876543210",
  "email": "jane.smith@example.com",
  "bestCallTime": "Morning",
  "linkedInProfileUrl": "https://www.linkedin.com/in/janesmith",
  "gitHubProfileUrl": "https://github.com/janesmith",
  "comment": "Open to remote opportunities."
}


Before the Cashing:
![{A9AC351A-87D6-45CD-B62A-338BD3FBCEC3}](https://github.com/user-attachments/assets/e072d0ab-738e-4c69-a6fa-3a5eddef0266)

After the Cashing:
![{F20FC3BD-4AF4-4EC9-AA39-4565CDDD4D67}](https://github.com/user-attachments/assets/f4dd1b30-eb92-4b54-80f2-6f41b423b096)



