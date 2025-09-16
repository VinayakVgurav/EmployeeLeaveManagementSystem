using EmployeeLeaveManagementSystem.Models;

namespace EmployeeLeaveManagementSystem.Services
{
    //ILeaveService is an interface → only declares methods, no logic inside.
    // It acts as a "contract" that LeaveService must follow.
    public interface ILeaveService
    {
        // Method to apply for a new leave.
        // Takes a LeaveRequest object and returns the created LeaveRequest after saving.
        Task<LeaveRequest> ApplyLeaveAsync(LeaveRequest request);

        //Method to update the status of a leave (e.g., Approved / Rejected).
        //Takes a leave request id and new status, returns the updated LeaveRequest or null if not found.
        Task<LeaveRequest?> UpdateLeaveStatusAsync(int id, string status);

        // Method to fetch all leave requests.
        // Returns a list of LeaveRequest objects (with async execution).
        Task<IEnumerable<LeaveRequest>> GetAllAsync();

        // Method to fetch leave balance for a specific employee by EmployeeID.
        // Returns LeaveBalance or null if not found.
        Task<LeaveBalance?> GetBalanceAsync(int empid);
    }
}

