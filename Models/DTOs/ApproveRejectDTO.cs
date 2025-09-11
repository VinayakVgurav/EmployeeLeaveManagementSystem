namespace EmployeeLeaveManagementSystem.Models.DTOs
{
    public class ApproveRejectDTO
    {
        public int LeaveRequestID { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }
}
