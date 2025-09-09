using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveManagementSystem.Models
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public string LeaveType { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public string Reason { get; set; } 
        public string Status { get; set; } 
        public string AdminRemarks { get; set; }
        public DateTime DateSubmitted { get; set; }
        public Employee Employee { get; set; }
    }
}
