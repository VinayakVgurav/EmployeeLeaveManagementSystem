using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; } 
    }
}
