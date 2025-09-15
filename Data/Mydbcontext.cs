using EmployeeLeaveManagementSystem.Models;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeLeaveManagementSystem.Data
{
    public class Mydbcontext:DbContext
    {
        public Mydbcontext(DbContextOptions<Mydbcontext>options):base(options)
        { 

        }   
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }


        // OnModelCreating is a special EF Core method.
        //OnModelCreating()=place where you tell EF Core how to build the database from your models.
        // It runs when the model (tables) are being created.
        //ModelBuilder=tells EF Core how to build the database schema and insert seed data.
        // We can configure extra rules and also add seed data here.
        //Seed Data=Seed data means initial/default data that EF Core automatically inserts into your database when it is created or migrated.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Create an Admin user for the system (seed data).
            //Whenever you create the database, insert this Admin user into the Employees table automatically.
            //Seed Admin User with fixed password hash (for "Admin@123")
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeID = 1,
                Name = "System Admin",
                Email = "admin@leave.com",
                Department = "HR",
                Designation = "Administrator",
                Role = "Admin",

                //Fixed hash string (generated once using PasswordHasher)
                Password = "AQAAAAEAACcQAAAAEKrOrrnN6sN2uZfLzJfMFRU2Z5c7FgCeNQ=="
            });

            //Also give the Admin some default leave balance (seed data).
            modelBuilder.Entity<LeaveBalance>().HasData(new LeaveBalance
            {
                LeaveBalanceID = 1,   
                EmployeeID = 1,       
                AnnualLeave = 20,     
                SickLeave = 10,
                CasualLeave = 5,
                OtherLeave = 0
            });

        }






        //MyDbContext = bridge between C# classes and DB tables.
        //DbSet<T> = represents a table.
        //OnModelCreating = lets us configure tables and seed initial data.
        //Seeding = auto-creates Admin user + Admin leave balance when DB is created.
    }
}
