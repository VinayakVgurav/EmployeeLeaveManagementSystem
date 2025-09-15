using EmployeeLeaveManagementSystem.Data;
using EmployeeLeaveManagementSystem.Helpers;
using EmployeeLeaveManagementSystem.Models;
using EmployeeLeaveManagementSystem.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly Mydbcontext _context;    // EF DbContext used to access database tables
        private readonly IConfiguration _config;  // application configuration (to read JWT settings)
        private readonly PasswordHasher<Employee> _hasher =new PasswordHasher<Employee>();
        // PasswordHasher is used to verify hashed passwords (never store/compare plaintext)

        // Constructor: receives dependencies via Dependency Injection (DI)
        public AuthService(Mydbcontext context, IConfiguration config)
        {
            _context = context;   //save DbContext reference for use in methods
            _config = config;     //save IConfiguration to read Jwt settings

        }

        // LoginAsync: verifies user credentials and returns a JWT token string if valid, otherwise null
        public async Task<string> loginAsync(LoginDTO dto)
        {
            // Query the Employees table for a user with the provided email (async, non-blocking)
            var user = await _context.Employees.FirstOrDefaultAsync(e=>e.Email == dto.Email);
            if (user == null) return null;   // if user not found, return null (login failed)

            // If password is valid, generate a JWT token using JwtHelper
            // JwtHelper uses configuration values (Jwt:Key, Issuer, Audience) to sign token
            var result = _hasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed) return null;

            return JwtHelper.GenerateToken(user.Name, user.Role, _config);
        }

        public Task<string?> LoginAsync(LoginDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}


//Mydbcontext _context — lets service read/write data (employees table, etc.).
//IConfiguration _config — used so token generation reads the secret key and issuer/audience from appsettings.json.
//PasswordHasher<Employee> — secure way to check password: compare hashed stored password with incoming plaintext password. Never compare plain text.
//FirstOrDefaultAsync — asynchronous DB call; keeps API responsive.
//JwtHelper.GenerateToken(...) — centralizes JWT creation, so token format is consistent across the app.