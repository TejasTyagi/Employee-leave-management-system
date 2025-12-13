//noahb
//description: Database context for the Employee Leave Management System
using Microsoft.EntityFrameworkCore;
using Employee_leave_management_system.Models;

namespace Employee_leave_management_system.Data;

public class Database : DbContext
{
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<Employee> Employees { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=leave.db");
    }
}
