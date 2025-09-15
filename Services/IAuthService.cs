using EmployeeLeaveManagementSystem.Models.DTOs;

namespace EmployeeLeaveManagementSystem.Services
{
    //IAuthService.cs defines the contract (methods like LoginAsync) for authentication,
    //so controllers depend on an interface instead of a concrete class (AuthService).
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDTO dto);
    }
}



//Services = business logic layer of your application.
//They sit between Controllers and Database (DbContext).
//A service contains the actual logic/rules of your application (like login, apply leave, approve leave).

//// Why Use Services?
     //Keep controllers clean
    //Controller only handles API request/response.
    //Service does the actual work (login, save to DB, update leave status).

////Reusability
   //Same logic can be used in multiple controllers without repeating code.
   
////Separation of concerns
   //Controller = talks to user
   //Service = contains rules/logic
   //DbContext = talks to database

////Easy to test
  //You can test services separately without needing API endpoints.