using EmployeeLeaveManagementSystem.Data;
using EmployeeLeaveManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementSystem.Services
{
    //LeaveService implements ILeaveService (the contract for leave operations)
    public class LeaveService : ILeaveService
    {
        private readonly Mydbcontext _context; //Field to hold DbContext (to access tables)

        //Constructor: receives DbContext via dependency injection
        public LeaveService(Mydbcontext context)
        {
            _context=context;
        }

        //Apply for a new leave → saves a LeaveRequest in DB and returns it
        public async Task<LeaveRequest> ApplyLeaveAsync(LeaveRequest request)
        {
            _context.LeaveRequests.Add(request);  //add leave request to LeaveRequests table
            await _context.SaveChangesAsync();    //save changes to DB
            return request;                       //return the newly added request
        }

        //Update the status (Approved/Rejected) of a leave request
        public async Task<LeaveRequest?> UpdateLeaveStatusAsync(int id,string status)
        {
            var leave = await _context.LeaveRequests.FindAsync(id); //find leave request by ID
            if (leave == null) return null;                         //if not found, return null

            leave.Status = status;                                  // update the status field
            await _context.SaveChangesAsync();                      //save changes to DB
            return leave;                                           //return updated leave request

        }

        //Get all leave requests including employee info
        public async Task<IEnumerable<LeaveRequest>> GetAllAsync()
        {
            return await _context.LeaveRequests.Include(l=> l.Employee).ToListAsync(); // also load related Employee data.. return as list asynchronously
        }

        //Get leave balance for a specific employee
        public async Task<LeaveBalance?> GetBalanceAsync(int empId)
        {
            return await _context.LeaveBalances.FirstOrDefaultAsync(b => b.EmployeeID == empId); // find the first leave balance record matching employee ID
        }
    }
}


//LeaveService.cs handles all leave-related business logic (apply leave, approve/reject leave, list requests, check balance)
//by working with the database through Mydbcontext.
//So, in LeaveService.cs, await means:
//Call the database asynchronously, don’t block the app, and continue once the DB responds.