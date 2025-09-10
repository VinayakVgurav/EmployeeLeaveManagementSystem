namespace EmployeeLeaveManagementSystem.Models.DTOs
{
    public class RegisterDTO
    {
        //string.Empty is just a constant in .NET equal to "" (empty string).
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
    }
}
