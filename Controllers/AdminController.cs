using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeLeaveManagementSystem.Services;
using EmployeeLeaveManagementSystem.Models;

namespace EmployeeLeaveManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]  // Only Admin can access
    public class AdminController : ControllerBase
    {
        private readonly ILeaveService _leaveService;     //Field to hold reference to leave service

        //Constructor → ILeaveService is injected automatically (Dependency Injection)
        public AdminController(ILeaveService leaveService)
        {
            _leaveService=leaveService;        //Save reference for use in methods
        }

        // GET: api/admin/leaverequests→ Fetch all leave requests
        [HttpGet("leaverequests")]
        //Async API method that returns an HTTP response with data or error.
        public async Task<ActionResult<IEnumerable<LeaveRequest>>> GetAllLeaveRequests()
        {
            var leaves = await _leaveService.GetAllAsync();   //Call service to get all leave requests (with employee data)
            return Ok(leaves);                                //Return 200 OK with data
        }

        // PUT: api/admin/approve/5 → Approve a leave by ID
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveLeave(int id)
        {
            var leave = await _leaveService.UpdateLeaveStatusAsync(id, "Approved");   //Update status to "Approved"
            if (leave == null) return NotFound();                                     //If leave not found → return 404
            return Ok(leave);                                                         //Return updated leave request
        }

        // PUT: api/admin/reject/5 → Reject a leave by ID
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectLeave(int id)
        {
            var leave = await _leaveService.UpdateLeaveStatusAsync(id, "Rejected");    //Update status to "Rejected"
            if (leave == null) return NotFound();
            return Ok(leave);
        }

        // GET: api/admin/balance/3 ->Check leave balance for an employee
        [HttpGet("balance/{empId}")]
        public async Task<ActionResult<LeaveBalance>> GetLeaveBalance(int empId)
        {
            var balance = await _leaveService.GetBalanceAsync(empId);           //Call service to get leave balance by employee ID
            if (balance == null) return NotFound();
            return Ok(balance);
        }
    }
}

//AdminController → Allows admins to approve/reject leave & view balances.
//AdminController.cs provides Admin-only APIs to view all leave requests,
//approve/reject them, and check employee leave balances.