using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementSystem.Models
{
    public class Mydbcontext:DbContext
    {
        public Mydbcontext(DbContextOptions<Mydbcontext>options):base(options)
        { 

        }   
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
