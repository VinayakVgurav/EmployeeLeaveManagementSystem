using EmployeeLeaveManagementSystem.Models;
using EmployeeLeaveManagementSystem.Models.DTOs;
using EmployeeLeaveManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeLeaveManagementSystem.Controllers;


namespace EmployeeLeaveManagementSystem.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
       private readonly Mydbcontext _context;           //Reference to database context for DB operations

        //Constructor → ASP.NET Core injects Mydbcontext automatically using Dependency Injection
        public EmployeeController(Mydbcontext context)
        {
            _context = context;  //Save context for use in methods
        }

        // GET: api/employee
        [HttpGet]

        //Fetch an employee by their ID and return a UserDTO in the HTTP response, or 404 if not found
        //This is a public API method that runs asynchronously and will return an HTTP response (ActionResult) containing
        //a list of UserDTO objects when someone calls GET /api/employee.
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetEmployees()
        {
            //Query Employees table and project each Employee into a UserDTO (safe object)
            var employees = await _context.Employees
                   .Select(e => new UserDTO
                   {
                      EmployeeID = e.EmployeeID,     //Map EmployeeID → Id field in DTO
                       Name =e.Name,
                      Email = e.Email,
                      Role = e.Role


                   }).ToListAsync();      //Execute query asynchronously and return as List
            return Ok(employees);          //Return 200 OK with employee list
        }

        //GET: api/employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);   //Find employee by ID (primary key lookup)
            if (emp ==null) return NotFound();

            //Convert Employee entity to UserDTO and return
            return new UserDTO
            {
                EmployeeID = emp.EmployeeID,
                Name = emp.Name,
                Email = emp.Email,
                Role = emp.Role
            };
        }

        //POST: api/employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee emp)
        {
            _context.Employees.Add(emp);      //Add new employee entity to DbContext
            await _context.SaveChangesAsync();//Save changes to DB

            //Return 201 Created with route to GetEmployee and newly created employee
            return CreatedAtAction(nameof(GetEmployee), new { id = emp.EmployeeID }, emp);
        }

        //PUT: api/employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id,Employee emp)
        {
            var existing = await _context.Employees.FindAsync(id);  //Look up employee by ID
            if (existing == null) return NotFound();

            //Update fields of existing employee with new values
            existing.Name = emp.Name;
            existing.Email = emp.Email;
            existing.Role = emp.Role;
            existing.Department = emp.Department;
            existing.Designation = emp.Designation;

            await _context.SaveChangesAsync();    //Save changes to DB
            return NoContent();

        }

        //DELETE: api/employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {

            var emp = await _context.Employees.FindAsync(id);  //Find employee by ID
            if (emp == null) return NotFound();

            _context.Employees.Remove(emp);                   //Mark entity for deletion
            await _context.SaveChangesAsync();                //Apply deletion in DB
            return NoContent();
        }
    }
}

//Role & Use in one line
//EmployeeController.cs exposes REST APIs for CRUD operations on employees,
//returning safe data via UserDTO instead of exposing full Employee entity.

//Meaning of this line:public async Task<ActionResult<IEnumerable<UserDTO>>> GetEmployees()
//public → This method can be called from outside the class (in this case, by HTTP requests).
//async → Marks this method as asynchronous.
//It allows use of await inside (like await _context.Employees.ToListAsync()).
//It helps keep the API responsive while waiting for DB queries.
//Task<...> → The method returns a Task because it’s async.
//Think of a Task as a "promise to return something later".
//The real result will be inside <...>.
//ActionResult<IEnumerable<UserDTO>> → The actual return type.
//ActionResult<T> → Special return type in controllers, so you can return Ok(), NotFound(), BadRequest(), etc., along with a value.
//IEnumerable<UserDTO> → A collection (list) of UserDTO objects.
//GetEmployees() → The method name, and since it’s marked with [HttpGet], it responds to GET api/employee requests.
