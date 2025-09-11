namespace EmployeeLeaveManagementSystem.Models.DTOs
{
    public class UserDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "Employee";
        public string Token { get; set; } = string.Empty;
    }
}
