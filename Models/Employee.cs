using Azure.Core;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeLeaveManagementSystem.Models
{
    public class Employee
    {
        [Key] //primary key in db table
        public int EmployeeID { get; set; }

        [Required] //must be provided..cant be null/empty
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress] //validates format for email
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Employee";

        // Navigation property (relationship) → represent relationships in the database
        // one employee can have many leave requests.
        public ICollection<LeaveRequest> LeaveRequests { get; set; }

        // one employee has **one leave balance** (Annual, Sick, etc.)
        public LeaveBalance? LeaveBalance { get; set; }
        

        //for understanding why navigation property used
        //ICollection<LeaveRequest>
        //One employee can apply for many leave requests.
        // This sets up a One-to-Many relationship:
        // One Employee → Many LeaveRequests.
        // LeaveBalance
        // Each employee has only one leave balance record.
        // This sets up a One-to-One relationship:
       //One Employee → One LeaveBalance.
    }
}
