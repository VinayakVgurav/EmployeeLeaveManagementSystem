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
        public Employee? Employee { get; set; } //navigatn prperty=LeaveReq has a relationship with emp table..corresponds to foreign key

        [Required]
        public string LeaveType { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; } 

        [Required]
        public DateTime EndDate { get; set; }

        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string AdminRemarks { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
       
    }
}
