namespace EmployeeLeaveManagementSystem.Models.DTOs
{
    public class EmployeeUpdateDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; }
        // Usually you don't update Password here, but you can add if needed
    }
}
