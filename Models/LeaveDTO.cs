using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeLeaveManagementSystem.Models
{
    public class LeaveDTO
    {
        public int LeaveBalanceID { get; set; }
        public int EmployeeID { get; set; }
        public int AnnualLeave { get; set; }
        public int SickLeave { get; set; }
        public int CasualLeave { get; set; }
        public int OtherLeave { get; set; }
        public Employee Employee { get; set; }
    }
}
