using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveManagementSystem.Models
{
    public class LeaveBalance
    {
        [Key]
        public int LeaveBalanceID { get; set; }

        [ForeignKey ("Employee")]
        public int EmployeeID { get; set; }
        public int AnnualLeave { get; set; }
        public int SickLeave { get; set; }
        public int CasualLeave { get; set; }
        public int OtherLeave { get; set; }
        public Employee Employee { get; set; }
    }
}
