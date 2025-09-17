using EmployeeLeaveManagementSystem.Controllers;
using EmployeeLeaveManagementSystem.Models.DTOs;
using EmployeeLeaveManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveManagementSystem.Controllers
{
    [ApiController]                  //Marks this as a Web API controller (automatic validation, response handling)
    [Route("api/[controller]")]      //Sets route → "api/auth" (because controller name is AuthController)
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;  //Dependency: reference to authentication service

        //Constructor: ASP.NET Core automatically injects IAuthService here (Dependency Injection)
        public AuthController(IAuthService authService)
        {
            _authService=authService;   //Store injected service in a private field
        }

        // POST: api/auth/login→ Endpoint for user login
        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            //Calls AuthService.LoginAsync to verify credentials and get JWT token
            var token = await _authService.LoginAsync(dto);
            //If no token returned → login failed (invalid credentials)
            if (token == null) 
                return Unauthorized(new { message = "Invalid email or password"});
            //If login successful → return token in response
            return Ok(new { token });
        }
    }
}
//AuthController.cs exposes an API endpoint (POST /api/auth/login) that uses AuthService to check user credentials
//and return a JWT token for authentication.