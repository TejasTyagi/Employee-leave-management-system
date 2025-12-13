//noahb
//description: class for Employee model
namespace Employee_leave_management_system.Models;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Role { get; set; }

    public int? ManagerId { get; set; }
}
