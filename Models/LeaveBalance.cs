using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveManagementSystem.Models
{
    public class LeaveBalance
    {
        [Key]
        public int LeaveBalanceID { get; set; }

        [Required,ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }  //navigatn prperty=LeaveBalance has a relationship with emp table..corresponds to foreign key

        public int AnnualLeave { get; set; } = 20;
        public int SickLeave { get; set; } = 10;
        public int CasualLeave { get; set; } = 5;
        public int OtherLeave { get; set; } = 0;
        
    }
}
